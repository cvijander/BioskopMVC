namespace BioskopMVC.Models
{
    public class Customer : User 
    {
        

        public int CustomerId { get; set; }

        public int MembershipId { get; set; }

        public Membership Membership { get; set; }

        public Customer(int customerId, int membershipId, int userId, string firstName, string lastName, DateTime dateOfBirth, string nationality, string email, string password, int personId, DateTime registrationDate) : base(personId, firstName, lastName, dateOfBirth, nationality, userId, email, password, registrationDate)
        {
            CustomerId = customerId;
            MembershipId = membershipId;
        }

        public Customer() { }


    }
}
