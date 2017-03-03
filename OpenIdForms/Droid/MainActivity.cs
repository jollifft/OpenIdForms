using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using OpenId.AppAuth;
using Android.Graphics;
using OpenId.AppAuth.Browser;
using System.Threading.Tasks;

namespace OpenIdForms.Droid
{
	[Activity(Label = "OpenIdForms.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		public static MainActivity CurrentActivity { get; set; }

		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			App.Init(new AndroidAppSetup());

			LoadApplication(new App());

			CurrentActivity = this;
		}
	}
}
