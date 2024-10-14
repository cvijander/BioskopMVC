namespace BioskopMVC.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        
        public int ActiveMovieId { get; set; }

        public ActiveMovie ActiveMovie { get; set; }
                
        public List<SeatingArea> SeatingAreas { get; set; }
        public double TotalPrice {  get; set; }

        public Reservation() { }

        public  Reservation(int reservationId, int customerId, int activeMovieId, List<SeatingArea> seatingAreas, double totalPrice)
        {
            ReservationId = reservationId;
            CustomerId = customerId;
            ActiveMovieId = activeMovieId;            
            SeatingAreas = seatingAreas;
            TotalPrice = totalPrice;
        }
    }

}
