namespace BioskopMVC.Models
{
    public class Actor: Person
    {
       
        public int ActorId { get; set; }

        public string Description { get; set; }

        public List<Movie> Movies { get; set; } = new List<Movie>();

        public Actor(int personId, string firstName, string lastName, DateTime dateOfBirth, string nationality, string description, int actorId) : base(personId, firstName, lastName, dateOfBirth, nationality)
        {

            ActorId = actorId;
            Description = description;

        }

    }
}
