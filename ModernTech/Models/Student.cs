namespace ModernTech.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string NumberOfRecordBook { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateOnly EnrollementDate { get; set; }
    }
}
