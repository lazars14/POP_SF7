using System.Collections.Generic;

namespace POP_SF7
{
    class UserCRUD
    {
        public UserCRUD() {}

        public static void addUser(List<User> usersList, User user)
        {
            usersList.Add(user);
        }

        public static bool deleteUser(List<User> usersList, int id)
        {
            for (int i = 0; i < usersList.Count; i++)
            {
                if (usersList[i].Id == id)
                {
                    usersList[i].Deleted = true;
                    return true;
                }
            }
            return false;
        }

        public void editUser(List<User> usersList, User editedUser)
        {
            for(int i = 0; i < usersList.Count; i++)
            {
                if(usersList[i].Id == editedUser.Id)
                {
                    usersList[i] = editedUser;
                }
            }
        }

        public static User findUserById(List<User> usersList, int id)
        {
            foreach(User user in usersList)
            {
                if(id == user.Id)
                {
                    return user;
                }
            }

            return null;
        }

        public static void sortUsers(School school)
        {
            List<User> list = school.ListOfUsers;
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

        public static void find(List<User> usersList, string userInput, string param, List<string> results)
        {
            foreach(User user in usersList)
            {
                switch (param)
                {
                    case "Ime":
                        if (param.Equals(user.FirstName))
                        {
                            results.Add(user.ToString());
                        }
                        break;
                    case "Prezime":
                        if (param.Equals(user.LastName))
                        {
                            results.Add(user.ToString());
                        }
                        break;
                    case "Korisnicko ime":
                        if (param.Equals(user.UserName))
                        {
                            results.Add(user.ToString());
                        }
                        break;
                }
            }
        }

        public static int getMaxId(List<User> usersList)
        {
            int maxId = 0;
            foreach(User user in usersList)
            {
                if(user.Id > maxId)
                {
                    maxId = user.Id;
                }
            }
            maxId += 1;
            return maxId;
        }
    }
}
