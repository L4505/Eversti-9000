using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Eversti.Models;
using SQLite;
using Xamarin.Forms;

namespace Eversti.DatabaseServices
{
    /// <summary>
    /// A simple CRUD class to handle the database operations
    /// </summary>
    public class DatabaseService
    {
        private SQLiteConnection database;
        private readonly object collisionLock = new object();
        private CultureInfo Culture = new CultureInfo("fi-FI");

        /// <summary>
        /// Collection to store all the Items from the database.
        /// </summary>
        public ObservableCollection<Item> Items { get; set; }

        /// <summary>
        /// Connects to the SQLite database, creates the table, populates Items
        /// </summary>
        /// <remarks>
        /// <see cref="Items"/>
        /// </remarks>
        public DatabaseService()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Item>();
            ResetItems();            
        }

        public void AddItem()
        {
            var item = new Item
            {
               
                Date = DateTime.Now.ToString("g",
                  CultureInfo.CreateSpecificCulture("fi-FI"))
            };
            Items.Add(item);
            SaveItem(Items.Last());
        }

        public void SaveItem(Item i)
        {
            lock (collisionLock)
            {
                if (i.Id != 0) database.Update(i);
                else database.Insert(i);
            }
        }

        public void DeleteItem()
        {
            if (Items.Count <= 1) return;
            var lastItem = Items.Last();
            database.Delete<Item>(lastItem.Id);
            Items.Remove(lastItem);
        }

        public void DeleteAll()
        {
            lock (collisionLock)
            {
                database.DropTable<Item>();
                database.CreateTable<Item>();
            }
            ResetItems();            
        }

        private void ResetItems()
        {
            // Populate the collection from database
            Items = new ObservableCollection<Item>(database.Table<Item>());
            // If database is not empty -> return
            if (database.Table<Item>().Any()) return;
            // Add default row
            var item = new Item
            {
                Date = "Aloita jo tänään!"
            };
            Items.Add(item);
            SaveItem(Items.Last());
            
        }
        public void MultiAdd(int u)
        {
            //lock (collisionLock)
            //{
            //    database.DropTable<Item>();
            //    database.CreateTable<Item>();
            //}
            //ResetItems();
            for (int i = 0; i < u; i++)
            {
                AddItem();
            }
            
        }
    }
}

