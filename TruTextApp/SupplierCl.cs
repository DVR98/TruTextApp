using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TruTextApp
{
    public class SupplierCl : INotifyPropertyChanged
    {
        private int _supID;
        public int SupID
        {
            get { return this._supID; }
            set
            {
                this._supID = value;
                this.OnPropertyChanged(nameof(SupID));
            }
        }


        private string _supName;
        public string SupName
        {
            get { return this._supName; }
            set
            {
                this._supName = value;
                this.OnPropertyChanged(nameof(SupName));
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