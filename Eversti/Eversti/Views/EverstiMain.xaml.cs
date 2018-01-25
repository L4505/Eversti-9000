using Eversti.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eversti
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EverstiMain : ContentPage
    {
        private MainViewModel ViewModel;

        public EverstiMain()
        {
            InitializeComponent();
            // Bring forth the glorious viewmodel
            ViewModel = new MainViewModel();
            // Set the glorious viewmodel as the views (xaml) data bindingcontext
            BindingContext = ViewModel;
        }
        /// Methods for the buttons Clicked="" in xaml    
        //  *Future dev note - these methods could maybe be done in viewmodel with 'Command'
        //
        public void AddItem() => ViewModel.AddItem();

        public void DeleteItem() => ViewModel.DeleteItem();

        async void DeleteAll(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Haluat poistaa kaikki merkinnät?!", "Tätä ei voi perua! Haluatko silti poistaa kaiken?", "Kyllä", "Ei");
            if (answer)
            {
                ViewModel.DeleteAll();
            }            
        }
        
    

    public async void ListAll(object o, EventArgs e) => await Navigation.PushModalAsync(new ListViewPage());
}
}