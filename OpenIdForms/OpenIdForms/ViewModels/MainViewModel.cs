using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OpenIdForms
{
	public class MainViewModel : BaseViewModel
	{
		public MainViewModel()
		{
			LoginCommand = new Command(async () => await LaunchLoginView());
		}

		public ICommand LoginCommand
		{
			get; private set;
		}

		public async Task LaunchLoginView()
		{
			//await App.Current.MainPage.DisplayAlert("Action", "You clicked the button!", "cool");
			await App.OpenIdService.GetAuthorizationCode();

		}
	}
}
