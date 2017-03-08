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
			TestCommand = new Command(async() => await TestMethod());
		}

		public ICommand LoginCommand
		{
			get; private set;
		}

		public ICommand TestCommand
		{
			get; private set;
		}

		private string _tokenText;
		public string TokenText
		{
			get 
			{
				return _tokenText;
			}
			set 
			{
				_tokenText = value;
				OnPropertyChanged();
			}
		}

		public async Task LaunchLoginView()
		{
			//await App.Current.MainPage.DisplayAlert("Action", "You clicked the button!", "cool");
			await App.OpenIdService.GetAuthorizationCode();

		}

		public async Task TestMethod()
		{
			//App.OpenIdService.DoTaskWithFreshTokens((accessToken) => 
			//{
			//	//set up httpclient

			//	//attach accessToken

			//	//make call 

			//	//return result
			//	return false;
			//});

			TokenText = await App.OpenIdService.GetActiveAccessToken();
		}

		//public bool MakeHttpCall(string accessToken)
		//{
		//	//set up httpclient

		//	//attach accessToken

		//	//make call 

		//	//return result
		//	return false;
		//}
	}
}
