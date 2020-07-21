using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TruTextApp
{
    public class CustomersCl : INotifyPropertyChanged
    {
        public int _customerID;
        public int CustomerID
        {
            get { return this._customerID; }
            set
            {
                this._customerID = value;
                this.OnPropertyChanged(nameof(CustomerID));
            }
        }

        public string _firstname;
        public string Firstname
        {
            get { return this._firstname; }
            set
            {
                this._firstname = value;
                this.OnPropertyChanged(nameof(Firstname));
            }
        }

        public string _surname;
        public string Surname
        {
            get { return this._surname; }
            set
            {
                this._surname = value;
                this.OnPropertyChanged(nameof(Surname));
            }
        }

        public string _address;
        public string Address
        {
            get { return this._address; }
            set
            {
                this._address = value;
                this.OnPropertyChanged(nameof(Address));
            }
        }

        public string _city;
        public string City
        {
            get { return this._city; }
            set
            {
                this._city = value;
                this.OnPropertyChanged(nameof(City));
            }
        }

        public string _cellnumber;
        public string Cellnumber
        {
            get { return this._cellnumber; }
            set
            {
                this._cellnumber = value;
                this.OnPropertyChanged(nameof(Cellnumber));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
