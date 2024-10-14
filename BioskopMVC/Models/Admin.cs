namespace BioskopMVC.Models
{
    public class Admin: User
    {
        public int AdminID {  get; set; }        
        public string AdminPrivileges { get; set; }

        public Admin(int adminId, string adminPrivileges,  int personId, string firstName, string lastName, DateTime dateOfBirth, string nationality, int userId, string email, string password, DateTime registrationDate) : base(personId, firstName, lastName, dateOfBirth, nationality, userId, email, password, registrationDate)
        {
            AdminID = adminId;
            AdminPrivileges = adminPrivileges;
        }

        public Admin() { }


    }
}
