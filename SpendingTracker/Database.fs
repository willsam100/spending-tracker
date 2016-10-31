module Database
open System
open System.Diagnostics
open SQLite
open DatabaseMigration

type Result<'a> = 
    | Success of 'a
    | Failure of string list

type CustId = CustId of string
type ProductId = ProductId of Guid
type ProductInfo = {Id: ProductId option; ProductName: string; DateCreated: DateTime} 

module Result = 
    let map f xResult = 
        match xResult with
        | Success x ->
            Success (f x)
        | Failure errs ->
            Failure errs

    let apply fResult xResult = 
        match fResult,xResult with
        | Success f, Success x ->
            Success (f x)
        | Failure errs, Success x ->
            Failure errs
        | Success f, Failure errs ->
            Failure errs
        | Failure errs1, Failure errs2 ->
            Failure (List.concat [errs1; errs2])

    let bind f xResult = 
        match xResult with
        | Success x ->
            f x
        | Failure errs ->
            Failure errs


type ApiClient() = 
  static let mutable dbConnection: SQLiteConnection = null

  member this.GetProductInfo(id:ProductId) = 
    let (ProductId guid) = id
    try 
      let result = dbConnection.Table<ProductInfoDb>().Where(fun x -> x.Id = guid).FirstOrDefault()
      match result with 
      | null -> Result.Failure [sprintf "Failed with error Not Fount"]
      | _ -> 
            let correctType = {Id = Some <| ProductId (result.Id); ProductName = result.ProductName; DateCreated = result.DateCreated}
            Result.Success (correctType) 
    with 
     | e -> Result.Failure [sprintf "Failed with error %A" e]

  member this.GetProductIds(custId: CustId) = 
    let (CustId customerId) = custId
    try 
      let result = dbConnection.Table<ProductDb>().Where(fun x -> x.CustomerId = customerId).FirstOrDefault()
      match result with 
      | null -> Result.Failure [sprintf "Failed with error Not Found"]
      | _ -> 
          let correctType = [Guid.NewGuid()] |> List.map (fun x -> ProductId x)
          Result.Success (correctType) 
    with 
     | e -> Result.Failure [sprintf "Failed with error %A" e]

  member this.GetProductInfo() = 
    try 
      dbConnection.Table<ProductInfoDb>() 
      |> Seq.map (fun x -> {Id = Some <| ProductId x.Id; ProductName = x.ProductName; DateCreated = x.DateCreated}) 
      |> Seq.toList
      |> List.sortByDescending (fun x -> x.DateCreated)
      |> Result.Success
    with 
     | e -> Result.Failure [sprintf "Failed with error %A" e]

  member this.Set(value:ProductInfo) = 
    try 
      let dbRow = ProductInfoDb()
      dbRow.ProductName <- value.ProductName
      dbRow.DateCreated <- value.DateCreated
      match value.Id with 
      | Some productId -> 
            let (ProductId id) = productId
            dbRow.Id <- id
      | None -> ()
      dbConnection.Insert(dbRow) |> ignore

      Debug.WriteLine <| sprintf "[API] Insert"
      Result.Success ()
    with 
      | e -> Result.Failure [sprintf "Failed with error %A" e]
         
  member this.Open(path: string, provider: SQLitePCL.ISQLite3Provider) =
      Debug.WriteLine <| sprintf "[API] Opening"
      dbConnection <- runMigration path provider

  member this.Close() =
      Debug.WriteLine <| sprintf "[API] Closing"
      dbConnection.Close()

  interface System.IDisposable with
      member this.Dispose() =
          Debug.WriteLine <| sprintf "[API] Disposing"
          dbConnection.Dispose()