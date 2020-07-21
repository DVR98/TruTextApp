using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TruTextApp
{
    class EmployeeCl : INotifyPropertyChanged
    {
        private int _empID;
        public int EmpID
        {
            get { return this._empID; }
            set
            {
                this._empID = value;
                this.OnPropertyChanged(nameof(EmpID));
            }
        }


        private string _empFirstname;
        public string EmpFirstname
        {
            get { return this._empFirstname; }
            set
            {
                this._empFirstname = value;
                this.OnPropertyChanged(nameof(EmpFirstname));
            }
        }

        private string _empSurname;
        public string EmpSurname
        {
            get { return this._empSurname; }
            set
            {
                this._empSurname = value;
                this.OnPropertyChanged(nameof(EmpSurname));
            }
        }

        private int _roleID;
        public int RoleID
        {
            get { return this._roleID; }
            set
            {
                this._roleID = value;
                this.OnPropertyChanged(nameof(RoleID));
            }
        }

        private string _email;
        public string Email
        {
            get { return this._email; }
            set
            {
                this._email = value;
                this.OnPropertyChanged(nameof(Email));
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

