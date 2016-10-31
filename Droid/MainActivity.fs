namespace SpendingTracker.Droid
open System;
open System.IO
open Android.App;
open Android.Content;
open Android.Content.PM;
open Android.Runtime;
open Android.Views;
open Android.Widget;
open Android.OS;
open Xamarin
open Xamarin.Forms.Platform.Android
open BarChart

[<Activity (Label = "SpendingTracker.Droid", Icon = "@mipmap/icon", MainLauncher = true, Theme = "@style/Theme.Splash")>]
type MainActivity() =
    inherit FormsAppCompatActivity()
    override this.OnCreate (bundle: Bundle) =
      this.SetTheme(Resource_Style.MyTheme)
      base.OnCreate (bundle)


      //FormsAppCompatActivity.ToolbarResource <- Resource_Layout.toolbar
      //FormsAppCompatActivity.TabLayoutResource <- Resource_Layout.tabs


      let data = [| 1.f; 2.f; 4.f; 8.f; 16.f; 32.f; |] |> Array.map (fun x -> new BarModel(Value = x))
      let chart = new BarChartView (this, ItemsSource = data);

      this.AddContentView (chart, new ViewGroup.LayoutParams (ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));


        //Xamarin.Forms.Forms.Init (this, bundle)

        //let path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)
        //let write p data = File.WriteAllBytes(p, data)
        //let exportDb name = 
        //  let writePath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path, "database.db")
        //  File.ReadAllBytes(Path.Combine(path, name)) |> write writePath
        //  Toast.MakeText(this, "Db exported to storage", ToastLength.Short).Show()

        //let createApp p dbType exporter = new SpendingTracker.FormsApp(p, dbType, exporter)


        //let app =  createApp path (new SQLitePCL.SQLite3Provider_e_sqlite3()) exportDb
        //this.LoadApplication(app)