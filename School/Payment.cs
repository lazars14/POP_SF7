using System;
using System.ComponentModel;

namespace POP_SF7
{
    public class Payment : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        public Course Course { get; set; }
        public Student Student { get; set; }

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

        public Payment(int id, Course course, Student student, double amount, DateTime date, bool deleted)
        {
            Id = id;
            Course = course;
            Student = student;
            Amount = amount;
            Date = date;
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
