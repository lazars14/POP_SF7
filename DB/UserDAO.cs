using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace POP_SF7.DB
{
    public class UserDAO
    {
        public static bool Load()
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                bool valid = false;

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

                    valid = true;
                }
                catch (SqlException e)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE);
                    ApplicationA.WriteToLog(e.StackTrace);
                }
                catch (InvalidOperationException a)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE);
                    ApplicationA.WriteToLog(a.StackTrace);
                }
                catch (ArgumentException g)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE);
                    ApplicationA.WriteToLog(g.StackTrace);
                }
                catch (NullReferenceException n)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE);
                    ApplicationA.WriteToLog(n.StackTrace);
                }

                return valid;
            }
        }

        public static bool Add(User user)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                bool valid = false;

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

                    valid = true;
                }
                catch (SqlException e)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE);
                    ApplicationA.WriteToLog(e.StackTrace);
                }
                catch (InvalidOperationException a)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE);
                    ApplicationA.WriteToLog(a.StackTrace);
                }
                catch (ArgumentException g)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE);
                    ApplicationA.WriteToLog(g.StackTrace);
                }
                catch (NullReferenceException n)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE);
                    ApplicationA.WriteToLog(n.StackTrace);
                }

                return valid;
            }
        }

        public static bool Edit(User user)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                bool valid = false;

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

                    valid = true;
                }
                catch (SqlException e)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE);
                    ApplicationA.WriteToLog(e.StackTrace);
                }
                catch (InvalidOperationException a)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE);
                    ApplicationA.WriteToLog(a.StackTrace);
                }
                catch (ArgumentException g)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE);
                    ApplicationA.WriteToLog(g.StackTrace);
                }
                catch (NullReferenceException n)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE);
                    ApplicationA.WriteToLog(n.StackTrace);
                }

                return valid;
            }
        }

        public static bool Delete(User user)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                bool valid = false;

                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Update UserU Set UserU_Deleted=1 Where UserU_Id=@Id;";

                try
                {
                    command.Parameters.Add(new SqlParameter("@Id", user.Id));

                    command.ExecuteNonQuery();

                    valid = true;
                }
                catch (SqlException e)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE);
                    ApplicationA.WriteToLog(e.StackTrace);
                }
                catch (InvalidOperationException a)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE);
                    ApplicationA.WriteToLog(a.StackTrace);
                }
                catch (ArgumentException g)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE);
                    ApplicationA.WriteToLog(g.StackTrace);
                }
                catch (NullReferenceException n)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE);
                    ApplicationA.WriteToLog(n.StackTrace);
                }

                return valid;
            }
        }
    }
}
