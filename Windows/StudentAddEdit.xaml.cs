using POP_SF7.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System;
using POP_SF7.Helpers;
using System.ComponentModel;
using System.Windows.Data;
using POP_SF7.School;

namespace POP_SF7
{
    /// <summary>
    /// Interaction logic for PersonAddEdit.xaml
    /// </summary>
    public partial class StudentAddEdit : Window
    {
        public ICollectionView CoursesView { get; set; }

        public Student StudentS { get; set; }
        public Decider Decider { get; set; }

        public string labelAddStudent = "Dodavanje novog ucenika";
        public string labelEditStudent = "Izmena postojeceg ucenika";
        
        public StudentAddEdit(Student student, Decider decider)
        {
            InitializeComponent();

            StudentS = student;
            Decider = decider;

            setupWindow();
        }

        private void setupWindow()
        {
            DataContext = StudentS;
            personInfo.descriptionlbl.Text = (Decider == Decider.ADD) ? labelAddStudent : labelEditStudent;

            CoursesView = CollectionViewSource.GetDefaultView(StudentS.ListOfCourses);
            coursesdg.ItemsSource = CoursesView;
            coursesdg.IsSynchronizedWithCurrentItem = true;
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if (personInfo.nametb.Text.Equals("") || personInfo.lastnametb.Text.Equals("") || personInfo.addresstb.Text.Equals("") || personInfo.jmbgtb.Text.Equals(""))
            {
                MessageBox.Show(ApplicationA.FILL_ALL_FIELDS_WARNING);
            }
            else
            {
                if (Decider == Decider.ADD)
                {
                    if (Student.Add(StudentS))
                    {
                        StudentS.Id = ApplicationA.Instance.Students.Count() + 1;
                        ApplicationA.Instance.Students.Add(StudentS);
                    }
                }
                else
                {
                    if (!Student.Edit(StudentS))
                    {
                        cancelbtn_Click(null, null);
                    }
                }
                Close();
            }
        }

        private void coursesdg_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            LoadColumnsHelper.LoadCourse(e);
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addCoursebtn_Click(object sender, RoutedEventArgs e)
        {
            SelectCourseLangStud scl = new SelectCourseLangStud(this);
            scl.Show();
        }

        private void undeleteCoursebtn_Click(object sender, RoutedEventArgs e)
        {
            Course selectedCourse = CoursesView.CurrentItem as Course;
            if (selectedCourse == null)
            {
                MessageBox.Show("Morate da selektujete kurs koji zelite da obrisete!");
            }
            else
            {
                if (selectedCourse.Deleted == false) // ovde ide provera na osnovu boje
                {
                    MessageBox.Show("Izabrani kurs nije obrisan!");
                }
                else
                {
                    var result = MessageBox.Show("Da li ste sigurni da hocete da povratite dati kurs za ovog ucenika?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        foreach (StudentAttendsCourse sac in ApplicationA.Instance.StudentAttendsCourseCollection)
                        {
                            if (sac.StudentId == StudentS.Id && sac.CourseId == selectedCourse.Id)
                            {
                                if (StudentAttendsCourse.UnDelete(sac))
                                {
                                    sac.Deleted = false;
                                    // boja - default
                                }
                            }
                        }
                    }
                }
            }
        }

        private void deleteCoursebtn_Click(object sender, RoutedEventArgs e)
        {
            Course selectedCourse = CoursesView.CurrentItem as Course;
            if(selectedCourse == null)
            {
                MessageBox.Show("Morate da selektujete kurs koji zelite da obrisete!");
            }
            else
            {
                if(selectedCourse.Deleted == true) // ovde ide provera na osnovu boje
                {
                    MessageBox.Show("Izabrani kurs je vec obrisan!");
                }
                else
                {
                    var result = MessageBox.Show("Da li ste sigurni da hocete da obrisete dati kurs za ovog ucenika?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        foreach(StudentAttendsCourse sac in ApplicationA.Instance.StudentAttendsCourseCollection)
                        {
                            if(sac.StudentId == StudentS.Id && sac.CourseId == selectedCourse.Id)
                            {
                                if(StudentAttendsCourse.Delete(sac))
                                {
                                    sac.Deleted = true;
                                    //boja - crvena
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
