namespace BioskopMVC.Models
{
    public class Director : Person
    {
        public int DirectorID { get; set; }
        public string Description { get; set; }
        public List<Movie> Movies { get; set; }=new List<Movie>();

        public Director(int personId, string firstName, string lastName, DateTime dateOfBirth, string nationality, string description, int directorID) : base(personId, firstName, lastName, dateOfBirth, nationality)
        {
            DirectorID = directorID;
            Description = description;
            
        }
    }
}
