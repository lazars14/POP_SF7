using POP_SF7.School;
using System.Collections.ObjectModel;

namespace POP_SF7
{
    public class ApplicationA
    {
        public const string CONNECTION_STRING = @"Integrated Security=SSPI;
                                          Initial Catalog=TheLanguageSchool;
                                          Data Source=DUSAN\SQLEXPRESS";

        public const string DATABASE_ERROR_MESSAGE = "Doslo je do greske sa bazom!" + "\n" + "Greska ";

        public SchoolS SchoolS { get; set; }
        public int UserId { get; set; }

        public ObservableCollection<Language> Languages { get; set; }
        public ObservableCollection<CourseType> CourseTypes { get; set; }
        public ObservableCollection<Course> Courses { get; set; }
        public ObservableCollection<Payment> Payments { get; set; }
        public ObservableCollection<Teacher> Teachers { get; set; }
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Student> Students { get; set; }
        public ObservableCollection<TeacherTeachesLanguage> TeacherTeachesLanguageCollection { get; set; }
        public ObservableCollection<StudentAttendsCourse> StudentAttendsCourseCollection { get; set; }
        public ObservableCollection<TeacherTeachesCourse> TeacherTeachesCourseCollection { get; set; }

        private static ApplicationA instance = new ApplicationA();

        public static ApplicationA Instance
        {
            get
            {
                return instance;
            }
        }

        private ApplicationA()
        {
            SchoolS = SchoolS.LoadSchool();
            Users = new ObservableCollection<User>();
            Languages = new ObservableCollection<Language>();
            CourseTypes = new ObservableCollection<CourseType>();
            Courses = new ObservableCollection<Course>();
            Payments = new ObservableCollection<Payment>();
            Teachers = new ObservableCollection<Teacher>();
            Students = new ObservableCollection<Student>();
            TeacherTeachesLanguageCollection = new ObservableCollection<TeacherTeachesLanguage>();
            StudentAttendsCourseCollection = new ObservableCollection<StudentAttendsCourse>();
            TeacherTeachesCourseCollection = new ObservableCollection<TeacherTeachesCourse>();
        }

        public static void LoadAllDataAdministrator()
        {
            Language.Load();
            CourseType.Load();
            Teacher.Load();
            TeacherTeachesLanguage.Load();
            TeacherTeachesCourse.Load();
        }

        public static void LoadAllDataEmployee()
        {
            Language.Load();
            CourseType.Load();
            Teacher.Load();
            Student.Load();
            Payment.Load();
            Course.Load();
            TeacherTeachesLanguage.Load();
            StudentAttendsCourse.Load();
            TeacherTeachesCourse.Load();
        }
    }
}
