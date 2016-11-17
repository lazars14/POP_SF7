using System.Collections.Generic;
using System;

namespace POP_SF7
{
    class Course
    {
        public int Id { get; set; }
        public Language Language { get; set; }
        public CourseType CourseType { get; set; }
        public double Price { get; set; }
        public List<Student> ListOfStudents { get; set; }
        public Teacher Teacher { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Deleted { get; set; }

        public Course(int courseId, Language language, CourseType courseType, double price, List<Student> listOfStudents, Teacher teacher, DateTime startDate, DateTime endDate, bool deleted)
        {
            Id = courseId;
            Language = language;
            CourseType = courseType;
            Price = price;
            ListOfStudents = listOfStudents;
            Teacher = teacher;
            StartDate = startDate;
            EndDate = endDate;
            Deleted = deleted;
        }
    }
}
