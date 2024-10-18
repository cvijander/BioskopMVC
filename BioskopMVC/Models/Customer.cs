using System.ComponentModel.DataAnnotations;

namespace BioskopMVC.Models
{
    public class Customer : User 
    {

        
        public int CustomerId { get; set; }

        [Required(ErrorMessage ="Membership je obavezan.")]
        public int MembershipId { get; set; }

        public Membership Membership { get; set; }

        public Customer(int customerId, int membershipId,  string firstName, string lastName, DateTime dateOfBirth,int nationalityId, string email, string password, int personId, DateTime registrationDate) : base(personId, firstName, lastName, dateOfBirth, nationalityId, email, password, registrationDate)
        {
            CustomerId = customerId;
            MembershipId = membershipId;
        }

        public Customer() { }


    }
}
