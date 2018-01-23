using System;
using System.Collections.Generic;
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

        //private ObservableCollection<Item> HiddenItems { get; set; }
        //public ObservableCollection<Item> Items
        //{
        //    get
        //    {
        //        return HiddenItems;
        //    }

        //    set
        //    {
        //        HiddenItems = Items;
        //    }
        //}

        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<Item> DisplayItem { get; set; }

        public DatabaseService()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Item>();
            this.Items = new ObservableCollection<Item>(database.Table<Item>());
            
            if (!database.Table<Item>().Any())
            {
                this.Items.Add(new Item { Date = "foo" });
            }
            this.DisplayItem = new ObservableCollection<Item>(Items);

        }

        //public ObservableCollection<Item> GetItems()
        //{

        //    return this.Items;
        //}
       

        public void AddItem()
        {
            dTime = DateTime.Now;
            this.Items.Add(new Item
            {
                Date = dTime.ToShortDateString() //dTime.ToShortDateString() 
            });
            //SetDisplayItem();
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

        //        var itemInstance = this.Items;
        //            {
        //                if (itemInstance.Id != 0)
        //                {
        //                    lock (collisionLock)
        //                    {
        //                        database.Update(itemInstance);
        //                    }
        //}
        //                else
        //                {
        //                    lock (collisionLock)
        //                    {
        //                        database.Insert(itemInstance);
        //                    }                    
        //                }               
        //            }
        //            return Items;
        //        }

        public void DeleteItem()
        {
            var i = Items.Last();
            int lastId = i.Id;
            if (lastId != 0 && lastId > 0)
            {
                database.Delete<Item>(lastId);
               
                //return Items;
            }
            if (lastId == 0)
            {

            }
            // return Items;


            //database.Delete<Item>(database.Query<Item>("SELECT last_insert_rowid()"));
        }

        //public void DeleteOneItem()
        //{
        //    int i = ItemId; 
        //    if (i != 0)
        //    {
        //        lock (collisionLock)
        //        {
        //            database.Delete<Item>(i);
        //        }
        //    }
        //    GetId();

        //}

        public void DeleteAll()
        {
            lock (collisionLock)
            {
                this.database.DropTable<Item>();
                this.database.CreateTable<Item>();
            }

            this.Items = new ObservableCollection<Item>
              (database.Table<Item>());

            //return Items;
        }
    }
}

