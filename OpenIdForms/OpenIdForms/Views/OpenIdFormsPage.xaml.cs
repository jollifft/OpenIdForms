using Xamarin.Forms;

namespace OpenIdForms
{
	public partial class OpenIdFormsPage : ContentPage
	{
		MainViewModel viewModel;
		public OpenIdFormsPage()
		{
			InitializeComponent();
			BindingContext = viewModel = new MainViewModel();
		}
	}
}
