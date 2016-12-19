using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Collections.ObjectModel;

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

        public Course() { }

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
        
        }

        public static void Add(Course course)
        {

        }

        public static void Edit(Course course)
        {

        }

        public static void Delete(Course course)
        {

        }

        #endregion

        #region IDataErrorInfo

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch(columnName)
                {
                    case "Price":
                        double test;
                        bool isNumeric = double.TryParse(Price.ToString(), out test);
                        if (!isNumeric) return "Cena mora da se napise u numerickom formatu!";
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
