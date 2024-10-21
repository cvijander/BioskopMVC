using System.ComponentModel.DataAnnotations;

namespace BioskopMVC.Models
{
    public class Actor: Person
    {

        [Required(ErrorMessage ="Biografija je obavezna.")]
        [StringLength(1000, ErrorMessage ="Duzina opisa ne moze biti veca od 1000 karaktera.")]
        public string Biography { get; set; }

        public string FamousRole { get; set; }

        public string Awards {  get; set; }

        public DateTime? DateOfDeath { get; set; }

        public List<Movie> Movies { get; set; } = new List<Movie>();

        public Actor(int personId, string firstName, string lastName, DateTime dateOfBirth, int nationalityId, string biography, string famousRole = null,string awards = null,DateTime? dateOfDeath = null  ) : base( personId, firstName, lastName, dateOfBirth, nationalityId)
        {
            
            Biography = biography;
            FamousRole = famousRole;
            Awards = awards;
            DateOfDeath = dateOfDeath;
        }

        public Actor() { }

    }
}
