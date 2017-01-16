using POP_SF7.School;
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
    public class TeacherTeachesLanguageDAO
    {
        public static bool Load()
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                bool valid = false;

                connection.Open();

                DataSet dataSet = new DataSet();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Select * From TeacherTeachesLanguage;";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                try
                {
                    dataAdapter.Fill(dataSet, "TeacherTeachesLanguage");

                    foreach (DataRow row in dataSet.Tables["TeacherTeachesLanguage"].Rows)
                    {
                        int id = (int)row["Teaches_Id"];
                        int teacherId = (int)row["Teaches_TeacherId"];
                        int languageId = (int)row["Teaches_LanguageId"];
                        bool deleted = (bool)row["Teaches_Deleted"];
                        TeacherTeachesLanguage teach = new TeacherTeachesLanguage(id, teacherId, languageId, deleted);

                        ApplicationA.Instance.TeacherTeachesLanguageCollection.Add(teach);
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

        public static bool Add(TeacherTeachesLanguage teach)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                bool valid = false;

                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Insert Into TeacherTeachesLanguage Values(@TeacherId,@LanguageId,@Deleted);";

                try
                {
                    command.Parameters.Add(new SqlParameter("@TeacherId", teach.TeacherId));
                    command.Parameters.Add(new SqlParameter("@LanguageId", teach.LanguageId));
                    command.Parameters.Add(new SqlParameter("@Deleted", teach.Deleted));

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

        public static bool Delete(TeacherTeachesLanguage teach)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                bool valid = false;

                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Update TeacherTeachesLanguage Set Teaches_Deleted=1 Where Teaches_Id=@Id;";

                try
                {
                    command.Parameters.Add(new SqlParameter("@Id", teach.Id));

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

        public static bool UnDelete(TeacherTeachesLanguage teach)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                bool valid = false;

                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Update TeacherTeachesLanguage Set Teaches_Deleted=0 Where Teaches_Id=@Id;";

                try
                {
                    command.Parameters.Add(new SqlParameter("@Id", teach.Id));

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
