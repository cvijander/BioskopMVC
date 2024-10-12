namespace BioskopMVC.Models
{
    public class Membership
    {
       

        public int MembershipID { get; set; }
        public string MembershipLevel { get; set; }
        public DateTime StartDate { get; set; }
        public double Discount { get; set; }

        public Membership(int membershipID, string membershipLevel, DateTime startDate, double discount)
        {
            MembershipID = membershipID;
            MembershipLevel = membershipLevel;
            StartDate = startDate;
            Discount = discount;
        }



    }
}
