using System;
using System.ComponentModel;

namespace POP_SF7.Models.School
{
    public class Payment : INotifyPropertyChanged, ICloneable
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private Course course;
        public Course Course
        {
            get { return course; }
            set { course = value; OnPropertyChanged("Course"); }
        }

        private Student student;
        public Student Student
        {
            get { return student; }
            set { student = value; OnPropertyChanged("Student"); }
        }

        private double amount;
        public double Amount
        {
            get { return amount; }
            set { amount = value; OnPropertyChanged("Amount"); }
        }

        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged("Date"); }
        }

        private bool deleted;
        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; OnPropertyChanged("Deleted"); }
        }

        public Payment() { }

        public Payment(int id, Course course, Student student, double amount, DateTime date, bool deleted)
        {
            Id = id;
            Course = course;
            Student = student;
            Amount = amount;
            Date = date;
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
            Payment paymentCopy = new Payment();
            paymentCopy.Id = Id;
            paymentCopy.Course = Course;
            paymentCopy.Student = Student;
            paymentCopy.Amount = Amount;
            paymentCopy.Date = Date;
            paymentCopy.Deleted = Deleted;

            return paymentCopy;
        }

        #endregion
    }
}
