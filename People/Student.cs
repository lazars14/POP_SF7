using System;
using System.Collections.ObjectModel;

namespace POP_SF7
{
    public class Student : Person, ICloneable
    {
        public ObservableCollection<Course> ListOfCourses { get; set; }
        public ObservableCollection<Course> ListOfDeletedCourses { get; set; }

        public Student()
        {
            FirstName = ApplicationA.FILL_FIELD;
            LastName = ApplicationA.FILL_FIELD;
            Address = ApplicationA.FILL_FIELD;
            Jmbg = "1234567890123";
            ListOfCourses = new ObservableCollection<Course>();
            ListOfDeletedCourses = new ObservableCollection<Course>();
        }

        public Student(int id) { Id = id; }

        public Student(int id, string firstName, string lastName, string jmbg, string personAddress, bool deleted) : base(id, firstName, lastName, jmbg, personAddress, deleted)
        {
            ListOfCourses = new ObservableCollection<Course>();
            ListOfDeletedCourses = new ObservableCollection<Course>();
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
            studentCopy.ListOfCourses = ListOfCourses;
            studentCopy.ListOfDeletedCourses = ListOfDeletedCourses;

            return studentCopy;
        }

        #endregion

    }
}
