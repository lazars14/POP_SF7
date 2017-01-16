using POP_SF7.Helpers;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace POP_SF7
{
    public enum Role { ADMINISTRATOR, EMPLOYEE }

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

        public User()
        {
            Jmbg = "1234567890123";
        }

        // za logged user-a, koristim za logovanje
        public User(int id)
        {
            Id = id;
            FirstName = "Load";
            LastName = "Fail";
            Jmbg = "0000000000000";
            UserName = "random";
            Password = "random";
        }

        public User(int id, string firstName, string lastName, string jmbg, string personAddress, string userName, string password, Role userRole, bool deleted) : base(id, firstName, lastName, jmbg, personAddress, deleted)
        {
            UserName = userName;
            Password = password;
            UserRole = userRole;
        }

        public override string ToString()
        {
            string delimiter = "|";
            return Id + delimiter + FirstName + delimiter + LastName + delimiter + Jmbg + delimiter + UserName + delimiter + Password;
        }

        #region IDataErrorInfo

        override public string Error
        {
            get
            {
                return "";
            }
        }

        override public string this[string columnName]
        {
            get
            {
                switch (columnName)
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
                        else if (!ValidationHelper.containExact(Jmbg, 13)) return ValidationHelper.returnMessageExactLength(13);
                        break;
                    case "Address":
                        if (ValidationHelper.EmptyField(Address)) return ValidationHelper.Empty;
                        else if (ValidationHelper.BiggerThanMaxLength(Address, 50)) return ValidationHelper.returnMessageMaxLength(50);
                        break;
                    case "UserName":
                        if (ValidationHelper.EmptyField(UserName)) return ValidationHelper.Empty;
                        else if (ValidationHelper.BiggerThanMaxLength(UserName, 20)) return ValidationHelper.returnMessageMaxLength(20);
                        break;
                    case "Password":
                        if (ValidationHelper.EmptyField(Password)) return ValidationHelper.Empty;
                        else if (ValidationHelper.BiggerThanMaxLength(Password, 20)) return ValidationHelper.returnMessageMaxLength(20);
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
