using Android.App;
using Android.Widget;
using Android.OS;
using Xamarin;
using SpendingTracker;
using System.IO;
using Xamarin.Forms.Platform.Android;
using SpendingTracker;
using System;
using Microsoft.FSharp.Core;


namespace SpendingTracker.Droid
{
	public static class ToFSharpFuncConverterExtensions
	{
		private static readonly Unit Unit = (Unit)Activator.CreateInstance(typeof(Unit), true);

		public static Func<T, Unit> ToFunc<T>(this Action<T> action)
		{
			return x => { action(x); return Unit; };
		}

		public static FSharpFunc<T, Unit> ToFSharpFunc<T>(this Action<T> action)
		{
			return FSharpFunc<T, Unit>.FromConverter(new Converter<T, Unit>(action.ToFunc()));
		}
	}

	[Activity(Label = "SpendingTracker.Droid", Icon = "@mipmap/icon", MainLauncher = true)]
	public class MainActivity : FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			ToolbarResource = Resource.Layout.toolbar;
			TabLayoutResource = Resource.Layout.tabs;

			base.OnCreate(savedInstanceState);
			Xamarin.Forms.Forms.Init(this, savedInstanceState);

			var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			Action<string> exporter = null;
			//= dbPath =>
			//{
			//using (var output = File.Create())
			//{
			//	using (FileStream fs = File.OpenRead(dbPath))
			//	{
			//		while (fs.CanRead)
			//		{
			//			output.WriteByte((byte)fs.ReadByte());
			//		}
			//		fs.Close();
			//	}
			//	output.Close();
			//}				
			//};
			//var result = exporter.ToFSharpFunc();


			//LoadApplication(new FormsApp(path, new SQLitePCL.SQLite3Provider_e_sqlite3(), null));
		}

	}
}

