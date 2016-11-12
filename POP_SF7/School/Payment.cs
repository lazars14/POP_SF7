namespace POP_SF7
{
    class Payment
    {
        public int Id { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }
        public double PaymentAmount { get; set; }
        public bool Deleted { get; set; }

        public Payment(int paymentId, Course course, Student student, double paymentAmount, bool deleted)
        {
            Id = paymentId;
            Course = course;
            Student = student;
            PaymentAmount = paymentAmount;
            Deleted = deleted;
        }
    }
}
