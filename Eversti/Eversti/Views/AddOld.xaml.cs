using Eversti.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eversti
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddOld : ContentPage
	{
        private MainViewModel ViewModel;
		public AddOld ()
		{
			InitializeComponent ();
            ViewModel = new MainViewModel();
            BindingContext = ViewModel;
		}
        void Entry_Completed(object sender, EventArgs e)
        {
            var text = ((Entry)sender).Text;
            if (ViewModel.AddOldTo(text))
            {
               DisplayAlert("Onnistumisen tunteita!", "Merkinnät lisättiin.", "OK");
            }
            else
            {
                DisplayAlert("I'm afraid I can't do that Dave.", "Tähän kelpaavat vain numerot", "OK");
            }

        }
        
    }
}