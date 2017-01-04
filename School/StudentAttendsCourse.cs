using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace POP_SF7.School
{
    public class StudentAttendsCourse
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public bool Deleted { get; set; }

        public StudentAttendsCourse(int id, int courseId, int studentId, bool deleted)
        {
            Id = id;
            CourseId = courseId;
            StudentId = studentId;
            Deleted = deleted;
        }

        #region Database Operations

        public static void Load()
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                DataSet dataSet = new DataSet();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Select * From StudentAttendsCourse;";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                try
                {
                    dataAdapter.Fill(dataSet, "StudentAttendsCourse");

                    foreach (DataRow row in dataSet.Tables["StudentAttendsCourse"].Rows)
                    {
                        int id = (int)row["Attends_Id"];
                        int courseId = (int)row["Attends_CourseId"];
                        int studentId = (int)row["Attends_StudentId"];
                        bool deleted = (bool)row["Attends_Deleted"];
                        StudentAttendsCourse attends = new StudentAttendsCourse(id, courseId, studentId, deleted);

                        ApplicationA.Instance.StudentAttendsCourseCollection.Add(attends);
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
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + g.ParamName);
                }
                catch (NullReferenceException n)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + n.GetType());
                }
            }

        }

        public static void Add(StudentAttendsCourse attends)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Insert Into StudentAttendsCourse Values(@CourseId,@StudentId,@Deleted);";

                try
                {
                    command.Parameters.Add(new SqlParameter("@CourseId", attends.CourseId));
                    command.Parameters.Add(new SqlParameter("@StudentId", attends.StudentId));
                    command.Parameters.Add(new SqlParameter("@Deleted", attends.Deleted));

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

        public static void Delete(StudentAttendsCourse attends)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Update StudentAttendsCourse Set Attends_Deleted=1 Where Attends_Id=@Id;";

                try
                {
                    command.Parameters.Add(new SqlParameter("@Id", attends.Id));

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
    }
}
