﻿using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace POP_SF7
{
    public class Teacher : Person, ICloneable
    {
        public ObservableCollection<Language> ListOfLanguages { get; set; }
        public ObservableCollection<Course> ListOfCourses { get; set; }

        public Teacher() { }

        public Teacher(int id, string firstName, string lastName, string jmbg, string personAddress, bool deleted) : base(id, firstName, lastName, jmbg, personAddress, deleted)
        {
            ListOfLanguages = new ObservableCollection<Language>();
            ListOfCourses = new ObservableCollection<Course>();
        }

        #region Database operations

        public static void Load()
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
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
            }
        }

        public static void Add(Teacher teacher)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Insert Into Teacher Values(@FirstName,@LastName,@Jmbg,@Address,@Deleted);";

                command.Parameters.Add(new SqlParameter("@FirstName", teacher.FirstName));
                command.Parameters.Add(new SqlParameter("@LastName", teacher.LastName));
                command.Parameters.Add(new SqlParameter("@Jmbg", teacher.Jmbg));
                command.Parameters.Add(new SqlParameter("@Address", teacher.Address));
                command.Parameters.Add(new SqlParameter("@Deleted", teacher.Deleted));

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
            }
        }

        public static void Edit(Teacher teacher)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Update Teacher Set Teacher_FirstName=@FirstName, Teacher_LastName=@LastName, Teacher_Jmbg=@Jmbg, Teacher_Address=@Address, Teacher_Deleted=@Deleted Where Teacher_Id=@Id;";

                command.Parameters.Add(new SqlParameter("@FirstName", teacher.FirstName));
                command.Parameters.Add(new SqlParameter("@LastName", teacher.LastName));
                command.Parameters.Add(new SqlParameter("@Jmbg", teacher.Jmbg));
                command.Parameters.Add(new SqlParameter("@Address", teacher.Address));
                command.Parameters.Add(new SqlParameter("@Deleted", teacher.Deleted));
                command.Parameters.Add(new SqlParameter("@Id", teacher.Id));

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
            }
        }

        public static void Delete(Teacher teacher)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Update Teacher Set Teacher_Deleted=1 Where Teacher_Id=@Id;";

                command.Parameters.Add(new SqlParameter("@Id", teacher.Id));

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
            }
        }

        #endregion

        #region ICloneable

        public object Clone()
        {
            Teacher teacherCopy = new Teacher();
            teacherCopy.Id = Id;
            teacherCopy.FirstName = FirstName;
            teacherCopy.LastName = LastName;
            teacherCopy.Jmbg = Jmbg;
            teacherCopy.Address = Address;
            teacherCopy.Deleted = Deleted;
            teacherCopy.ListOfLanguages = ListOfLanguages;
            teacherCopy.ListOfCourses = ListOfCourses;

            return teacherCopy;
        }

        #endregion

    }
}
