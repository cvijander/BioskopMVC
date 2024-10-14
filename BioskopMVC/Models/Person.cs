using System.ComponentModel.DataAnnotations;

namespace BioskopMVC.Models
{
    public class Person
    {
        

        public int PersonId { get; set; }
        
        public string FirstName { get; set; }

        
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Nationality { get; set; }

        public Person(int personId, string firstName, string lastName, DateTime dateOfBirth, string nationality)
        {
            PersonId = personId;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Nationality = nationality;
        }

        public Person() { }




    }
}
