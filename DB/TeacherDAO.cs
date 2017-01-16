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
    public class TeacherDAO
    {
        public static bool Load()
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                bool valid = false;

                connection.Open();

                DataSet dataSet = new DataSet();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Select * From Teacher;";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                try
                {
                    dataAdapter.Fill(dataSet, "Teacher");

                    foreach (DataRow row in dataSet.Tables["Teacher"].Rows)
                    {
                        Teacher teacher = new Teacher();
                        teacher.Id = (int)row["Teacher_Id"];
                        teacher.FirstName = (string)row["Teacher_FirstName"];
                        teacher.LastName = (string)row["Teacher_LastName"];
                        teacher.Jmbg = (string)row["Teacher_Jmbg"];
                        teacher.Address = (string)row["Teacher_Address"];
                        teacher.Deleted = (bool)row["Teacher_Deleted"];

                        ApplicationA.Instance.Teachers.Add(teacher);
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

        public static bool Add(Teacher teacher)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                bool valid = false;

                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Insert Into Teacher Values(@FirstName,@LastName,@Jmbg,@Address,@Deleted);";

                try
                {
                    command.Parameters.Add(new SqlParameter("@FirstName", teacher.FirstName));
                    command.Parameters.Add(new SqlParameter("@LastName", teacher.LastName));
                    command.Parameters.Add(new SqlParameter("@Jmbg", teacher.Jmbg));
                    command.Parameters.Add(new SqlParameter("@Address", teacher.Address));
                    command.Parameters.Add(new SqlParameter("@Deleted", teacher.Deleted));

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

        public static bool Edit(Teacher teacher)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                bool valid = false;

                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Update Teacher Set Teacher_FirstName=@FirstName, Teacher_LastName=@LastName, Teacher_Jmbg=@Jmbg, Teacher_Address=@Address, Teacher_Deleted=@Deleted Where Teacher_Id=@Id;";

                try
                {
                    command.Parameters.Add(new SqlParameter("@FirstName", teacher.FirstName));
                    command.Parameters.Add(new SqlParameter("@LastName", teacher.LastName));
                    command.Parameters.Add(new SqlParameter("@Jmbg", teacher.Jmbg));
                    command.Parameters.Add(new SqlParameter("@Address", teacher.Address));
                    command.Parameters.Add(new SqlParameter("@Deleted", teacher.Deleted));
                    command.Parameters.Add(new SqlParameter("@Id", teacher.Id));

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

        public static bool Delete(Teacher teacher)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                bool valid = false;

                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Update Teacher Set Teacher_Deleted=1 Where Teacher_Id=@Id;";

                try
                {
                    command.Parameters.Add(new SqlParameter("@Id", teacher.Id));

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
