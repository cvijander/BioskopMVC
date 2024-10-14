namespace BioskopMVC.Models
{
    public class ActiveMovie
    {
        public int ActiveMovieId { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }

        public int CinemaHallId { get; set; }
        public CinemaHall CinemaHall { get; set; }

        public DateTime ShowTime { get; set; }  // Datum i vreme prikazivanja

        public DateTime StartDate { get; set; }  // Početak prikazivanja
        public DateTime EndDate { get; set; }    // Kraj prikazivanja

        public bool IsActive { get; set; }       // Status filma

        // Konstruktor bez parametara
        public ActiveMovie() { }

        // Konstruktor sa parametrima
        public ActiveMovie(int movieId, int cinemaId, int cinemaHallId, DateTime showTime, DateTime startDate, DateTime endDate, bool isActive)
        {
            MovieId = movieId;
            CinemaId = cinemaId;
            CinemaHallId = cinemaHallId;
            ShowTime = showTime;
            StartDate = startDate;
            EndDate = endDate;
            IsActive = isActive;
        }
    }
}
