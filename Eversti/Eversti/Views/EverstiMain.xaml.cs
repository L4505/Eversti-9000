using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Eversti.ViewModels;

namespace Eversti
{    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EverstiMain : ContentPage
    {
        private MainViewModel ViewModel;

        public EverstiMain()
        {
            ViewModel = new MainViewModel();
            BindingContext = ViewModel;
            InitializeComponent();
            
        }

        public void AddItem() => ViewModel.AddItem();

        public void DeleteItem() => ViewModel.DeleteItem();

        public void DeleteAll()
        {
            ViewModel.DeleteAll();
            //ViewModel = new MainViewModel();
            //BindingContext = ViewModel;
        }
    }
}