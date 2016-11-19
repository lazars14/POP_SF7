namespace POP_SF7
{
    public enum Role { Administrator, Employee }

    public class User : Person
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Role UserRole { get; set; }

        public User(int id, string firstName, string lastName, string jmbg, string personAddress, string userName, string password, Role role, bool deleted) : base(id, firstName, lastName, jmbg, personAddress, deleted)
        {
            UserName = userName;
            Password = password;
            UserRole = role;
        }
    }
}
