using System.Collections.Generic;

namespace POP_SF7
{
    public enum CourseLevel { Begginer, Intermediate, Advanced }

    class Course
    {
        public int Id { get; set; }
        public Language Language { get; set; }
        public CourseType CourseType { get; set; }
        public double Price { get; set; }
        public List<Student> ListOfStudents { get; set; }
        public Teacher Teacher { get; set; }
        public bool Deleted { get; set; }

        public Course(int courseId, Language language, CourseType courseType, double price, List<Student> listOfStudents, Teacher teacher, bool deleted)
        {
            Id = courseId;
            Language = language;
            CourseType = courseType;
            Price = price;
            ListOfStudents = listOfStudents;
            Teacher = teacher;
            Deleted = deleted;
        }
    }
}
