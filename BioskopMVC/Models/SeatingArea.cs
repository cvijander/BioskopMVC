namespace BioskopMVC.Models
{
    public class SeatingArea
    {
        public int SeatingAreaId { get; set; }
        public string Sector { get; set; }
        public string Row { get; set; }
        public int SeatNumber { get; set; }

        public SeatingArea() { }

        public SeatingArea(int seatingAreaId, string sector, string row, int seatNumber)
        {
            SeatingAreaId = seatingAreaId;
            Sector = sector;
            Row = row;
            SeatNumber = seatNumber;
        }
    }
}
