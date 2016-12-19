using POP_SF7.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace POP_SF7
{
    public class School : INotifyPropertyChanged, ICloneable, IDataErrorInfo
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged("Address"); }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; OnPropertyChanged("PhoneNumber"); }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged("Email"); }
        }

        private string webSite;
        public string WebSite
        {
            get { return webSite; }
            set { webSite = value; OnPropertyChanged("WebSite"); }
        }

        private string pib;
        public string Pib
        {
            get { return pib; }
            set { pib = value; OnPropertyChanged("Pib"); }
        }

        private string identificationNumber;
        public string IdentificationNumber
        {
            get { return identificationNumber; }
            set { identificationNumber = value; OnPropertyChanged("IdentificationNumber"); }
        }

        private string accountNumber;
        public string AccountNumber
        {
            get { return accountNumber; }
            set { accountNumber = value; OnPropertyChanged("AccountNumber"); }
        }

        public School() { }

        public School(string name, string address, string phoneNumber, string email, string webSite, string PIB, string identificationNumber, string accountNumber)
        {
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            WebSite = webSite;
            Pib = PIB;
            IdentificationNumber = identificationNumber;
            AccountNumber = accountNumber;
        }

        #region DB

        public static School LoadSchool()
        {
            throw new NotImplementedException();
        }

        public static void UpdateSchool()
        {

        }

        #endregion

        #region IDataErrorInfo

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch(columnName)
                {
                    case "Name":
                        if (string.IsNullOrEmpty(Name)) return "Morate da popunite naziv skole!";
                        break;
                    case "Address":
                        if (string.IsNullOrEmpty(Address)) return "Morate da popunite adresu skole!";
                        break;
                    case "PhoneNumber":
                        EMailValidationRule validator0 = new EMailValidationRule();
                        if (validator0.Validate(PhoneNumber, null) != ValidationResult.ValidResult) return "Neispravan format broja telefona!";
                        break;
                    case "Email":
                        EMailValidationRule validator = new EMailValidationRule();
                        if (validator.Validate(Email, null) != ValidationResult.ValidResult) return "Neispravan format e-mail adrese";
                        break;
                    case "WebSite":
                        bool isUri = Uri.IsWellFormedUriString(WebSite, UriKind.RelativeOrAbsolute);
                        if (!isUri) return "Neispravan format adrese web sajta!";
                        break;
                    case "Pib":
                        int test;
                        bool isNumeric = int.TryParse(Pib.ToString(), out test);
                        if (!isNumeric) return "Pib mora da se napise u numerickom formatu!";
                        else if (Pib.ToString().Length != 9) return "Pib mora da sadrzi 9 cifara!";
                        break;
                    case "IdentificationNumber":
                        int test1;
                        bool isNumeric1 = int.TryParse(IdentificationNumber.ToString(), out test1);
                        if (!isNumeric1) return "Maticni broj mora da se napise u numerickom formatu!";
                        else if (Pib.ToString().Length != 8) return "Maticni broj mora da sadrzi 8 cifara!";
                        break;
                    case "AccountNumber":
                        AccountNumberValidationRule validator1 = new AccountNumberValidationRule();
                        if (validator1.Validate(AccountNumber, null) != ValidationResult.ValidResult) return "Neispravan format racuna! xxx-xxxxxxxxxxxxx-xx";
                        break;
                }
                return "";
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
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
            School schoolCopy = new School();
            schoolCopy.Name = Name;
            schoolCopy.Address = Address;
            schoolCopy.PhoneNumber = PhoneNumber;
            schoolCopy.Email = Email;
            schoolCopy.WebSite = WebSite;
            schoolCopy.Pib = Pib;
            schoolCopy.IdentificationNumber = IdentificationNumber;
            schoolCopy.AccountNumber = AccountNumber;

            return schoolCopy;
        }

        #endregion
    }
}
