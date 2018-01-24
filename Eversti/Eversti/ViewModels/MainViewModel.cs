using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Eversti.DatabaseServices;
using Eversti.Models;

namespace Eversti.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private DatabaseService database;

        public ObservableCollection<Item> DisplayItem
        {
            get { return database.Items; }           
        }

        public Item LatestItem
        {
            get { return database.Items.Last(); }
        }

        public MainViewModel()
        {
            database = new DatabaseService();            
        }

        public void AddItem()
        {
            database.AddItem();
            OnPropertyChanged(nameof(DisplayItem));
            OnPropertyChanged(nameof(LatestItem));
        }

        public void DeleteItem()
        {
            database.DeleteItem();
            OnPropertyChanged(nameof(DisplayItem));
            OnPropertyChanged(nameof(LatestItem));
        }

        public void DeleteAll()
        {
            database.DeleteAll();
            OnPropertyChanged(nameof(DisplayItem));
            OnPropertyChanged(nameof(LatestItem));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }
    }
}
