using System.Collections.Generic;

namespace POP_SF7
{
    public class Teacher : Person
    {
        public List<Language> ListOfLanguages { get; set; }
        public List<Course> ListOfCourses { get; set; }

        public Teacher(int id, string firstName, string lastName, string jmbg, string personAddress, bool deleted, List<Language> listOfLanguages, List<Course> listOfCourses)
            : base(id, firstName, lastName, jmbg, personAddress, deleted)
        {
            ListOfLanguages = listOfLanguages;
            ListOfCourses = listOfCourses;
        }
    }
}
