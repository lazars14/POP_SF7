using POP_SF7.Models.Courses;
using POP_SF7.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF7.ViewModels
{
    internal class CourseAddEditViewModel
    {
        public Course Course { get; set; }
        public List<Teacher> ListOfTeachers { get; set; }
        public List<CourseType> ListOfCourseTypes { get; set; }
        public List<Student> ListOfStudents { get; set; }
    }
}
