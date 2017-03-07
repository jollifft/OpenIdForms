using System;
using System.Threading.Tasks;
using Foundation;
using OpenId.AppAuth;
using UIKit;

namespace OpenIdForms.iOS
{
	public class OpenIdService : IOpenIdService
	{
		// The OIDC issuer from which the configuration will be discovered.
		public const string kIssuer = @"https://ssotest.kochind.com/";

		// The OAuth client ID.
		public const string kClientID = "mobilessotest";

		// The OAuth redirect URI for the client kClientID.
		public const string kRedirectURI = "com.kbsmad.ssotest.mobilessotest://oauth";

		// NSCoding key for the authState property.
		public static NSString kAppAuthExampleAuthStateKey = (NSString)"authState";

		private AuthState _authState;
		public AuthState AuthState
		{
			get { return _authState; }
			set
			{
				if (_authState != value)
				{
					_authState = value;
				}
			}
		}

		public OpenIdService()
		{
		}

		public async Task GetAuthorizationCode()
		{
			var issuer = new NSUrl(kIssuer);
			var redirectURI = new NSUrl(kRedirectURI);

			Console.WriteLine($"Fetching configuration for issuer: {issuer}");

			try
			{
				// discovers endpoints
				var configuration = await AuthorizationService.DiscoverServiceConfigurationForIssuerAsync(issuer);

				Console.WriteLine($"Got configuration: {configuration}");

				string codeMethod = @"plain";
				string code_verifier = AuthorizationRequest.GenerateCodeVerifier();
				string state = AuthorizationRequest.GenerateState();

				// builds authentication request
				AuthorizationRequest request = new AuthorizationRequest(configuration, kClientID, null, Scope.OpenId, redirectURI, ResponseType.Code, state, code_verifier, code_verifier, codeMethod, null);

				// performs authentication request
				var appDelegate = (AppDelegate)UIApplication.SharedApplication.Delegate;
				Console.WriteLine($"Initiating authorization request with scope: {request.Scope}");

				appDelegate.CurrentAuthorizationFlow = AuthState.PresentAuthorizationRequest(request, UIApplication.SharedApplication.KeyWindow.RootViewController, (authState, error) =>
				{
					if (authState != null)
					{
						AuthState = authState;
						Console.WriteLine($"Got authorization tokens. Access token: {authState.LastTokenResponse.AccessToken}");
					}
					else {
						Console.WriteLine($"Authorization error: {error.LocalizedDescription}");
						AuthState = null;
					}
				});
			}
			catch (Exception ex)
			{

				Console.WriteLine($"Error retrieving discovery document: {ex}");
				AuthState = null;
			}
		}
	}
}
