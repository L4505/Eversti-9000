using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Eversti.DatabaseServices;
using Eversti.Models;
using Xamarin.Forms;

namespace Eversti.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private DatabaseService database;

        //private ObservableCollection<Item> latestItem;
        //public ObservableCollection<Item> LatestItem
        //{
        //    get { return latestItem; }
        //    set { SetProperty(ref latestItem, value); }
        //}

        //private ObservableCollection<Item> itemList;
        //public ObservableCollection<Item> ItemList
        //{
        //    get { return itemList; }
        //    set { SetProperty(ref itemList, value); }
        //}

        private ObservableCollection<Item> _displayItem;
        public ObservableCollection<Item> DisplayItem
        {
            get { return _displayItem; }
            set
            {                
                    _displayItem = value;
                    this.OnPropertyChanged(nameof(DisplayItem));               
            }
        }

        private ObservableCollection<Item> itemList;
        public ObservableCollection<Item> ItemList
        {
            get { return itemList; }
            set
            {
                itemList = value;
                OnPropertyChanged(nameof(ItemList));
            }
        }
        public string Test { get; set; }

        public MainViewModel()
        {
            database = new DatabaseService();
            ItemList = database.Items;
            DisplayItem = database.DisplayItem;
            SetDisplayItem();
            Test = "Terveisiä viewmodelista";            
        }

        public void SetDisplayItem()
        {
            DisplayItem.Clear();
            List<Item> temp = ItemList.ToList();

            this.DisplayItem.Add(new Item
            {
                Id = this.ItemList.Last().Id,
                Date = this.ItemList.Last().Date
            });

        }

        public void AddItem()
        {
            database.AddItem();
            SetDisplayItem();
        }

        public void DeleteItem()
        {
            database.DeleteItem();
            // SetLatest();
        }

        public void DeleteAll()
        {
            database.DeleteAll();
            // SetLatest();
        }

        public void SetLatest()
        {
            

        }

        //get {

        //        var temp = ItemList.LastOrDefault();
        //        if (temp.Id != 0)
        //        {
        //            return $"Kannassa: {temp.Id.ToString()}";
        //        }
        //        return "Tyhjältä näyttää!";
        //    }

        //public DatabaseService Database { get => database; set => database = value; }
        

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }






    }
}
