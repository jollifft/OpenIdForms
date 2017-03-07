
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OpenId.AppAuth;
using Org.Json;

namespace OpenIdForms.Droid
{
	[Activity(Label = "RedirectActivity", MainLauncher = false, Theme = "@style/MyTheme")]
	public class RedirectActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		private AuthState authState;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			if (authState == null)
			{
				authState = GetAuthStateFromIntent(Intent);
				AuthorizationResponse response = AuthorizationResponse.FromIntent(Intent);
				AuthorizationException ex = AuthorizationException.FromIntent(Intent);
				authState.Update(response, ex);

				if (response != null)
				{
					Console.WriteLine("Received AuthorizationResponse.");
					App.OpenIdService.PerformTokenRequest(authState.JsonSerializeString());
				}
				else
				{
					Console.WriteLine("Authorization failed: " + ex);
				}
			}

			Finish();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			authState.Dispose();
		}

		private static AuthState GetAuthStateFromIntent(Intent intent)
		{
			if (!intent.HasExtra(Constants.EXTRA_AUTH_STATE))
			{
				throw new InvalidOperationException("The AuthState instance is missing in the intent.");
			}

			try
			{
				return AuthState.JsonDeserialize(intent.GetStringExtra(Constants.EXTRA_AUTH_STATE));
			}
			catch (JSONException ex)
			{
				Console.WriteLine("Malformed AuthState JSON saved: " + ex);
				throw new InvalidOperationException("The AuthState instance is missing in the intent.", ex);
			}
		}
	}



}
