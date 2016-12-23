using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using POP_SF7.Helpers;

namespace POP_SF7
{
    public class Course : INotifyPropertyChanged, ICloneable, IDataErrorInfo
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private Language language;
        public Language Language
        {
            get { return language; }
            set { language = value; OnPropertyChanged("Language"); }
        }

        private CourseType courseType;
        public CourseType CourseType
        {
            get { return courseType; }
            set { courseType = value; OnPropertyChanged("CourseType"); }
        }

        private double price;
        public double Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged("Price"); }
        }

        public ObservableCollection<Student> ListOfStudents { get; set; }

        private Teacher teacher;
        public Teacher Teacher
        {
            get { return teacher; }
            set { teacher = value; OnPropertyChanged("Teacher"); }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; OnPropertyChanged("StartDate"); }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; OnPropertyChanged("EndDate"); }
        }

        private bool deleted;
        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; OnPropertyChanged("Deleted"); }
        }

        public Course() { Price = 0; }

        public Course(int id, Language language, CourseType courseType, double price, Teacher teacher, DateTime startDate, DateTime endDate, bool deleted)
        {
            Id = id;
            Language = language;
            CourseType = courseType;
            Price = price;
            ListOfStudents = new ObservableCollection<Student>();
            Teacher = teacher;
            StartDate = startDate;
            EndDate = endDate;
            Deleted = deleted;
        }

        #region Database operations

        public static void Load()
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
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
                        Course course = new Course();
                        course.Id = (int)row["Course_Id"];
                        course.Price = (double)row["Course_Price"];
                        course.StartDate = (DateTime)row["Course_StartDate"];
                        course.EndDate = (DateTime)row["Course_EndDate"];
                        course.Deleted = (bool)row["Deleted"];

                        ApplicationA.Instance.Courses.Add(course);
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
            }
        }

        public static void Add(Course course)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Insert Into Course Values(@Language,@CourseType,@Price,@Teacher,@StartDate,@EndDate,@Deleted);";

                command.Parameters.Add(new SqlParameter("@Language", course.Language.Id));
                command.Parameters.Add(new SqlParameter("@CourseType", course.CourseType.Id));
                command.Parameters.Add(new SqlParameter("@Price", course.Price));
                command.Parameters.Add(new SqlParameter("@Teacher", course.Teacher.Id));
                command.Parameters.Add(new SqlParameter("@StartDate", course.StartDate));
                command.Parameters.Add(new SqlParameter("@EndDate", course.EndDate));
                command.Parameters.Add(new SqlParameter("@Deleted", course.Deleted));

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
            }
        }

        public static void Edit(Course course)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Update Course Set Course_Language=@Language, Course_CourseType=@CourseType, Course_Price=@Price, Course_Teacher=@Teacher, Course_StartDate=@StartDate, Course_EndDate=@EndDate, Course_Deleted=@Deleted Where Course_Id=@Id);";

                command.Parameters.Add(new SqlParameter("@Language", course.Language.Id));
                command.Parameters.Add(new SqlParameter("@CourseType", course.CourseType.Id));
                command.Parameters.Add(new SqlParameter("@Price", course.Price));
                command.Parameters.Add(new SqlParameter("@Teacher", course.Teacher.Id));
                command.Parameters.Add(new SqlParameter("@StartDate", course.StartDate));
                command.Parameters.Add(new SqlParameter("@EndDate", course.EndDate));
                command.Parameters.Add(new SqlParameter("@Deleted", course.Deleted));
                command.Parameters.Add(new SqlParameter("@Id", course.Id));

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
            }
        }

        public static void Delete(Course course)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Update Course Set Course_Deleted=1 Where Course_Id=@Id;";

                command.Parameters.Add(new SqlParameter("@Id", course.Id));

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
            }
        }

        #endregion

        #region IDataErrorInfo

        public string Error
        {
            get
            {
                return "";
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch(columnName)
                {
                    case "Price":
                        bool isDouble = ValidationHelper.isDouble(Price.ToString());
                        if (!isDouble) return ValidationHelper.Numeric;
                        break;
                    case "EndDate":
                        if (EndDate < StartDate) return "Zavrsni datum ne sme biti manji od pocetnog!";
                        break;
                }
                return "";
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

        #region ICloneable

        public object Clone()
        {
            Course courseCopy = new Course();
            courseCopy.Id = Id;
            courseCopy.Language = Language;
            courseCopy.CourseType = CourseType;
            courseCopy.Price = Price;
            courseCopy.ListOfStudents = new ObservableCollection<Student>(ListOfStudents);
            courseCopy.Teacher = Teacher;
            courseCopy.StartDate = StartDate;
            courseCopy.EndDate = EndDate;
            courseCopy.Deleted = Deleted;

            return courseCopy;
        }

        #endregion
    }
}
