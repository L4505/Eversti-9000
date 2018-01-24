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
            InitializeComponent();
            ViewModel = new MainViewModel();
            BindingContext = ViewModel;
        }

        public void AddItem() => ViewModel.AddItem();

        public void DeleteItem() => ViewModel.DeleteItem();

        public void DeleteAll() => ViewModel.DeleteAll();
    }
}