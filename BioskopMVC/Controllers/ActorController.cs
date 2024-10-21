using BioskopMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace BioskopMVC.Controllers
{
    public class ActorController : Controller
    {
        private readonly string _connectionString;

        public ActorController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BioskopDBConnection");

        }


        public IActionResult Index()
        {
            var actors = GetAllActors();
            return View(actors);
        }

        private List<Actor> GetAllActors()
        {
            List<Actor> actors = new List<Actor>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT a.ActorId, p.FirstName, p.LastName, a.Biography, a.FamousRole, a.Awards,a.DateOfDeath, p.NationalityId, n.Name  AS NationalityName From Actor a JOIN Person p ON a.PersonId = p.PersonId JOIN Nationality n ON p.NationalityID = n.NationalityId ";

                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Actor actor = new Actor
                        {
                            PersonId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Biography = reader.GetString(3),
                            FamousRole = reader.GetString(4),
                            Awards = reader.GetString(5),
                            DateOfDeath = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6),
                            NationalityId = reader.GetInt32(7),
                            Nationality = new Nationality
                            {
                                Name = reader.GetString(8)
                            }
                        };

                        actors.Add(actor);
                    }
                }
            }
            return actors;
        }
    }
}