using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TruTextApp
{
    public class BooksCl : INotifyPropertyChanged
    {
        private int _bookID;
        public int BookID
        {
            get { return this._bookID; }
            set
            {
                this._bookID = value;
                this.OnPropertyChanged(nameof(BookID));
            }
        }


        private string _title;
        public string Title
        {
            get { return this._title; }
            set
            {
                this._title = value;
                this.OnPropertyChanged(nameof(Title));
            }
        }

        private string _author;
        public string Author
        {
            get { return this._author; }
            set
            {
                this._author = value;
                this.OnPropertyChanged(nameof(Author));
            }
        }

        private string _sellingPrice;
        public string SellingPrice
        {
            get { return this._sellingPrice; }
            set
            {
                this._sellingPrice = value;
                this.OnPropertyChanged(nameof(SellingPrice));
            }
        }

        private string _orderPrice;
        public string OrderPrice
        {
            get { return this._orderPrice; }
            set
            {
                this._orderPrice = value;
                this.OnPropertyChanged(nameof(OrderPrice));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
