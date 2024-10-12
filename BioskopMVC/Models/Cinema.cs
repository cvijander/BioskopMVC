namespace BioskopMVC.Models
{
    public class Cinema
    {
        public int CinemaId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Adress { get; set; }
        public string Location { get; set; }

        public int Capacity { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }

        public List<CinemaHall> CinemaHalls { get; set; }
    }
}
