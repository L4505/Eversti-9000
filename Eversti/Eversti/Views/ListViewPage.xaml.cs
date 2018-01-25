using Eversti.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eversti
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListViewPage : ContentPage
	{
        private MainViewModel ViewModel;
		public ListViewPage ()
		{
            InitializeComponent();
            // Bring forth the glorious viewmodel
            ViewModel = new MainViewModel();
            // Set the glorious viewmodel as the views (xaml) data bindingcontext
            BindingContext = ViewModel;
        }
	}
}