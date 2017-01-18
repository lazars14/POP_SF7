using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using POP_SF7.Helpers;
using System.Globalization;

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

        public ObservableCollection<Student> ListOfStudents { get; set; }
        public ObservableCollection<Student> ListOfDeletedStudents { get; set; }

        public Course() { Price = 0; StartDate = DateTime.Today; EndDate = DateTime.Today.AddDays(7); ListOfStudents = new ObservableCollection<Student>(); ListOfDeletedStudents = new ObservableCollection<Student>(); }

        public Course(int id) { Id = id; }

        public Course(int id, int languageId, int courseTypeId, double price, int teacherId, DateTime startDate, DateTime endDate, bool deleted)
        {
            Id = id;
            Language = new Language(languageId);
            CourseType = new CourseType(courseTypeId);
            Price = price;
            Teacher = new Teacher(teacherId);
            StartDate = startDate;
            EndDate = endDate;
            Deleted = deleted;
            ListOfStudents = new ObservableCollection<Student>();
            ListOfDeletedStudents = new ObservableCollection<Student>();
        }

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
            courseCopy.Teacher = Teacher;
            courseCopy.StartDate = StartDate;
            courseCopy.EndDate = EndDate;
            courseCopy.Deleted = Deleted;
            courseCopy.ListOfStudents = ListOfStudents;
            courseCopy.ListOfDeletedStudents = ListOfDeletedStudents;

            return courseCopy;
        }

        #endregion
    }
}
