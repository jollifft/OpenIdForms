using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using OpenId.AppAuth;
using UIKit;

namespace OpenIdForms.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		// The authorization flow session which receives the return URL from SFSafariViewController.
		public IAuthorizationFlowSession CurrentAuthorizationFlow { get; set; }

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			App.Init(new IOSAppSetup());

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}

		// Handles inbound URLs. Checks if the URL matches the redirect URI for a pending
		// AppAuth authorization request.
		public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		{
			// Sends the URL to the current authorization flow (if any) which will process it if it relates to
			// an authorization response.
			if (CurrentAuthorizationFlow?.ResumeAuthorizationFlow(url) == true)
			{
				return true;
			}

			// Your additional URL handling (if any) goes here.

			return false;
		}
	}
}
