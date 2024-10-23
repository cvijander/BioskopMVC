using System.ComponentModel.DataAnnotations;

namespace BioskopMVC.Models
{
    public class Director : Person
    {

        [Required(ErrorMessage ="Opis je obavezan.")]
        [StringLength(1000, ErrorMessage ="Duzina opisa ne moze biti veca od 1000 karaktera.")]
        public string Filmography { get; set; }


        [StringLength(500,ErrorMessage ="Duzina polja za nagrade ne moze biti veca od 500 karaktera.")]
        public string Awards { get; set; }


        [StringLength(255, ErrorMessage = "Duzina polja za nagrade ne moze biti veca od 500 karaktera.")]
        public string Style { get; set; }


        [Range(1,100, ErrorMessage = "Broj godina mora biti u opsegu od 1 do 100.")]
        public int YearsDirecting { get; set; }


        [StringLength(500, ErrorMessage = "Duzina polja za znacajne filmove ne moze biti veca od 500 karaktera.")]
        public string NotableFilms { get;set; }

        public List<Movie> Movies { get; set; }=new List<Movie>();

        public Director(int personId, string firstName, string lastName, DateTime dateOfBirth, int nationalityId, string filmography, string awards, string style, int yearsDirecting, string notableFilms) : base(personId, firstName, lastName, dateOfBirth, nationalityId)
        {
            
            Filmography = filmography;
            Awards =awards;
            Style =style;
            YearsDirecting = yearsDirecting;
            NotableFilms =notableFilms;

            
        }

        public Director() { }
    }
}
