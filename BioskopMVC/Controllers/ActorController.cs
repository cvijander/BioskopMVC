using BioskopMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using System.Linq.Expressions;

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
                string sql = "SELECT a.ActorId, p.FirstName, p.LastName, p.DateOfBirth, a.Biography, a.FamousRole, a.Awards, a.DateOfDeath, p.NationalityId, n.Name  AS NationalityName From Actor a JOIN Person p ON a.PersonId = p.PersonId JOIN Nationality n ON p.NationalityID = n.NationalityId ";

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
                            DateOfBirth = reader.GetDateTime(3),
                            Biography = reader.GetString(4),
                            FamousRole = reader.GetString(5),
                            Awards = reader.GetString(6),
                            DateOfDeath = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7),
                            NationalityId = reader.GetInt32(8),
                            Nationality = new Nationality
                            {
                                Name = reader.GetString(9)
                            }
                        };

                        actors.Add(actor);
                    }
                }
            }
            return actors;
        }

        // GET Actor / Create
        public IActionResult Create()
        {
            List<Nationality> nationalities = GetAllNationalities();
            ViewBag.Nationalities = new SelectList(nationalities, "NationalityId", "Name");
            return View();
        }

        // Za nacionalnosti 
        private List<Nationality> GetAllNationalities()
        {
            List<Nationality> nacionalities = new List<Nationality>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT NationalityId, Name FROM Nationality";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Nationality nationality = new Nationality
                        {
                            NationalityId = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };

                        nacionalities.Add(nationality);
                    }
                }
            }

            return nacionalities;
        }

        // POST Actor / Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Actor actor)
        {
            

            try
            {
                ModelState.Remove("PersonId");
                ModelState.Remove("Nationality");
                if (ModelState.IsValid)
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        Console.WriteLine($"FirstName: {actor.FirstName}");
                        Console.WriteLine($"LastName: {actor.LastName}");
                        Console.WriteLine($"DateOfBirth: {actor.DateOfBirth}");
                        Console.WriteLine($"NationalityId: {actor.NationalityId}");
                        Console.WriteLine($"Biography: {actor.Biography}");
                        Console.WriteLine($"FamousRole: {actor.FamousRole}");
                        Console.WriteLine($"Awards: {actor.Awards}");
                        Console.WriteLine($"DateOfDeath: {actor.DateOfDeath}");

                        connection.Open();
                        string personSql = "INSERT INTO Person (FirstName, LastName, DateOfBirth, NationalityId) VALUES (@FirstName, @LastName, @DateOfBirth, @NationalityId); SELECT SCOPE_IDENTITY();";

                        SqlCommand personCommand = new SqlCommand(personSql, connection);

                        personCommand.Parameters.AddWithValue("@FirstName", actor.FirstName);
                        personCommand.Parameters.AddWithValue("@LastName", actor.LastName);
                        personCommand.Parameters.AddWithValue("@DateOfBirth", actor.DateOfBirth);
                        personCommand.Parameters.AddWithValue("@NationalityId", actor.NationalityId);

                        int personId = Convert.ToInt32(personCommand.ExecuteScalar());

                        string actorSql = "INSERT INTO Actor (PersonId, Biography, FamousRole,Awards, DateOfDeath) VALUES (@PersonId, @Biography, @FamousRole, @Awards, @DateOfDeath)";
                        SqlCommand actorCommand = new SqlCommand(actorSql, connection);

                        actorCommand.Parameters.AddWithValue("@PersonId", personId);
                        actorCommand.Parameters.AddWithValue("@Biography", actor.Biography ?? string.Empty);
                        actorCommand.Parameters.AddWithValue("@FamousRole", actor.FamousRole ?? string.Empty);
                        actorCommand.Parameters.AddWithValue("@Awards", actor.Awards ?? string.Empty);
                        actorCommand.Parameters.AddWithValue("@DateOfDeath", (object)actor.DateOfDeath ?? DBNull.Value);

                        actorCommand.ExecuteNonQuery();

                        return RedirectToAction(nameof(Index));
                    }

                }
                else
                {
                    foreach (var error in ModelState)
                    {
                        Console.WriteLine($"Key: {error.Key}, Errors: {string.Join(",",error.Value.Errors.Select(e => e.ErrorMessage))}");
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Greska prilikom kreiranja glumca: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Doslo je do greske prilikom kreiranja glumca");
            }
     

            List<Nationality> nationalities = GetAllNationalities();
            ViewBag.Nationalities = new SelectList(nationalities, "NationalityId", "Name");
            return View(actor);
        }
    }
}