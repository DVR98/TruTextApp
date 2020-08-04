using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TruTextApp
{
    public class RoleCl : INotifyPropertyChanged
    {
        private int _roleId;
        public int RoleId
        {
            get { return this._roleId; }
            set
            {
                this._roleId = value;
                this.OnPropertyChanged(nameof(RoleId));
            }
        }


        private string _rep;
        public string Rep
        {
            get { return this._rep; }
            set
            {
                this._rep = value;
                this.OnPropertyChanged(nameof(Rep));
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
