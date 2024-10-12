namespace BioskopMVC.Models
{
    public class Customer : User 
    {
        public Customer(int customerId, Membership membership, int userId, string firstName,string lastName,DateTime dateOfBirth, string nationality,string email, string password, int personId, DateTime registrationDate):base(personId,firstName,lastName,dateOfBirth,nationality,userId,email,password,registrationDate)
        {
            CustomerId = customerId;
            Membership = membership;
        }

        public int CustomerId { get; set; }

        public Membership Membership { get; set; }


    }
}
