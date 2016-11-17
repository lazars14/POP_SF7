using System.Collections.Generic;

namespace POP_SF7
{
    class Student : Person
    {
        public List<Payment> ListOfPayments { get; set; }
        public List<Course> ListOfCourses { get; set; }

        public Student(int id, string firstName, string lastName, string jmbg, string personAddress, bool deleted, List<Payment> listOfPayments, List<Course> listOfCourses) : base(id, firstName, lastName, jmbg, personAddress, deleted)
        {
            ListOfPayments = listOfPayments;
            ListOfCourses = listOfCourses;
        }
    }
}
