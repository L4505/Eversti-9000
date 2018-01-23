using SQLite;
using System.ComponentModel;

namespace Eversti.Models
{
    [Table("Items")]
    public class Item : INotifyPropertyChanged
    {
        private int _id;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                this._id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        private string _date;
        [NotNull]
        public string Date
        {
            get
            {
                return _date;
            }
            set
            {
                this._date = value;
                OnPropertyChanged(nameof(Date));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }
    }
}