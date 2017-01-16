using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace POP_SF7.DB
{
    public class SchoolDAO
    {
        public static SchoolS LoadSchool()
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                DataSet dataSet = new DataSet();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Select * From School;";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                try
                {
                    dataAdapter.Fill(dataSet, "School");

                    DataRow row = dataSet.Tables["School"].Rows[0];

                    SchoolS school = new SchoolS();
                    school.IdentificationNumber = (string)row["School_IdentificationNumber"];
                    school.Name = (string)row["School_Name"];
                    school.Address = (string)row["School_Address"];
                    school.PhoneNumber = (string)row["School_PhoneNumber"];
                    school.Email = (string)row["School_Email"];
                    school.WebSite = (string)row["School_WebSite"];
                    school.Pib = (string)row["School_Pib"];
                    school.AccountNumber = (string)row["School_AccountNumber"];

                    return school;
                }
                catch (SqlException e)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE);
                    ApplicationA.WriteToLog(e.StackTrace);
                }
                catch (InvalidOperationException a)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE);
                    ApplicationA.WriteToLog(a.StackTrace);
                }
                catch (ArgumentException g)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE);
                    ApplicationA.WriteToLog(g.StackTrace);
                }
                catch (NullReferenceException n)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE);
                    ApplicationA.WriteToLog(n.StackTrace);
                }

                return null;
            }
        }

        public static bool UpdateSchool(SchoolS school)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                bool valid = false;

                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"Update School Set School_Name=@Name, School_Address=@Address, School_PhoneNumber=@PhoneNumber, School_Email=@Email, School_WebSite=@WebSite, School_Pib=@Pib, School_IdentificationNumber=@IdentificationNumber, School_AccountNumber=@AccountNumber Where School_Id=1;";

                try
                {
                    command.Parameters.Add(new SqlParameter("@Name", school.Name));
                    command.Parameters.Add(new SqlParameter("@Address", school.Address));
                    command.Parameters.Add(new SqlParameter("@PhoneNumber", school.PhoneNumber));
                    command.Parameters.Add(new SqlParameter("@Email", school.Email));
                    command.Parameters.Add(new SqlParameter("@WebSite", school.WebSite));
                    command.Parameters.Add(new SqlParameter("@Pib", school.Pib));
                    command.Parameters.Add(new SqlParameter("@IdentificationNumber", school.IdentificationNumber));
                    command.Parameters.Add(new SqlParameter("@AccountNumber", school.AccountNumber));

                    command.ExecuteNonQuery();

                    valid = true;
                }
                catch (SqlException e)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE);
                    ApplicationA.WriteToLog(e.StackTrace);
                }
                catch (InvalidOperationException a)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE);
                    ApplicationA.WriteToLog(a.StackTrace);
                }
                catch (ArgumentException g)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE);
                    ApplicationA.WriteToLog(g.StackTrace);
                }
                catch (NullReferenceException n)
                {
                    MessageBox.Show(ApplicationA.DATABASE_ERROR_MESSAGE);
                    ApplicationA.WriteToLog(n.StackTrace);
                }

                return valid;
            }
        }
    }
}
