namespace POP_SF7
{
    class CourseType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }

        public CourseType(int id, string name, bool deleted)
        {
            Id = id;
            Name = name;
            Deleted = deleted;
        }
    }
}
