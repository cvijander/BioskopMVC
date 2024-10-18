using System.ComponentModel.DataAnnotations;

namespace BioskopMVC.Models
{
    public class Nationality
    {
        public int NationalityId { get; set; }

        [Required(ErrorMessage ="Nazvi nacionalnosti je obavezan.")]
        [StringLength(50, ErrorMessage ="Naziv ne sme biti duzi od 50 karaktera.")]
        public string Name { get; set; }  
    }
}
