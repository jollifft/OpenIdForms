using Xamarin.Forms;

namespace OpenIdForms
{
	public partial class App : Application
	{
		public static IOpenIdService OpenIdService { get; set; }

		public App()
		{
			InitializeComponent();

			MainPage = new OpenIdFormsPage();
		}

		public static void Init(AppSetup appSetup)
		{
			appSetup.CreateContainer();
			OpenIdService = DependencyResolver.Get<IOpenIdService>();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
