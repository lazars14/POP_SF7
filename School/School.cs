using POP_SF7.Helpers;
using POP_SF7.Validations;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace POP_SF7
{
    public class SchoolS : INotifyPropertyChanged, ICloneable, IDataErrorInfo
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

        public SchoolS() { }

        public SchoolS(string name, string address, string phoneNumber, string email, string webSite, string PIB, string identificationNumber, string accountNumber)
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

        public static SchoolS LoadSchool()
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                DataSet dataSet = new DataSet();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Select * From School;";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                try
                {
                    dataAdapter.Fill(dataSet, "School");

                    DataRow row = dataSet.Tables["School"].Rows[0];

                    SchoolS school = new SchoolS();
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
                catch (SqlException e)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + e.GetType());
                }
                catch (InvalidOperationException a)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + a.GetType());
                }
                catch (ArgumentException g)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + g.GetType());
                }
                catch (NullReferenceException n)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + n.GetType());
                }
                return null;

            }
        }

        public static bool UpdateSchool(SchoolS school)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                bool valid = false;

                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Update School Set School_Name=@Name, School_Address=@Address, School_PhoneNumber=@PhoneNumber, School_Email=@Email, School_WebSite=@WebSite, School_Pib=@Pib, School_IdentificationNumber=@IdentificationNumber, School_AccountNumber=@AccountNumber Where School_Id=1;";

                try
                {
                    command.Parameters.Add(new SqlParameter("@Name", school.Name));
                    command.Parameters.Add(new SqlParameter("@Address", school.Address));
                    command.Parameters.Add(new SqlParameter("@PhoneNumber", school.PhoneNumber));
                    command.Parameters.Add(new SqlParameter("@Email", school.Email));
                    command.Parameters.Add(new SqlParameter("@WebSite", school.WebSite));
                    command.Parameters.Add(new SqlParameter("@Pib", school.Pib));
                    command.Parameters.Add(new SqlParameter("@IdentificationNumber", school.IdentificationNumber));
                    command.Parameters.Add(new SqlParameter("@AccountNumber", school.AccountNumber));

                    command.ExecuteNonQuery();

                    valid = true;
                }
                catch (SqlException e)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + e.GetType());
                }
                catch (InvalidOperationException a)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + a.GetType());
                }
                catch (ArgumentException g)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + g.GetType());
                }
                catch (NullReferenceException n)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + n.GetType());
                }

                return valid;
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
                        if (ValidationHelper.EmptyField(Name)) return ValidationHelper.Empty;
                        else if (ValidationHelper.BiggerThanMaxLength(Name, 50)) return ValidationHelper.returnMessageMaxLength(50);
                        break;
                    case "Address":
                        if (ValidationHelper.EmptyField(Address)) return ValidationHelper.Empty;
                        else if (ValidationHelper.BiggerThanMaxLength(Address, 100)) return ValidationHelper.returnMessageMaxLength(100);
                        break;
                    case "PhoneNumber":
                        PhoneNumberValidationRule validator0 = new PhoneNumberValidationRule();
                        if (validator0.Validate(PhoneNumber, null) != ValidationResult.ValidResult) return ValidationHelper.Pattern + PhoneNumberValidationRule.CorrectPattern;
                        break;
                    case "Email":
                        EMailValidationRule validator = new EMailValidationRule();
                        if (validator.Validate(Email, null) != ValidationResult.ValidResult) return ValidationHelper.Pattern + EMailValidationRule.CorrectPattern;
                        else if (ValidationHelper.BiggerThanMaxLength(Email, 50)) return ValidationHelper.returnMessageMaxLength(50);
                        break;
                    case "WebSite":
                        if (ValidationHelper.EmptyField(WebSite)) return ValidationHelper.Empty;
                        else if (ValidationHelper.BiggerThanMaxLength(WebSite, 30)) return ValidationHelper.returnMessageMaxLength(30);
                        break;
                    case "Pib":
                        bool isNumeric = ValidationHelper.numeric(Pib);
                        if (!isNumeric) return ValidationHelper.Numeric;
                        else if (!ValidationHelper.containExact(Pib, 9)) return ValidationHelper.returnMessageExactLength(9);
                        break;
                    case "IdentificationNumber":
                        bool isNumeric1 = ValidationHelper.numeric(IdentificationNumber);
                        if (!isNumeric1) return ValidationHelper.Numeric;
                        else if (!ValidationHelper.containExact(IdentificationNumber, 8)) return ValidationHelper.returnMessageExactLength(8);
                        break;
                    case "AccountNumber":
                        AccountNumberValidationRule validator1 = new AccountNumberValidationRule();
                        if (validator1.Validate(AccountNumber, null) != ValidationResult.ValidResult) return ValidationHelper.Pattern + AccountNumberValidationRule.CorrectPattern;
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
            SchoolS schoolCopy = new SchoolS();
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
