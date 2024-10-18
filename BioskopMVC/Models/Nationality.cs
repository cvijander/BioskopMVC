using System.ComponentModel.DataAnnotations;

namespace BioskopMVC.Models
{
    public class Nationality
    {
        public int NationalityID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }  
    }
}
