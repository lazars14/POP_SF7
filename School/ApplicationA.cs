using POP_SF7.DB;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace POP_SF7
{
    public class ApplicationA
    {
        public const string CONNECTION_STRING = @"Integrated Security=SSPI;
                                          Initial Catalog=TheLanguageSchool;
                                          Data Source=DUSAN\SQLEXPRESS";

        public const string FILE_NAME = @"..\..\Log.txt";

        public const string DATABASE_ERROR_MESSAGE = "Doslo je do greske sa bazom!";

        public const string FILL_ALL_FIELDS_WARNING = "Morate da popunite sva polja!";

        public const string FILL_FIELD = "Popuniti";

        public SchoolS SchoolS { get; set; }

        public static User LoggedUser { get; set; }
        public int UserId { get; set; }

        public static bool AdminDataLoaded = false;
        public static bool EmployeeDataLoaded = false;

        public ObservableCollection<Language> Languages { get; set; }
        public ObservableCollection<CourseType> CourseTypes { get; set; }
        public ObservableCollection<Course> Courses { get; set; }
        public ObservableCollection<Payment> Payments { get; set; }
        public ObservableCollection<Teacher> Teachers { get; set; }
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Student> Students { get; set; }

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
            SchoolS = SchoolDAO.LoadSchool();
            LoggedUser = new User(0);

            Users = new ObservableCollection<User>();
            Languages = new ObservableCollection<Language>();
            CourseTypes = new ObservableCollection<CourseType>();
            Courses = new ObservableCollection<Course>();
            Payments = new ObservableCollection<Payment>();
            Teachers = new ObservableCollection<Teacher>();
            Students = new ObservableCollection<Student>();
        }

        public static void WriteToLog(string stackTrace)
        {
            using (FileStream fs = new FileStream(FILE_NAME, FileMode.Append))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                string delimiter = "|";
                string nextLine = "\n";
                sw.Write(DateTime.Now + delimiter + LoggedUser.ToString() + nextLine + stackTrace + nextLine);
            }
        }

        public static bool LoadAllDataAdministrator()
        {
            bool valid = true;

            valid = LanguageDAO.Load();
            valid = CourseTypeDAO.Load();
            valid = CourseDAO.Load();
            valid = TeacherDAO.Load();
            valid = TeacherTeachesLanguageDAO.Load();
            valid = TeacherTeachesCourseDAO.Load();

            AdminDataLoaded = true;

            return valid;

            /*
            MessageBox.Show(ApplicationA.Instance.Languages.Count.ToString() + " Jezici");
            MessageBox.Show(ApplicationA.Instance.CourseTypes.Count.ToString() + " Tipovi kurseva");
            MessageBox.Show(ApplicationA.Instance.Courses.Count.ToString() + " Kursevi");
            MessageBox.Show(ApplicationA.Instance.Teachers.Count.ToString() + " Nastavnici");
            */
        }

        public static bool LoadAllDataEmployee()
        {
            bool valid = true;

            valid = LanguageDAO.Load(); 
            valid = CourseTypeDAO.Load(); 
            valid = TeacherDAO.Load(); 
            valid = StudentDAO.Load(); 
            valid = PaymentDAO.Load();
            valid = CourseDAO.Load(); 
            valid = TeacherTeachesLanguageDAO.Load(); 
            valid = StudentAttendsCourseDAO.Load();
            valid = TeacherTeachesCourseDAO.Load();

            EmployeeDataLoaded = true;

            return valid;

            /*
            MessageBox.Show(ApplicationA.Instance.Languages.Count.ToString() + " Jezici");
            MessageBox.Show(ApplicationA.Instance.CourseTypes.Count.ToString() + " Tipovi kurseva");
            MessageBox.Show(ApplicationA.Instance.Teachers.Count.ToString() + " Nastavnici");
            MessageBox.Show(ApplicationA.Instance.Students.Count.ToString() + " Ucenici");
            MessageBox.Show(ApplicationA.Instance.Payments.Count.ToString() + " Uplate");
            MessageBox.Show(ApplicationA.Instance.Courses.Count.ToString() + " Kursevi");
            */
        }

        public static bool LoadEmployeeAdminDifference()
        {
            bool valid = true;

            valid = StudentDAO.Load();
            valid = PaymentDAO.Load();
            valid = StudentAttendsCourseDAO.Load();

            EmployeeDataLoaded = true;

            return valid;

            /*
            MessageBox.Show(ApplicationA.Instance.Students.Count.ToString() + " Ucenici");
            MessageBox.Show(ApplicationA.Instance.Payments.Count.ToString() + " Uplate");
            MessageBox.Show(ApplicationA.Instance.StudentAttendsCourseCollection.Count.ToString() + " SAC");
            */
        }
    }
}
