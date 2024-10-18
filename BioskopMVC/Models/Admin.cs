using System.ComponentModel.DataAnnotations;

namespace BioskopMVC.Models
{
    public class Admin: User
    {

        [Required(ErrorMessage = "Prava administatora su obavezna.")]
        [StringLength(100, ErrorMessage = "Prava administrtatora ne mogu biti duza od 100 karaktera.")]
        public string AdminPrivileges { get; set; }

        public Admin(string adminPrivileges,  int personId, string firstName, string lastName, DateTime dateOfBirth, int nationalityId, string email, string password, DateTime registrationDate) : base(personId, firstName, lastName, dateOfBirth, nationalityId, email, password, registrationDate)
        {
            AdminPrivileges = adminPrivileges;
        }

        public Admin() { }


    }
}
