namespace BioskopMVC.Models
{
    public class MovieGenre
    {
        public int MovieGenreId { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}
