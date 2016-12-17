using System;
using System.ComponentModel;

namespace POP_SF7
{
    public enum Role { Administrator, Employee }

    public class User : Person, INotifyPropertyChanged, ICloneable, IDataErrorInfo
    {
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged("UserName"); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged("Password"); }
        }

        private Role userRole;
        public Role UserRole
        {
            get { return userRole; }
            set { userRole = value; OnPropertyChanged("UserRole"); }
        }

        public User() { }

        public User(int id, string firstName, string lastName, string jmbg, string personAddress, string userName, string password, Role userRole, bool deleted) : base(id, firstName, lastName, jmbg, personAddress, deleted)
        {
            UserName = userName;
            Password = password;
            UserRole = userRole;
        }

        #region IDataErrorInfo

        override public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        override public string this[string columnName]
        {
            get
            {
                switch (columnName)
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
                    case "UserName":
                        if (string.IsNullOrEmpty(UserName)) return "Morate da popunite korisnicko ime!";
                        break;
                    case "Password":
                        if (string.IsNullOrEmpty(Password)) return "Morate da popunite lozinku!";
                        break;
                }
                return "";
            }
        }

        #endregion

        #region INotifyPropertyChanged

        override public event PropertyChangedEventHandler PropertyChanged;

        override protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

        #region ICloneable

        public object Clone()
        {
            User userCopy = new User();
            userCopy.Id = Id;
            userCopy.FirstName = FirstName;
            userCopy.LastName = LastName;
            userCopy.Jmbg = Jmbg;
            userCopy.Address = Address;
            userCopy.Deleted = Deleted;
            userCopy.UserName = UserName;
            userCopy.Password = Password;
            userCopy.UserRole = UserRole;

            return userCopy;
        }

        #endregion

    }
}
