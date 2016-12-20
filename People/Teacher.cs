using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

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

                SqlCommand loadCommand = connection.CreateCommand();
                loadCommand.CommandText = @"Select * From Teacher;";

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = loadCommand;
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
        }

        public static void Add(Teacher teacher)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand addCommand = connection.CreateCommand();
                addCommand.CommandText = @"Insert Into Teacher Values(@FirstName,@LastName,@Jmbg,@Address,@Deleted);";

                addCommand.Parameters.Add(new SqlParameter("@FirstName", teacher.FirstName));
                addCommand.Parameters.Add(new SqlParameter("@LastName", teacher.LastName));
                addCommand.Parameters.Add(new SqlParameter("@Jmbg", teacher.Jmbg));
                addCommand.Parameters.Add(new SqlParameter("@Address", teacher.Address));
                addCommand.Parameters.Add(new SqlParameter("@Deleted", teacher.Deleted));

                addCommand.ExecuteNonQuery();
            }
        }

        public static void Edit(Teacher teacher)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand addCommand = connection.CreateCommand();
                addCommand.CommandText = @"Update Teacher Set Teacher_FirstName=@FirstName, Teacher_LastName=@LastName, Teacher_Jmbg=@Jmbg, Teacher_Address=@Address, Teacher_Deleted=@Deleted Where Teacher_Id=@Id;";

                addCommand.Parameters.Add(new SqlParameter("@FirstName", teacher.FirstName));
                addCommand.Parameters.Add(new SqlParameter("@LastName", teacher.LastName));
                addCommand.Parameters.Add(new SqlParameter("@Jmbg", teacher.Jmbg));
                addCommand.Parameters.Add(new SqlParameter("@Address", teacher.Address));
                addCommand.Parameters.Add(new SqlParameter("@Deleted", teacher.Deleted));
                addCommand.Parameters.Add(new SqlParameter("@Id", teacher.Id));

                addCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(Teacher teacher)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand addCommand = connection.CreateCommand();
                addCommand.CommandText = @"Update Teacher Set Teacher_Deleted=1 Where Teacher_Id=@Id;";

                addCommand.Parameters.Add(new SqlParameter("@Id", teacher.Id));

                addCommand.ExecuteNonQuery();
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
