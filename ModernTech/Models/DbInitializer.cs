namespace ModernTech.Models
{
    public class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            if (context.Students.Any())
            {
                return;
            }

            var students = new Student[]
            {
                new Student { FullName = "Ivan Ivanov", NumberOfRecordBook = "12345", BirthDate = new DateOnly(2000, 1, 1), EnrollementDate = new DateOnly(2018, 9, 1)},
                new Student { FullName = "Petr Petrov", NumberOfRecordBook = "67890", BirthDate = new DateOnly(2001, 2, 2), EnrollementDate = new DateOnly(2019, 9, 1)},
            };
            context.Students.AddRange(students);

            //foreach (var student in students)
            //{
            //    context.Students.Add(student);
            //}

            context.SaveChanges(); 
        }
    }
}
