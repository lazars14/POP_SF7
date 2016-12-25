﻿using POP_SF7.Helpers;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace POP_SF7
{
    public class Payment : INotifyPropertyChanged, ICloneable, IDataErrorInfo
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private Course course;
        public Course Course
        {
            get { return course; }
            set { course = value; OnPropertyChanged("Course"); }
        }

        private Student student;
        public Student Student
        {
            get { return student; }
            set { student = value; OnPropertyChanged("Student"); }
        }

        private double amount;
        public double Amount
        {
            get { return amount; }
            set { amount = value; OnPropertyChanged("Amount"); }
        }

        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged("Date"); }
        }

        private bool deleted;
        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; OnPropertyChanged("Deleted"); }
        }

        public Payment() { Amount = 0; }

        public Payment(int id, Course course, Student student, double amount, DateTime date, bool deleted)
        {
            Id = id;
            Course = course;
            Student = student;
            Amount = amount;
            Date = date;
            Deleted = deleted;
        }

        #region Database operations

        public static void Load()
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                DataSet dataSet = new DataSet();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Select * From Payment;";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                try
                {
                    dataAdapter.Fill(dataSet, "Payment");

                    foreach (DataRow row in dataSet.Tables["Payment"].Rows)
                    {
                        Payment payment = new Payment();
                        payment.Id = (int)row["Payment_Id"];
                        payment.Amount = (double)row["Payment_Amount"];
                        payment.Date = (DateTime)row["Payment_Date"];
                        payment.Deleted = (bool)row["Payment_Deleted"];

                        ApplicationA.Instance.Payments.Add(payment);
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + e.GetType());
                }
                catch (InvalidOperationException a)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + a.GetType());
                }
                catch (ArgumentException g)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + g.GetType());
                }
                catch (NullReferenceException n)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + n.GetType());
                }
            }
        }

        public static void Add(Payment payment)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Insert Into Payment Values(@Course,@Student,@Amount,@Date,@Deleted);";

                command.Parameters.Add(new SqlParameter("@Course", payment.Course.Id));
                command.Parameters.Add(new SqlParameter("@Student", payment.Student.Id));
                command.Parameters.Add(new SqlParameter("@Amount", payment.Amount));
                command.Parameters.Add(new SqlParameter("@Date", payment.Date));
                command.Parameters.Add(new SqlParameter("@Deleted", payment.Deleted));

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + e.GetType());
                }
                catch (InvalidOperationException a)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + a.GetType());
                }
                catch (ArgumentException g)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + g.GetType());
                }
                catch (NullReferenceException n)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + n.GetType());
                }
            }
        }

        public static void Edit(Payment payment)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Update Payment Set Payment_Course=@Course, Payment_Student=@Student, Payment_Amount=@Amount, Payment_Date=@Date, Payment_Deleted=@Deleted Where Payment_Id=@Id;";

                command.Parameters.Add(new SqlParameter("@Course", payment.Course.Id));
                command.Parameters.Add(new SqlParameter("@Student", payment.Student.Id));
                command.Parameters.Add(new SqlParameter("@Amount", payment.Amount));
                command.Parameters.Add(new SqlParameter("@Date", payment.Date));
                command.Parameters.Add(new SqlParameter("@Deleted", payment.Deleted));
                command.Parameters.Add(new SqlParameter("@Id", payment.Id));

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + e.GetType());
                }
                catch (InvalidOperationException a)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + a.GetType());
                }
                catch (ArgumentException g)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + g.GetType());
                }
                catch (NullReferenceException n)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + n.GetType());
                }
            }
        }

        public static void Delete(Payment payment)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Update Payment Set Payment_Deleted=1 Where Payment_Id=@Id;";

                command.Parameters.Add(new SqlParameter("@Id", payment.Id));

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + e.GetType());
                }
                catch (InvalidOperationException a)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + a.GetType());
                }
                catch (ArgumentException g)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + g.GetType());
                }
                catch (NullReferenceException n)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE + n.GetType());
                }
            }
        }

        #endregion

        #region IDataErrorInfo

        public string Error
        {
            get
            {
                return "";
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch(columnName)
                {
                    case "Amount":
                        bool isDouble = ValidationHelper.isDouble(Amount.ToString());
                        if (!isDouble) return ValidationHelper.Numeric;
                        break;
                }
                return "";
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

        #region ICloneable

        public object Clone()
        {
            Payment paymentCopy = new Payment();
            paymentCopy.Id = Id;
            paymentCopy.Course = Course;
            paymentCopy.Student = Student;
            paymentCopy.Amount = Amount;
            paymentCopy.Date = Date;
            paymentCopy.Deleted = Deleted;

            return paymentCopy;
        }

        #endregion
    }
}
