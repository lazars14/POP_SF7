using System;
using System.ComponentModel;

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
                throw new NotImplementedException();
            }
        }

        public virtual string this[string columnName]
        {
            get
            {
                switch(columnName)
                {
                    case "FirstName":
                        if (string.IsNullOrEmpty(FirstName)) return "Morate da popunite ime!";
                        break;
                    case "LastName":
                        if (string.IsNullOrEmpty(LastName)) return "Morate da popunite prezime!";
                        break;
                    case "Jmbg":
                        int test;
                        bool isNumeric = int.TryParse(Jmbg, out test);
                        if (!isNumeric) return "Jmbg mora da se napise u numerickom formatu!";
                        else if (Jmbg.Length != 13) return "Jmbg mora da sadrzi 13 cifara!";
                        break;
                    case "Address":
                        if (string.IsNullOrEmpty(Address)) return "Morate da popunite adresu!";
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
