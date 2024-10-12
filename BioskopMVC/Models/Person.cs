namespace BioskopMVC.Models
{
    public class Person
    {
        

        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Nationality { get; set; }

        public Person(int id, string firstName, string lastName, DateTime dateOfBirth, string nationality)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Nationality = nationality;
        }



    }
}
