namespace POP_SF7
{
    class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Jmbg { get; set; }
        public string PersonAddress { get; set; }
        public bool Deleted { get; set; }

        public Person(int id, string firstName, string lastName, string jmbg, string personAddress, bool deleted)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Jmbg = jmbg;
            PersonAddress = personAddress;
            Deleted = deleted;
        }
    }
}
