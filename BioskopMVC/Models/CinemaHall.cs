﻿namespace BioskopMVC.Models
{
    public class CinemaHall
    {
        public int HallId { get; set; }
        public string HallName { get; set; }
        public int Capacity { get; set; }
        public string Type { get; set; }        
        public List<Movie> Movies { get; set; }
        public List<SeatingArea> SeatingAreas { get; set; }

        public int CinemaId { get; set; }   

        public Cinema Cinema { get; set; }

        public CinemaHall()
        {
            Movies = new List<Movie>();
            SeatingAreas = new List<SeatingArea>();
        }

        public CinemaHall(int hallId, string hallName, int capacity, string type, List<Movie> movies, List<SeatingArea> seatingAreas, int cinemaId )
        {
            HallId = hallId;
            HallName = hallName;
            Capacity = capacity;
            Type = type;
            Movies = movies;
            CinemaId =cinemaId;
            SeatingAreas = seatingAreas;
        }
    }
}
