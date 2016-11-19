using System.Collections.Generic;

namespace POP_SF7
{
    public class School
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public string Pib { get; set; }
        public string IdentificationNumber { get; set; }
        public string AccountNumber { get; set; }
        public List<Course> ListOfCourses { get; set; }
        public List<CourseType> ListOfCourseTypes { get; set; }
        public List<Language> ListOfLanguages { get; set; }
        public List<Payment> ListOfPayments { get; set; }
        public List<User> ListOfUsers { get; set; }
        public List<Teacher> ListOfTeachers { get; set; }
        public List<Student> ListOfStudents { get; set; }

        public School(string name, string address, string phoneNumber, string email, string webSite, string PIB, string identificationNumber, string accountNumber)
        {
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            WebSite = webSite;
            Pib = PIB;
            IdentificationNumber = identificationNumber;
            AccountNumber = accountNumber;
            ListOfCourses = new List<Course>();
            ListOfCourseTypes = new List<CourseType>();
            ListOfLanguages = new List<Language>();
            ListOfPayments = new List<Payment>();
            ListOfUsers = new List<User>();
            ListOfTeachers = new List<Teacher>();
            ListOfStudents = new List<Student>();
        }
    }
}
