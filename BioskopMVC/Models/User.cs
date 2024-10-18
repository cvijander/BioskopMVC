using System.ComponentModel.DataAnnotations;

namespace BioskopMVC.Models
{
    public class User : Person
    {
        [Required(ErrorMessage ="Email je obavezan.")]
        [EmailAddress(ErrorMessage ="Neispravan format email adrese.")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Lozinka je obavezna.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }


        public User(int personId, string firstName, string lastName, DateTime dateOfBirth, int nationalityId, string email, string password, DateTime registrationDate) : base(personId, firstName, lastName, dateOfBirth, nationalityId)
        {
            Email = email;
            Password = password;
            RegistrationDate = registrationDate;
        }

        public User () { }
        
    }
}
