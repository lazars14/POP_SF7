using POP_SF7.Helpers;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace POP_SF7
{
    public class Payment : INotifyPropertyChanged, ICloneable, IDataErrorInfo
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

        public Payment() { Amount = 0; Date = DateTime.Today; }

        public Payment(int id) { Id = id; }

        public Payment(int id, int courseId, int studentId, double amount, DateTime date, bool deleted)
        {
            Id = id;
            Course = new Course(courseId);
            Student = new Student(studentId);
            Amount = amount;
            Date = date;
            Deleted = deleted;
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
                    case "Amount":
                        bool isDouble = ValidationHelper.isDouble(Amount.ToString());
                        if (!isDouble) return ValidationHelper.Numeric;
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
