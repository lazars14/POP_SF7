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
    public class TeacherTeachesCourse
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public bool Deleted { get; set; }

        public TeacherTeachesCourse(int id, int teacherId, int courseId, bool deleted)
        {
            Id = id;
            TeacherId = teacherId;
            CourseId = courseId;
            Deleted = deleted;
        }

        #region Database Operations

        public static bool Load()
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                bool valid = false;

                connection.Open();

                DataSet dataSet = new DataSet();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Select * From TeacherTeachesCourse;";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                try
                {
                    dataAdapter.Fill(dataSet, "TeacherTeachesCourse");

                    foreach (DataRow row in dataSet.Tables["TeacherTeachesCourse"].Rows)
                    {
                        int id = (int)row["TCourse_Id"];
                        int teacherId = (int)row["TCourse_TeacherId"];
                        int courseId = (int)row["TCourse_CourseId"];
                        bool deleted = (bool)row["TCourse_Deleted"];
                        TeacherTeachesCourse teach = new TeacherTeachesCourse(id, teacherId, courseId, deleted);

                        ApplicationA.Instance.TeacherTeachesCourseCollection.Add(teach);
                    }

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

        public static bool Add(TeacherTeachesCourse teaches)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                bool valid = false;

                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Insert Into TeacherTeachesCourse Values(@TeacherId,@CourseId,@Deleted);";

                try
                {
                    command.Parameters.Add(new SqlParameter("@TeacherId", teaches.TeacherId));
                    command.Parameters.Add(new SqlParameter("@CourseId", teaches.CourseId));
                    command.Parameters.Add(new SqlParameter("@Deleted", teaches.Deleted));

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
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + g.ParamName);
                }
                catch (NullReferenceException n)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + n.GetType());
                }

                return valid;
            }
        }

        public static bool Delete(TeacherTeachesCourse teaches)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                bool valid = false;

                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Update TeacherTeachesCourse Set TCourse_Deleted=1 Where TCourse_Id=@Id;";

                try
                {
                    command.Parameters.Add(new SqlParameter("@Id", teaches.Id));

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

        public static bool UnDelete(TeacherTeachesCourse teaches)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                bool valid = false;

                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Update TeacherTeachesCourse Set TCourse_Deleted=0 Where TCourse_Id=@Id;";

                try
                {
                    command.Parameters.Add(new SqlParameter("@Id", teaches.Id));

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
    }
}
