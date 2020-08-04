using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TruTextApp
{
    public class SaleCl : INotifyPropertyChanged
    {
        public int _saleID;
        public int SalesID
        {
            get { return this._saleID; }
            set
            {
                this._saleID = value;
                this.OnPropertyChanged(nameof(SalesID));
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

        public int _custID;
        public int CustID
        {
            get { return this._custID; }
            set
            {
                this._custID = value;
                this.OnPropertyChanged(nameof(CustID));
            }
        }

        public string _date;
        public string Date
        {
            get { return this._date; }
            set
            {
                this._date= value;
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
