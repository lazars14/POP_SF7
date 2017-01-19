using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace POP_SF7.DB
{
    public class CourseDAO
    {
        public static bool Load()
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                bool valid = false;

                connection.Open();

                DataSet dataSet = new DataSet();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Select * From Course;";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                try
                {
                    dataAdapter.Fill(dataSet, "Course");

                    foreach (DataRow row in dataSet.Tables["Course"].Rows)
                    {
                        int id = (int)row["Course_Id"];
                        int languageId = (int)row["Course_LanguageId"];
                        int courseTypeId = (int)row["Course_CourseTypeId"];
                        int teacherId = (int)row["Course_TeacherId"];
                        double price = (double)row["Course_Price"];
                        DateTime startDate = (DateTime)row["Course_StartDate"];
                        DateTime endDate = (DateTime)row["Course_EndDate"];
                        bool deleted = (bool)row["Course_Deleted"];
                        Course course = new Course(id, languageId, courseTypeId, price, teacherId, startDate, endDate, deleted);

                        ApplicationA.Instance.Courses.Add(course);
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

        public static bool Add(Course course)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                bool valid = false;

                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Insert Into Course Values(@Language,@CourseType,@Price,@Teacher,@StartDate,@EndDate,@Deleted);";

                try
                {
                    command.Parameters.Add(new SqlParameter("@Language", course.Language.Id));
                    command.Parameters.Add(new SqlParameter("@CourseType", course.CourseType.Id));
                    command.Parameters.Add(new SqlParameter("@Price", course.Price));
                    command.Parameters.Add(new SqlParameter("@Teacher", course.Teacher.Id));
                    command.Parameters.Add(new SqlParameter("@StartDate", course.StartDate));
                    command.Parameters.Add(new SqlParameter("@EndDate", course.EndDate));
                    command.Parameters.Add(new SqlParameter("@Deleted", course.Deleted));

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

        public static bool Edit(Course course)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                bool valid = false;

                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Update Course Set Course_LanguageId=@Language, Course_CourseTypeId=@CourseType, Course_Price=@Price, Course_TeacherId=@Teacher, Course_StartDate=@StartDate, Course_EndDate=@EndDate, Course_Deleted=@Deleted Where Course_Id=@Id;";

                try
                {
                    command.Parameters.Add(new SqlParameter("@Language", course.Language.Id));
                    command.Parameters.Add(new SqlParameter("@CourseType", course.CourseType.Id));
                    command.Parameters.Add(new SqlParameter("@Price", course.Price));
                    command.Parameters.Add(new SqlParameter("@Teacher", course.Teacher.Id));
                    command.Parameters.Add(new SqlParameter("@StartDate", course.StartDate));
                    command.Parameters.Add(new SqlParameter("@EndDate", course.EndDate));
                    command.Parameters.Add(new SqlParameter("@Deleted", course.Deleted));
                    command.Parameters.Add(new SqlParameter("@Id", course.Id));

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

        public static bool Delete(Course course)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                bool valid = false;

                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Update Course Set Course_Deleted=1 Where Course_Id=@Id;";

                try
                {
                    command.Parameters.Add(new SqlParameter("@Id", course.Id));

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
