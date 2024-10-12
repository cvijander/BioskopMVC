namespace BioskopMVC.Models
{
    public class Movie
    {
        public int MovieId { get; set; }

        public string Title { get; set; }

        public TimeSpan Duration { get; set; }
        public string Description { get; set; }

        public List<Actor> Actors { get; set; }

        public List<Director> Director { get; set; }
    }
}
