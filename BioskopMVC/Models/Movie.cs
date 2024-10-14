namespace BioskopMVC.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public string ProductionCompany { get; set; }
        public double Rating { get; set; }
        public string PosterUrl { get; set; }        
        public List<Actor> Actors { get; set; }
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
