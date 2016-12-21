using POP_SF7.Helpers;
using System;
using System.ComponentModel;
using System.Linq;

namespace POP_SF7
{
    public class Person : INotifyPropertyChanged, IDataErrorInfo
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; OnPropertyChanged("FirstName"); }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; OnPropertyChanged("LastName"); }
        }

        private string jmbg;
        public string Jmbg
        {
            get { return jmbg; }
            set { jmbg = value; OnPropertyChanged("Jmbg"); }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged("Address"); }
        }
        
        private bool deleted;
        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; OnPropertyChanged("Deleted"); }
        }

        public Person() { }

        public Person(int id, string firstName, string lastName, string jmbg, string address, bool deleted)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Jmbg = jmbg;
            Address = address;
            Deleted = deleted;
        }

        #region IDataErrorInfo

        public virtual string Error
        {
            get
            {
                return "";
            }
        }

        public virtual string this[string columnName]
        {
            get
            {
                switch(columnName)
                {
                    case "FirstName":
                        if (ValidationHelper.EmptyField(FirstName)) return ValidationHelper.Empty;
                        else if (ValidationHelper.BiggerThanMaxLength(FirstName, 30)) return ValidationHelper.returnMessageMaxLength(30);
                        break;
                    case "LastName":
                        if (ValidationHelper.EmptyField(LastName)) return ValidationHelper.Empty;
                        else if (ValidationHelper.BiggerThanMaxLength(LastName, 30)) return ValidationHelper.returnMessageMaxLength(30);
                        break;
                    case "Jmbg":
                        bool isNumeric = ValidationHelper.numeric(Jmbg);
                        if (!isNumeric) return ValidationHelper.Numeric;
                        else if (ValidationHelper.containExact(Jmbg, 13)) return ValidationHelper.returnMessageExactLength(13);
                        break;
                    case "Address":
                        if (ValidationHelper.EmptyField(Address)) return ValidationHelper.Empty;
                        else if (ValidationHelper.BiggerThanMaxLength(Address, 50)) return ValidationHelper.returnMessageMaxLength(50);
                        break;
                }
                return "";
            }
        }

        #endregion

        #region INotifyPropertyChanged

        virtual public event PropertyChangedEventHandler PropertyChanged;

        virtual protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

    }
}
