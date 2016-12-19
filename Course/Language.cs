
using POP_SF7;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace POP_SF7
{
    public class Language : INotifyPropertyChanged, ICloneable, IDataErrorInfo
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        private bool deleted;
        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; OnPropertyChanged("Deleted"); }
        }

        public Language() { }

        public Language(int id, string name, bool deleted)
        {
            Id = id;
            Name = name;
            Deleted = deleted;
        }

        #region Database operations

        public static void Load()
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                DataSet dataSet = new DataSet();

                SqlCommand loadCommand = connection.CreateCommand();
                loadCommand.CommandText = @"Select * From Language;";
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = loadCommand;
                dataAdapter.Fill(dataSet, "Language");

                foreach (DataRow row in dataSet.Tables["Language"].Rows)
                {
                    Language lang = new Language();
                    lang.Id = (int)row["Language_Id"];
                    lang.Name = (string)row["Language_Name"];
                    lang.Deleted = (bool)row["Language_Deleted"];

                    ApplicationA.Instance.Languages.Add(lang);
                }
            }
        }

        public static void Add(Language language)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand addCommand = connection.CreateCommand();
                addCommand.CommandText = @"Insert Into Language Values(@Name, @Deleted);";

                addCommand.Parameters.Add(new SqlParameter("@Name", language.Name));
                addCommand.Parameters.Add(new SqlParameter("@Deleted", language.Deleted));

                addCommand.ExecuteNonQuery();
            }
        }

        public static void Edit(Language language)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand addCommand = connection.CreateCommand();
                addCommand.CommandText = @"Update Language Set Language_Name=@Name, Language_Deleted=@Deleted Where Language_Id=@Id;";

                addCommand.Parameters.Add(new SqlParameter("@Name", language.Name));
                addCommand.Parameters.Add(new SqlParameter("@Deleted", language.Deleted));
                addCommand.Parameters.Add(new SqlParameter("@Id", language.Id));

                addCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(Language language)
        {
            using (SqlConnection connection = new SqlConnection(ApplicationA.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand addCommand = connection.CreateCommand();
                addCommand.CommandText = @"Delete From Language Where Language_Id=@Id;";

                addCommand.Parameters.Add(new SqlParameter("@Id", language.Id));

                addCommand.ExecuteNonQuery();
            }
        }

        #endregion

        #region IDataErrorInfo

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch(columnName)
                {
                    case "Name":
                        if (string.IsNullOrEmpty(Name)) return "Morate da popunite naziv jezika!";
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
            Language languageCopy = new Language();
            languageCopy.Id = Id;
            languageCopy.Name = Name;
            languageCopy.Deleted = Deleted;

            return languageCopy;
        }

        #endregion
    }
}
