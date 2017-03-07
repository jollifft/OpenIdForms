using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Support.V4.Content;
using Java.Lang;
using OpenId.AppAuth;
using OpenId.AppAuth.Browser;
using Plugin.CurrentActivity;

namespace OpenIdForms.Droid
{
	public class OpenIdService : IOpenIdService
	{
		public AuthState AuthState { get; set; }
		public AuthorizationService AuthService { get; set; }

		private static string DiscoveryEndpoint = "https://ssotest.kochind.com/.well-known/openid-configuration";
		private static string ClientId = "mobilessotest";
		private static string RedirectUri = "com.kbsmad.ssotest.mobilessotest://oauth";

		public OpenIdService()
		{
			AuthState = new AuthState();

		}

		public async Task GetAuthorizationCode()
		{
			AppAuthConfiguration config = new AppAuthConfiguration.Builder()
																.SetBrowserMatcher(new BrowserBlacklist(
																	VersionedBrowserMatcher.SamsungCustomTab, VersionedBrowserMatcher.FirefoxBrowser))
																.Build();
			AuthService = new AuthorizationService(CrossCurrentActivity.Current.Activity, config);
			//AuthState = new AuthState();

			AuthorizationServiceConfiguration serviceConfig = await AuthorizationServiceConfiguration.FetchFromUrlAsync(Android.Net.Uri.Parse(DiscoveryEndpoint));
			string codeVerifier = "d129a4f8-c5c7-439d-b9c1-dd0349faba5cd129a4f8-c5c7-439d-b9c1-dd0349faba5c";

			var authRequest = new AuthorizationRequest.Builder(serviceConfig, ClientId, ResponseTypeValues.Code, Android.Net.Uri.Parse(RedirectUri))
													  .SetScope("openid")
													  .SetCodeVerifier(codeVerifier, codeVerifier, "plain")
													  .Build();

			AuthService.PerformAuthorizationRequest(
				authRequest,
				CreatePostAuthorizationIntent(CrossCurrentActivity.Current.Activity, authRequest, serviceConfig.DiscoveryDoc),
				AuthService.CreateCustomTabsIntentBuilder().SetToolbarColor(Color.ParseColor("#00a8e1")).Build());
		}

		public PendingIntent CreatePostAuthorizationIntent(Context context, AuthorizationRequest request, AuthorizationServiceDiscovery discoveryDoc)
		{
			Intent intent = new Intent(context, typeof(RedirectActivity));
			intent.PutExtra(Constants.EXTRA_AUTH_STATE, AuthState.JsonSerializeString());
			if (discoveryDoc != null)
			{
				intent.PutExtra("authServiceDiscovery", discoveryDoc.DocJson.ToString());
			}

			return PendingIntent.GetActivity(context, request.GetHashCode(), intent, 0);
		}
	}
}
