using POP_SF7;
using POP_SF7.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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

        #region Database operations

        public static School LoadSchool()
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                DataSet dataSet = new DataSet();

                SqlCommand selectSchoolCommand = connection.CreateCommand();
                selectSchoolCommand.CommandText = @"Select * From School;";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectSchoolCommand);

                try
                {
                    dataAdapter.Fill(dataSet, "School");

                    DataRow row = dataSet.Tables["School"].Rows[0];

                    School school = new School();
                    school.IdentificationNumber = (string)row["School_IdentificationNumber"];
                    school.Name = (string)row["School_Name"];
                    school.Address = (string)row["School_Address"];
                    school.PhoneNumber = (string)row["School_PhoneNumber"];
                    school.Email = (string)row["School_Email"];
                    school.WebSite = (string)row["School_WebSite"];
                    school.Pib = (string)row["School_Pib"];
                    school.AccountNumber = (string)row["School_AccountNumber"];

                    return school;
                }
                catch(SqlException e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                return null;
            }
        }

        public static void UpdateSchool(School school)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                SqlCommand updateCommand = connection.CreateCommand();
                updateCommand.CommandText = @"Update School Set School_Name=@Name, School_Address=@Address, School_PhoneNumber=@PhoneNumber, School_Email=@Email, School_WebSite=@WebSite, School_Pib=@Pib, School_IdentificationNumber=@IdentificationNumber, School_AccountNumber=@AccountNumber Where School_Id=1;";

                updateCommand.Parameters.Add(new SqlParameter("@Name", school.Name));
                updateCommand.Parameters.Add(new SqlParameter("@Address", school.Address));
                updateCommand.Parameters.Add(new SqlParameter("@PhoneNumber", school.PhoneNumber));
                updateCommand.Parameters.Add(new SqlParameter("@Email", school.Email));
                updateCommand.Parameters.Add(new SqlParameter("@WebSite", school.WebSite));
                updateCommand.Parameters.Add(new SqlParameter("@Pib", school.Pib));
                updateCommand.Parameters.Add(new SqlParameter("@IdentificationNumber", school.IdentificationNumber));
                updateCommand.Parameters.Add(new SqlParameter("@AccountNumber", school.AccountNumber));

                try
                {
                    updateCommand.ExecuteNonQuery();
                }
                catch(SqlException e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                catch(InvalidOperationException a)
                {
                    Console.WriteLine(a.StackTrace);
                }
            }
        }

        #endregion

        #region IDataErrorInfo

        public string Error
        {
            get
            {
                return "";
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
                        PhoneNumberValidationRule validator0 = new PhoneNumberValidationRule();
                        if (validator0.Validate(PhoneNumber, null) != ValidationResult.ValidResult) return "Neispravan format broja telefona!";
                        break;
                    case "Email":
                        EMailValidationRule validator = new EMailValidationRule();
                        if (validator.Validate(Email, null) != ValidationResult.ValidResult) return "Neispravan format e-mail adrese";
                        break;
                    case "WebSite":
                        if (string.IsNullOrEmpty(WebSite)) return "Morate da popunite website!";
                        break;
                    case "Pib":
                        bool isNumeric = Pib.All(char.IsDigit);
                        if (!isNumeric) return "Pib mora da se napise u numerickom formatu!";
                        else if (Pib.Length != 9) return "Pib mora da sadrzi 9 cifara!";
                        break;
                    case "IdentificationNumber":
                        bool isNumeric1 = IdentificationNumber.All(char.IsDigit);
                        if (!isNumeric1) return "Maticni broj mora da se napise u numerickom formatu!";
                        else if (IdentificationNumber.Length != 8) return "Maticni broj mora da sadrzi 8 cifara!";
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
