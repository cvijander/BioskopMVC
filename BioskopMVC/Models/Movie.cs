using System.ComponentModel.DataAnnotations;

namespace BioskopMVC.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        [Required(ErrorMessage = "Naslov je obavezan.")]
        [StringLength(200, ErrorMessage = "Naslov ne moze biti duzi od 200 karaktera.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Duzina trajanja filmova je obavezna.")]
        public TimeSpan Duration { get; set; }

        [Required(ErrorMessage = "Datum izlaska filma je obavezan.")]
        [DataType(DataType.Date, ErrorMessage = "Unesite ispravan datum.")]

        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Opis filma je obavezan.")]
        [StringLength(1000, ErrorMessage = "Opis filma ne  moze biti duzi od 1000 karaktera.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Produkcijska kuca je obavezna.")]
        [StringLength(200, ErrorMessage = "Ime produkcijske kuce ne moze biti duze od 200 karaktera.")]
        public string ProductionCompany { get; set; }

        [Required(ErrorMessage = "Ocena filma je obavezna")]
        [Range(0.0, 10.0, ErrorMessage = "Ocena mora biti izmedju 0 i 10, sa decimalnim vrednostima")]
        public double Rating { get; set; }

        [Url(ErrorMessage ="Unesite ispravan URL.")]
        public string PosterUrl { get; set; }

        [Required(ErrorMessage ="Film mora imati barem jednog glumca.")]
        public List<Actor> Actors { get; set; } = new List<Actor>();


        [Required(ErrorMessage = "Reziser je obavezan.")]
        public Director Director { get; set; }

        public bool isActive { get; set; }

        public List<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();

        public Movie(int movieId, string title, TimeSpan duration, DateTime releaseDate, string description, string productionCompany, double rating, string posterUrl, List<MovieGenre> movieGenres, List<Actor> actors, Director director, bool isActive)
        {
            MovieId = movieId;
            Title = title;
            Duration = duration;
            ReleaseDate = releaseDate;
            Description = description;
            ProductionCompany = productionCompany;
            Rating = rating;
            PosterUrl = posterUrl;
            MovieGenres = movieGenres;
            Actors = actors;
            Director = director;
            this.isActive = isActive;
        }

        public Movie()
        {
            MovieGenres = new List<MovieGenre> ();
            Actors = new List<Actor>();
            Director = null;
        }
    }

}
