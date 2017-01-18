using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace POP_SF7.Helpers
{
    public class LoadColumnsHelper
    {
        // ili da bude u svakoj klasi posebno za tabelu?

        public static void LoadLanguage(DataGridAutoGeneratingColumnEventArgs e)
        {
            switch ((string)e.Column.Header)
            {
                case "Id":
                    e.Cancel = true;
                    break;
                case "Name":
                    e.Column.Header = "Ime";
                    break;
                case "Deleted":
                    e.Column.Header = "Obrisano";
                    break;
                case "Error":
                    e.Cancel = true;
                    break;
            }
        }

        public static void LoadCourseType(DataGridAutoGeneratingColumnEventArgs e)
        {
            switch ((string)e.Column.Header)
            {
                case "Id":
                    e.Cancel = true;
                    break;
                case "Name":
                    e.Column.Header = "Ime";
                    break;
                case "Deleted":
                    e.Column.Header = "Obrisano";
                    break;
                case "Error":
                    e.Cancel = true;
                    break;
            }
        }

        public static void LoadPayment(DataGridAutoGeneratingColumnEventArgs e)
        {
            switch ((string)e.Column.Header)
            {
                case "Id":
                    e.Cancel = true;
                    break;
                case "Course":
                    e.Cancel = true;
                    break;
                case "Student":
                    e.Cancel = true;
                    break;
                case "Amount":
                    e.Column.Header = "Iznos";
                    break;
                case "Date":
                    e.Column.Header = "Datum";
                    break;
                case "Deleted":
                    e.Column.Header = "Obrisano";
                    break;
                case "Error":
                    e.Cancel = true;
                    break;
            }
        }

        public static void LoadCourse(DataGridAutoGeneratingColumnEventArgs e)
        {
            switch ((string)e.Column.Header)
            {
                case "Id":
                    e.Cancel = true;
                    break;
                case "Language":
                    e.Cancel = true;
                    break;
                case "CourseType":
                    e.Cancel = true;
                    break;
                case "Price":
                    e.Column.Header = "Cena";
                    break;
                case "Teacher":
                    e.Cancel = true;
                    break;
                case "StartDate":
                    e.Column.Header = "Datum pocetka";
                    break;
                case "EndDate":
                    e.Column.Header = "Datum kraja";
                    break;
                case "Deleted":
                    e.Column.Header = "Obrisan";
                    break;
                case "ListOfStudents":
                    e.Cancel = true;
                    break;
                case "ListOfDeletedStudents":
                    e.Cancel = true;
                    break;
                case "Error":
                    e.Cancel = true;
                    break;
            }
        }

        public static void LoadUser(DataGridAutoGeneratingColumnEventArgs e)
        {
            switch ((string)e.Column.Header)
            {
                case "Id":
                    e.Cancel = true;
                    break;
                case "FirstName":
                    e.Column.Header = "Ime";
                    break;
                case "LastName":
                    e.Column.Header = "Prezime";
                    break;
                case "Address":
                    e.Column.Header = "Adresa";
                    break;
                case "Jmbg":
                    e.Column.Header = "JMBG";
                    break;
                case "Deleted":
                    e.Column.Header = "Obrisan";
                    break;
                case "UserName":
                    e.Column.Header = "Korisnicko ime";
                    break;
                case "Password":
                    e.Column.Header = "Lozinka";
                    break;
                case "UserRole":
                    e.Column.Header = "Uloga";
                    break;
                case "Error":
                    e.Cancel = true;
                    break;
            }
        }

        public static void LoadTeacher(DataGridAutoGeneratingColumnEventArgs e)
        {
            switch ((string)e.Column.Header)
            {
                case "Id":
                    e.Cancel = true;
                    break;
                case "FirstName":
                    e.Column.Header = "Ime";
                    break;
                case "LastName":
                    e.Column.Header = "Prezime";
                    break;
                case "Address":
                    e.Column.Header = "Adresa";
                    break;
                case "Jmbg":
                    e.Column.Header = "JMBG";
                    break;
                case "Deleted":
                    e.Column.Header = "Obrisan";
                    break;
                case "ListOfLanguages":
                    e.Cancel = true;
                    break;
                case "ListOfCourses":
                    e.Cancel = true;
                    break;
                case "ListOfDeletedLanguages":
                    e.Cancel = true;
                    break;
                case "ListOfDeletedCourses":
                    e.Cancel = true;
                    break;
                case "Error":
                    e.Cancel = true;
                    break;
                case "FullName":
                    e.Cancel = true;
                    break;
            }
        }

        public static void LoadStudent(DataGridAutoGeneratingColumnEventArgs e)
        {
            switch ((string)e.Column.Header)
            {
                case "Id":
                    e.Cancel = true;
                    break;
                case "FirstName":
                    e.Column.Header = "Ime";
                    break;
                case "LastName":
                    e.Column.Header = "Prezime";
                    break;
                case "Address":
                    e.Cancel = true;
                    break;
                case "Jmbg":
                    e.Column.Header = "JMBG";
                    break;
                case "Deleted":
                    e.Column.Header = "Obrisan";
                    break;
                case "ListOfDeletedCourses":
                    e.Cancel = true;
                    break;
                case "ListOfCourses":
                    e.Cancel = true;
                    break;
                case "Error":
                    e.Cancel = true;
                    break;
            }
        }
    }
}
