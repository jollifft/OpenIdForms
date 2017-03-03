using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OpenIdForms
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		public BaseViewModel()
		{

		}

		private bool _isBusy;

		public bool IsBusy
		{
			get { return _isBusy; }
			set
			{
				_isBusy = value;
				OnPropertyChanged();
			}
		}

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Raises the property changed.
		/// </summary>
		/// <param name="propertyName">Property name.</param>
		public void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion
	}
}
