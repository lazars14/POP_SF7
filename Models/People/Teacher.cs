using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

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
