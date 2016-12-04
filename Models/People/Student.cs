using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace POP_SF7.Models.People
{
    public class Student : Person, ICloneable
    {
        public ObservableCollection<Payment> ListOfPayments { get; set; }
        public ObservableCollection<Course> ListOfCourses { get; set; }

        public Student() { }

        public Student(int id, string firstName, string lastName, string jmbg, string personAddress, bool deleted) : base(id, firstName, lastName, jmbg, personAddress, deleted)
        {
            ListOfPayments = new ObservableCollection<Payment>();
            ListOfCourses = new ObservableCollection<Course>();
        }

        #region ICloneable

        public object Clone()
        {
            Student studentCopy = new Student();
            studentCopy.Id = Id;
            studentCopy.FirstName = FirstName;
            studentCopy.LastName = LastName;
            studentCopy.Jmbg = Jmbg;
            studentCopy.Address = Address;
            studentCopy.Deleted = Deleted;
            studentCopy.ListOfPayments = ListOfPayments;
            studentCopy.ListOfCourses = ListOfCourses;

            return studentCopy;
        }

        #endregion

    }
}
