using POP_SF7.Models.Courses;
using POP_SF7.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF7.ViewModels
{
    class CourseMenuViewModel
    {
        public List<Course> ListOfCourses { get; set; }
        public List<Student> ListOfStudents { get; set; }
        public List<String> ListOfLanguages { get; set; }
        public List<String> ListOfCourseTypes { get; set; }

        public string SortCriteria { get; set; }
        public string SortOrder { get; set; }
        public bool SearchLanguage { get; set; }
        public bool SearchCourseType { get; set; }
        public string Language { get; set; }
        public string CourseType { get; set; }
    }
}
