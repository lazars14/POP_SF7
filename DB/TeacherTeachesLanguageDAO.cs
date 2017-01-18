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
                        int teacherId = (int)row["Teaches_TeacherId"];
                        Teacher t = ApplicationA.Instance.Teachers[teacherId - 1];

                        int languageId = (int)row["Teaches_LanguageId"];
                        Language l = ApplicationA.Instance.Languages[languageId - 1];

                        bool deleted = (bool)row["Teaches_Deleted"];
                        
                        if(!deleted)
                        {
                            t.ListOfLanguages.Add(l);
                        }
                        else
                        {
                            t.ListOfDeletedLanguages.Add(l);
                        }
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

        public static bool Add(int teacherId, int languageId)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                bool valid = false;

                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Insert Into TeacherTeachesLanguage Values(@TeacherId,@LanguageId,0);";

                try
                {
                    command.Parameters.Add(new SqlParameter("@TeacherId", teacherId));
                    command.Parameters.Add(new SqlParameter("@LanguageId", languageId));

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

        public static bool Delete(int teacherId, int languageId)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                bool valid = false;

                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Update TeacherTeachesLanguage Set Teaches_Deleted=1 Where Teaches_TeacherId=@TeacherId And Teaches_LanguageId=@LanguageId;";

                try
                {
                    command.Parameters.Add(new SqlParameter("@TeacherId", teacherId));
                    command.Parameters.Add(new SqlParameter("@LanguageId", languageId));

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

        public static bool UnDelete(int teacherId, int languageId)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                bool valid = false;

                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Update TeacherTeachesLanguage Set Teaches_Deleted=0 Where Teaches_TeacherId=@TeacherId And Teaches_LanguageId=@LanguageId;";

                try
                {
                    command.Parameters.Add(new SqlParameter("@TeacherId", teacherId));
                    command.Parameters.Add(new SqlParameter("@LanguageId", languageId));

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
