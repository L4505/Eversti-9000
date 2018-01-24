using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Eversti.Models;
using SQLite;
using Xamarin.Forms;

namespace Eversti.DatabaseServices
{
    public class DatabaseService
    {
        private SQLiteConnection database;
        private DateTime dTime;
        private object collisionLock = new object();
        /// <summary>
        /// Collection to store all the Items from the database.
        /// </summary>
        public ObservableCollection<Item> Items { get; set; }
        /// <summary>
        /// Collection to store the latest item from the Items collection.
        /// </summary>
        public ObservableCollection<Item> DisplayItem { get; set; }

        public DatabaseService()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Item>();
            this.Items = new ObservableCollection<Item>(database.Table<Item>());
            // If the database is empty, add one default entry to the collection Items
            if (!database.Table<Item>().Any())
            {
                this.Items.Add(new Item { Date = "Aloita jo tänään!" });
            }
            // Create the default entry to also to the DisplayItem collection
            this.DisplayItem = new ObservableCollection<Item>(Items);
        }
        /// <summary>
        /// 
        /// </summary>
        public void AddItem()
        {
            dTime = DateTime.Now;
            this.Items.Add(new Item
            {
                Date = dTime.ToShortDateString() 
            });
            
            SaveItem(this.Items.Last());
        }

        public void SaveItem(Item i)
        {
            lock (collisionLock)
            {
                if (i.Id != 0)
                {
                    database.Update(i);
                }
                else
                {
                    database.Insert(i);
                }
            }
        }
        
        public void DeleteItem()
        {
            var i = Items.Last();
            int lastId = i.Id;
            if (lastId != 0 && lastId > 0)
            {
                database.Delete<Item>(lastId);
                Items.Remove(i);
            }
            if (lastId == 0)
            {
                // displayalert Tyhjää täynnä
            }
        }

        public void DeleteAll()
        {
            lock (collisionLock)
            {
                this.database.DropTable<Item>();
                this.database.CreateTable<Item>();
            }

            this.Items = new ObservableCollection<Item>
              (database.Table<Item>());
            Items.Add(new Item { Date = "Aloita jo Tänään!" });
        }
    }
}

