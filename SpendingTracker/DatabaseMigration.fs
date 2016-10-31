module DatabaseMigration
open System
open SQLite
open System.IO

let dbName = "database.db3"
let dbVersion = 1

[<AllowNullLiteral>]
type ProductDb() = 
  [<PrimaryKey>] [<AutoIncrement>]
  member val Id: Guid = Guid.Empty with get, set
  member val CustomerId: string = "" with get, set
  member val ProductId: Guid = Guid.Empty with get, set

[<AllowNullLiteral>]
type ProductInfoDb() = 
  [<PrimaryKey>] [<AutoIncrement>]
  member val Id: Guid = Guid.Empty with get, set
  member val ProductName: string = "" with get, set
  member val DateCreated: DateTime = DateTime.MinValue with get, set

let migrations = 
  // TODO when required
  // return a list of functions to be exectud for each migration
  [(fun x -> ())]

let runMigration path provider = 

  SQLitePCL.raw.SetProvider(provider)
  let connection = new SQLiteConnection(Path.Combine(path, dbName), false)
  connection.CreateTable<ProductInfoDb>() |> ignore
  connection.CreateTable<ProductDb>() |> ignore

  migrations |> List.iter (fun f -> f())
  connection
