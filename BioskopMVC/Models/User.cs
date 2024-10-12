namespace BioskopMVC.Models
{
    public class User : Person
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }

        public User(int personId, string firstName, string lastName, DateTime dateOfBirth, string nationality, int userId, string email, string password, DateTime registrationDate) : base(personId, firstName, lastName, dateOfBirth, nationality)
        {
            UserId = userId;
            Email = email;
            Password = password;
            RegistrationDate = registrationDate;
        }

        
    }
}
