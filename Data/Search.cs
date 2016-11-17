using System.Collections.Generic;

namespace POP_SF7
{
    class Search
    {
        public static string find(List<dynamic> sourceList, string userInput, string param)
        {
            foreach (var someEntity in sourceList)
            {
                switch (param)
                {
                    case "Ime":
                        if(someEntity.FirstName.Equals(userInput))
                        {
                            return someEntity.ToString();
                        }
                        break;
                    case "Prezime":
                        if (someEntity.LastName.Equals(userInput))
                        {
                            return someEntity.ToString();
                        }
                        break;
                    case "Jmbg":
                        if (someEntity.JMBG.Equals(userInput))
                        {
                            return someEntity.ToString();
                        }
                        break;
                    case "Korisnicko ime":
                        if (someEntity.UserName.Equals(userInput))
                        {
                            return someEntity.ToString();
                        }
                        break;
                }
            }

            return null;
        }
    }
}
