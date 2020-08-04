using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TruTextApp
{
    public class OrdersCl : INotifyPropertyChanged
    {
        public int _orderID;
        public int OrderID
        {
            get { return this._orderID; }
            set
            {
                this._orderID = value;
                this.OnPropertyChanged(nameof(OrderID));
            }
        }

        public int _empID;
        public int EmpID
        {
            get { return this._empID; }
            set
            {
                this._empID = value;
                this.OnPropertyChanged(nameof(EmpID));
            }
        }

        public int _supID;
        public int SupID
        {
            get { return this._supID; }
            set
            {
                this._supID = value;
                this.OnPropertyChanged(nameof(SupID));
            }
        }

        public string _date;
        public string Date
        {
            get { return this._date; }
            set
            {
                this._date = value;
                this.OnPropertyChanged(nameof(Date));
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
