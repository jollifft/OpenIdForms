
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
		private AuthorizationService authService;
		private JSONObject userInfoJson;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			authService = new AuthorizationService(this);

			if (savedInstanceState != null)
			{
				if (savedInstanceState.ContainsKey(Constants.KEY_AUTH_STATE))
				{
					try
					{
						authState = AuthState.JsonDeserialize(savedInstanceState.GetString(Constants.KEY_AUTH_STATE));
					}
					catch (JSONException ex)
					{
						Console.WriteLine("Malformed authorization JSON saved: " + ex);
					}
				}

				if (savedInstanceState.ContainsKey(Constants.KEY_USER_INFO))
				{
					try
					{
						userInfoJson = new JSONObject(savedInstanceState.GetString(Constants.KEY_USER_INFO));
					}
					catch (JSONException ex)
					{
						Console.WriteLine("Failed to parse saved user info JSON: " + ex);
					}
				}
			}

			if (authState == null)
			{
				authState = GetAuthStateFromIntent(Intent);
				AuthorizationResponse response = AuthorizationResponse.FromIntent(Intent);
				AuthorizationException ex = AuthorizationException.FromIntent(Intent);
				authState.Update(response, ex);

				if (response != null)
				{
					Console.WriteLine("Received AuthorizationResponse.");
					PerformTokenRequest(response.CreateTokenExchangeRequest());
				}
				else
				{
					Console.WriteLine("Authorization failed: " + ex);
				}
			}

			//var authState = GetAuthStateFromIntent(Intent);
			//AuthorizationResponse response = AuthorizationResponse.FromIntent(Intent);
			//AuthorizationException ex = AuthorizationException.FromIntent(Intent);
			//authState.Update(response, ex);

			Finish();
		}

		protected override void OnSaveInstanceState(Bundle outState)
		{
			if (authState != null)
			{
				outState.PutString(Constants.KEY_AUTH_STATE, authState.JsonSerializeString());
			}

			if (userInfoJson != null)
			{
				outState.PutString(Constants.KEY_USER_INFO, userInfoJson.ToString());
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			authService.Dispose();
		}

		private void PerformTokenRequest(TokenRequest request)
		{
			IClientAuthentication clientAuthentication;
			try
			{
				clientAuthentication = authState.ClientAuthentication;
			}
			catch (ClientAuthenticationUnsupportedAuthenticationMethod ex)
			{
				Console.WriteLine("Token request cannot be made, client authentication for the token endpoint could not be constructed: " + ex);
				return;
			}

			authService.PerformTokenRequest(request, ReceivedTokenResponse);
		}

		private void ReceivedTokenResponse(TokenResponse tokenResponse, AuthorizationException authException)
		{
			Console.WriteLine("Token request complete");
			authState.Update(tokenResponse, authException);
			//RefreshUi();
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
