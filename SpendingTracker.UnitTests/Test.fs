namespace SpendingTracker.UnitTests
open System
open System.IO
open NUnit.Framework
open SQLite
open SpendingTracker

[<TestFixture>]
type Test() = 


  [<SetUp>]
  member x.Setup() = 
    SQLitePCL.Batteries.Init()
    

  [<Test>]
  member x.TestCase() =

    let upLevel n (path: string) = 
      let dirs = path.Split(Path.DirectorySeparatorChar)
      let upTwoLevels = dirs |> Array.toSeq |> Seq.rev |> Seq.skip n |> Seq.rev |> Seq.toArray
      Path.DirectorySeparatorChar.ToString() + Path.Combine(upTwoLevels)

    let output = Directory.GetCurrentDirectory()
    let path = upLevel 3 output

    DatabaseMigration.runMigration path (new  SQLitePCL.SQLite3Provider_e_sqlite3())

    





