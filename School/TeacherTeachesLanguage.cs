﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace POP_SF7.School
{
    public class TeacherTeachesLanguage
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int LanguageId { get; set; }
        public bool Deleted { get; set; }

        public TeacherTeachesLanguage(int id, int teacherId, int languageId, bool deleted)
        {
            Id = id;
            TeacherId = teacherId;
            LanguageId = languageId;
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
                command.CommandText = @"Select * From TeacherTeachesLanguage;";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                try
                {
                    dataAdapter.Fill(dataSet, "TeacherTeachesLanguage");

                    foreach(DataRow row in dataSet.Tables["TeacherTeachesLanguage"].Rows)
                    {
                        int id = (int)row["Teaches_Id"];
                        int teacherId = (int)row["Teaches_TeacherId"];
                        int languageId = (int)row["Teaches_LanguageId"];
                        bool deleted = (bool)row["Teaches_Deleted"];
                        TeacherTeachesLanguage teach = new TeacherTeachesLanguage(id, teacherId, languageId, deleted);

                        ApplicationA.Instance.TeacherTeachesLanguageCollection.Add(teach);
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

        public static void Add(TeacherTeachesLanguage teach)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Insert Into TeacherTeachesLanguage Values(@TeacherId,@LanguageId,@Deleted);";

                try
                {
                    command.Parameters.Add(new SqlParameter("@TeacherId", teach.TeacherId));
                    command.Parameters.Add(new SqlParameter("@LanguageId", teach.LanguageId));
                    command.Parameters.Add(new SqlParameter("@Deleted", teach.Deleted));

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

        public static void Delete(TeacherTeachesLanguage teach)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Update TeacherTeachesLanguage Set Teaches_Deleted=1 Where Teaches_Id=@Id;";

                try
                {
                    command.Parameters.Add(new SqlParameter("@Id", teach.Id));

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
