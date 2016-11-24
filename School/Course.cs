using System.Collections.Generic;
using System;
using System.ComponentModel;

namespace POP_SF7
{
    public class Course : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        public Language Language { get; set; }
        public CourseType CourseType { get; set; }

        private double price;
        public double Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged("Price"); }
        }

        public List<Student> ListOfStudents { get; set; }
        public Teacher Teacher { get; set; }

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

        public Course(int id, Language language, CourseType courseType, double price, List<Student> listOfStudents, Teacher teacher, DateTime startDate, DateTime endDate, bool deleted)
        {
            Id = id;
            Language = language;
            CourseType = courseType;
            Price = price;
            ListOfStudents = listOfStudents;
            Teacher = teacher;
            StartDate = startDate;
            EndDate = endDate;
            Deleted = deleted;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
