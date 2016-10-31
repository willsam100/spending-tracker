namespace SpendingTracker.iOS

open System
open UIKit
open Foundation
open Xamarin
open Xamarin.Forms
open Xamarin.Forms.Platform.iOS
open SpendingTracker

[<Register ("AppDelegate")>]
type AppDelegate () =
    inherit FormsApplicationDelegate ()

    override this.FinishedLaunching (app, options) =
     
        Forms.Init()
        this.LoadApplication (new FormsApp());
       
        #if ENABLE_TEST_CLOUD
          Xamarin.Calabash.Start ()
        #endif

        base.FinishedLaunching (app, options)



module Main =
    [<EntryPoint>]
    let main args =
        UIApplication.Main(args, null, "AppDelegate")
        0

