using System.Collections.Generic;

namespace POP_SF7
{
    class LanguageCRUD
    {
        public void addLanguage(List<Language> languagesList, Language language)
        {
            languagesList.Add(language);
        }

        public static bool deleteLanguage(List<Language> languagesList, int id)
        {
            for(int i = 0; i < languagesList.Count; i++)
            {
                if(languagesList[i].Id == id)
                {
                    languagesList[i].Deleted = true;
                    return true;
                }
            }
            return false;
        }

        public void editLanguage(List<Language> languagesList, Language editedLanguage)
        {

        }

        public static void sortLanguages(School school)
        {
            List<Language> list = school.ListOfLanguages;
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[i].Id > list[j].Id)
                    {
                        var tmp = list[i];
                        list[i] = list[j];
                        list[j] = tmp;
                    }
                }
            }
        }

        public Language findLanguageById(List<Language> languagesList, int id)
        {
            foreach(Language language in languagesList)
            {
                if(language.Id == id)
                {
                    return language;
                }
            }

            return null;
        }
    }
}
