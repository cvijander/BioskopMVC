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
        public List<string> Genres { get; set; }
        public List<Actor> Actors { get; set; }
        public Director Directors { get; set; }


        public Movie(int movieId, string title, TimeSpan duration, DateTime releaseDate, string description, string productionCompany, double rating, string posterUrl, List<string> genres, List<Actor> actors, Director directors)
        {
            MovieId = movieId;
            Title = title;
            Duration = duration;
            ReleaseDate = releaseDate;
            Description = description;
            ProductionCompany = productionCompany;
            Rating = rating;
            PosterUrl = posterUrl;
            Genres = genres;
            Actors = actors;
            Directors = directors;
        }

        public Movie()
        {
            Genres = new List<string>();
            Actors = new List<Actor>();
            Directors = null;
        }
    }

}
