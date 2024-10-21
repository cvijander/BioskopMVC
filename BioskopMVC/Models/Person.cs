using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioskopMVC.Models
{
    public class Person
    {

        [Key]
        public int PersonId { get; set; }

        [Required(ErrorMessage ="Ime je obavezno.")]
        [StringLength(50, ErrorMessage ="Duzina imena ne moze biti vise od 50 karaktera.")]
        public string FirstName { get; set; }


        [Required(ErrorMessage ="Prezime je obavezno.")]
        [StringLength(50, ErrorMessage ="Duzina prezimena ne moze biti vise od 50 karaktera." )]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Datum rodjenja je obavezan.")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
                        
        public int NationalityId { get; set; }

        [BindNever]
        public Nationality Nationality { get; set; }

        public Person(int personId, string firstName, string lastName, DateTime dateOfBirth, int nationalityId)
        {
            PersonId = personId;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            NationalityId = nationalityId;
        }

        public Person() { }

    }
}
