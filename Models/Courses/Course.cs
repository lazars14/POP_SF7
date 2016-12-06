using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using POP_SF7.Models.People;

namespace POP_SF7.Models.Courses
{
    public class Course : INotifyPropertyChanged, ICloneable
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
