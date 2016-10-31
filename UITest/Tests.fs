namespace UITest

open NUnit.Framework
open System
open Xamarin.UITest
open Xamarin.UITest.Queries

type Tests() = 

  [<TestCase(Platform.Android)>]
  [<TestCase(Platform.iOS)>]
  member this.AppLaunches(platform : Platform) = 

    let platform = platform
    let app = AppInitializer.startApp(platform)

    app.WaitForElement(fun c -> c.Marked("label").Text("Welcome to F# Xamarin.Forms!")) |> ignore


  [<TestCase(Platform.Android)>]
  [<TestCase(Platform.iOS)>]
  member this.SelectDetails(platform : Platform) = 

    let platform = platform
    let app = AppInitializer.startApp(platform)
    app.Tap(fun c -> c.Marked("OK"))
    app.Tap(fun c -> c.Marked("Contacts"))
    app.WaitForElement(fun c -> c.Marked("email").Text("willsam100@gmail.com")) |> ignore



  [<TestCase(Platform.Android)>]
  [<TestCase(Platform.iOS)>]
  member this.AddItem(platform : Platform) = 

    let platform = platform
    let app = AppInitializer.startApp(platform)
    app.Tap(fun c -> c.Marked("add"))
    app.Repl()