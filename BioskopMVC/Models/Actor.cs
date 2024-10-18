using System.ComponentModel.DataAnnotations;

namespace BioskopMVC.Models
{
    public class Actor: Person
    {

        [Required(ErrorMessage ="Opis je obavezan.")]
        [StringLength(1000, ErrorMessage ="Duzina opisa ne moze biti veca od 1000 karaktera.")]
        public string Description { get; set; }

        public List<Movie> Movies { get; set; } = new List<Movie>();

        public Actor(int personId, string firstName, string lastName, DateTime dateOfBirth, int nationalityId, string description) : base(personId, firstName, lastName, dateOfBirth, nationalityId)
        {
            
            Description = description;
        }

        public Actor() { }

    }
}
