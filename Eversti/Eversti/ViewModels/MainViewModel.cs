using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Eversti.DatabaseServices;
using Eversti.Models;
using Xamarin.Forms;

namespace Eversti.ViewModels
{
    /// <summary>
    /// Viewmodel class that is in between the view <seealso cref="EverstiMain"/> 
    /// and <seealso cref="DatabaseService"/> handling the collections and passing input from 
    /// the view to do database operations
    /// </summary>
    class MainViewModel : INotifyPropertyChanged
    {
        private DatabaseService database;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>DisplayItem property gets all the items from the database</summary>
        public ObservableCollection<Item> DisplayItem => database.Items;        

        /// <summary>The LatestItem property</summary>
        /// <value>The LatestItem property gets it value from the last item of the collection Items</value>
        public int LatestItem => database.Items.Count()-1;

        public string Date => database.Items.Last().Date;

        // Instantiate the database helper
        public MainViewModel()
        {
            database = new DatabaseService();            
        }

        public void AddItem()
        {
            database.AddItem();
            Update();
        }

        public void DeleteItem()
        {
            database.DeleteItem();
            Update();
        }

        public void DeleteAll()
        {
            database.DeleteAll();
            Update();
        }

        public bool AddOldTo(string s)
        {
            
            if (int.TryParse(s, out int t))
            {
                database.MultiAdd(t);
               
                Update();
                return true;
            }
            return false;            
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }

        private void Update()
        {
            OnPropertyChanged(nameof(DisplayItem));
            OnPropertyChanged(nameof(LatestItem));
            OnPropertyChanged(nameof(Date));
        }
    }
}
