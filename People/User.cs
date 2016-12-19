using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

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

        #region Database operations

        public static void Load()
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                DataSet dataSet = new DataSet();

                SqlCommand loadCommand = connection.CreateCommand();
                loadCommand.CommandText = @"Select * From User;";

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = loadCommand;
                dataAdapter.Fill(dataSet, "User");

                foreach (DataRow row in dataSet.Tables["User"].Rows)
                {
                    User user = new User();
                    user.Id = (int)row["UserU_Id"];
                    user.FirstName = (string)row["UserU_FirstName"];
                    user.LastName = (string)row["UserU_LastName"];
                    user.Jmbg = (string)row["UserU_Jmbg"];
                    user.Address = (string)row["UserU_Address"];
                    user.Deleted = (bool)row["UserU_Deleted"];
                    user.UserName = (string)row["UserU_UserName"];
                    user.Password = (string)row["UserU_Password"];
                    string role = (string)row["UserU_UserRole"];
                    user.UserRole = (role.Equals("ADMINISTRATOR")) ? Role.Administrator : Role.Employee;

                    ApplicationA.Instance.Users.Add(user);
                }
            }
        }

        public static void Add(User user)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand addCommand = connection.CreateCommand();
                addCommand.CommandText = @"Insert Into User Values(@FirstName,@LastName,@Jmbg,@Address,@Deleted,@UserName,@Password,@Role);";

                addCommand.Parameters.Add(new SqlParameter("@FirstName", user.FirstName));
                addCommand.Parameters.Add(new SqlParameter("@LastName", user.LastName));
                addCommand.Parameters.Add(new SqlParameter("@Jmbg", user.Jmbg));
                addCommand.Parameters.Add(new SqlParameter("@Address", user.Address));
                addCommand.Parameters.Add(new SqlParameter("@Deleted", user.Deleted));
                addCommand.Parameters.Add(new SqlParameter("@UserName", user.UserName));
                addCommand.Parameters.Add(new SqlParameter("@Password", user.Password));
                addCommand.Parameters.Add(new SqlParameter("@Role", user.UserRole.ToString()));

                addCommand.ExecuteNonQuery();
            }
        }

        public static void Edit(User user)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand addCommand = connection.CreateCommand();
                addCommand.CommandText = @"Update User Set UserU_FirstName=@FirstName, UserU_LastName=@LastName, UserU_Jmbg=@Jmbg, UserU_Address=@Address, UserU_Deleted=@Deleted, UserU_UserName=@UserName, UserU_Password=@Password, UserU_UserRole=@Role Where UserU_Id=@Id;";

                addCommand.Parameters.Add(new SqlParameter("@FirstName", user.FirstName));
                addCommand.Parameters.Add(new SqlParameter("@LastName", user.LastName));
                addCommand.Parameters.Add(new SqlParameter("@Jmbg", user.Jmbg));
                addCommand.Parameters.Add(new SqlParameter("@Address", user.Address));
                addCommand.Parameters.Add(new SqlParameter("@Deleted", user.Deleted));
                addCommand.Parameters.Add(new SqlParameter("@Id", user.Id));
                addCommand.Parameters.Add(new SqlParameter("@UserName", user.UserName));
                addCommand.Parameters.Add(new SqlParameter("@Password", user.Password));
                addCommand.Parameters.Add(new SqlParameter("@Role", user.UserRole.ToString()));

                addCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(User user)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand addCommand = connection.CreateCommand();
                addCommand.CommandText = @"Delete From User Where UserU_Id=@Id;";

                addCommand.Parameters.Add(new SqlParameter("@Id", user.Id));

                addCommand.ExecuteNonQuery();
            }
        }

        #endregion

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
