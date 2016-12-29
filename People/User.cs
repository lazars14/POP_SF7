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

        public User(int id, string firstName, string lastName, string jmbg, string personAddress, string userName, string password, Role userRole, bool deleted) : base(id, firstName, lastName, jmbg, personAddress, deleted)
        {
            UserName = userName;
            Password = password;
            UserRole = userRole;
        }

        #region Database operations

        public static void Load()
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                DataSet dataSet = new DataSet();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Select * From UserU;";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                try
                {
                    dataAdapter.Fill(dataSet, "UserU");

                    foreach (DataRow row in dataSet.Tables["UserU"].Rows)
                    {
                        User user = new User();
                        user.Id = (int)row["UserU_Id"];
                        user.FirstName = (string)row["UserU_FirstName"];
                        user.LastName = (string)row["UserU_LastName"];
                        user.Jmbg = (string)row["UserU_Jmbg"];
                        user.Address = (string)row["UserU_Address"];
                        user.Deleted = (bool)row["UserU_Deleted"];
                        user.UserName = (string)row["UserU_UserName"];
                        user.Password = (string)row["UserU_PasswordP"];
                        string role = (string)row["UserU_UserRole"];
                        user.UserRole = (role.Equals("ADMINISTRATOR")) ? Role.ADMINISTRATOR : Role.EMPLOYEE;

                        ApplicationA.Instance.Users.Add(user);
                    }
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
            }
        }

        public static void Add(User user)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Insert Into UserU Values(@FirstName,@LastName,@Jmbg,@Address,@Deleted,@UserName,@Password,@Role);";

                try
                {
                    command.Parameters.Add(new SqlParameter("@FirstName", user.FirstName));
                    command.Parameters.Add(new SqlParameter("@LastName", user.LastName));
                    command.Parameters.Add(new SqlParameter("@Jmbg", user.Jmbg));
                    command.Parameters.Add(new SqlParameter("@Address", user.Address));
                    command.Parameters.Add(new SqlParameter("@Deleted", user.Deleted));
                    command.Parameters.Add(new SqlParameter("@UserName", user.UserName));
                    command.Parameters.Add(new SqlParameter("@Password", user.Password));
                    command.Parameters.Add(new SqlParameter("@Role", user.UserRole.ToString()));

                    command.ExecuteNonQuery();
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
            }
        }

        public static void Edit(User user)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Update UserU Set UserU_FirstName=@FirstName, UserU_LastName=@LastName, UserU_Jmbg=@Jmbg, UserU_Address=@Address, UserU_Deleted=@Deleted, UserU_UserName=@UserName, UserU_PasswordP=@Password, UserU_UserRole=@Role Where UserU_Id=@Id;";

                try
                {
                    command.Parameters.Add(new SqlParameter("@FirstName", user.FirstName));
                    command.Parameters.Add(new SqlParameter("@LastName", user.LastName));
                    command.Parameters.Add(new SqlParameter("@Jmbg", user.Jmbg));
                    command.Parameters.Add(new SqlParameter("@Address", user.Address));
                    command.Parameters.Add(new SqlParameter("@Deleted", user.Deleted));
                    command.Parameters.Add(new SqlParameter("@Id", user.Id));
                    command.Parameters.Add(new SqlParameter("@UserName", user.UserName));
                    command.Parameters.Add(new SqlParameter("@Password", user.Password));
                    command.Parameters.Add(new SqlParameter("@Role", user.UserRole.ToString()));

                    command.ExecuteNonQuery();
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
            }
        }

        public static void Delete(User user)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Update UserU Set UserU_Deleted=1 Where UserU_Id=@Id;";

                command.Parameters.Add(new SqlParameter("@Id", user.Id));

                try
                {
                    command.ExecuteNonQuery();
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
            }
        }

        #endregion

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
