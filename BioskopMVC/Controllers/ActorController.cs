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
                string sql = "SELECT p.PersonId, p.FirstName, p.LastName, p.DateOfBirth, a.Biography, a.FamousRole, a.Awards, a.DateOfDeath, p.NationalityId, n.Name  AS NationalityName From Actor a JOIN Person p ON a.PersonId = p.PersonId JOIN Nationality n ON p.NationalityID = n.NationalityId ";

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

        // GET : Actor / EDIT/5

        public IActionResult Edit(int id)
        {
            Actor actor = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string sql = "SELECT p.PersonId, p.FirstName, p.LastName, p.DateOfBirth, a.Biography, a.FamousRole, a.Awards, a.DateOfDeath, p.NationalityId, n.Name AS NationalityName FROM Actor a JOIN Person p ON a.PersonId = p.PersonId JOIN Nationality n ON p.NationalityId = n.NationalityId WHERE a.PersonId = @PersonId";
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@PersonId", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                               actor = new Actor
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

                        }
                    }
                }
                if(actor == null )
                {
                    return NotFound();
                }
            }

            catch (Exception ex )
            {

                Console.WriteLine($"Greska prilikom dohvatanja glumca: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Doslo je do greske prilikm ucitavanja glumca");
            }

            List<Nationality> nationalities = GetAllNationalities();
            ViewBag.Nationalities = new SelectList(nationalities, "NationalityId", "Name", actor.NationalityId);

            return View(actor);

        }

        //POST Actor / Edit / 5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Actor actor)
        {
            try
            {
                ModelState.Remove("Nationality");
                if (!ModelState.IsValid)
                {
                    foreach (var error in ModelState)
                    {
                        Console.WriteLine($"Key: {error.Key}, Errors: {string.Join(",", error.Value.Errors.Select(e => e.ErrorMessage))}");
                    }
                    List<Nationality> nationalitiesTest = GetAllNationalities();
                    ViewBag.Nationalities = new SelectList(nationalitiesTest, "NationalityId", "Name", actor.NationalityId);
                    return View(actor);
                }
                
                if (id != actor.PersonId)
                {
                    return BadRequest("Invalid Person ID");
                }

                using (SqlConnection connection = new SqlConnection(_connectionString))
                 {
                        connection.Open();

                    PrintAllActors(connection);

                        Console.WriteLine($"PersonId: {actor.PersonId}");
                        Console.WriteLine($"FirstName: {actor.FirstName}");
                        Console.WriteLine($"LastName: {actor.LastName}");
                        Console.WriteLine($"DateOfBirth: {actor.DateOfBirth}");
                        Console.WriteLine($"Biography: {actor.Biography}");
                        Console.WriteLine($"FamousRole: {actor.FamousRole}");
                        Console.WriteLine($"Awards: {actor.Awards}");
                        Console.WriteLine($"DateOfDeath: {actor.DateOfDeath}");
                        Console.WriteLine($"NationalityId: {actor.NationalityId}");
                        Console.WriteLine($"PersonId being checked: {actor.PersonId}");


                    string personSql = "UPDATE Person Set FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, NationalityId = @NationalityId WHERE PersonId = @PersonId";
                        SqlCommand personCommand = new SqlCommand(personSql, connection);


                        personCommand.Parameters.AddWithValue("@FirstName", actor.FirstName);
                        personCommand.Parameters.AddWithValue("@LastName", actor.LastName);
                        personCommand.Parameters.AddWithValue("@DateOfBirth", actor.DateOfBirth);
                        personCommand.Parameters.AddWithValue("@NationalityId", actor.NationalityId);
                        personCommand.Parameters.AddWithValue("@PersonId", actor.PersonId);
                        
                        int rowsAffected = personCommand.ExecuteNonQuery();
                        Console.WriteLine($"Rows affected in Person table: {rowsAffected}");


                        string checkSql = "SELECT COUNT(*) FROM Actor WHERE PersonId = @PersonId";
                        SqlCommand checkCommand = new SqlCommand(checkSql, connection);
                        checkCommand.Parameters.AddWithValue("@PersonId", actor.PersonId);
                        int actorExists = (int)checkCommand.ExecuteScalar();
                        Console.WriteLine($"Actro exists for PersonId {actor.PersonId}: {actorExists}");

                        if(actorExists >0 )
                        {
                            string actorSql = "UPDATE Actor SET Biography = @Biography, FamousRole = @FamousRole, Awards = @Awards, DateOfDeath = @DateOfDeath WHERE PersonId = @PersonId";
                            SqlCommand actorCommand = new SqlCommand(actorSql, connection);


                            Console.WriteLine($"Actor exists for PersonId {actor.PersonId}: {actorExists}");
                            actorCommand.Parameters.AddWithValue("@Biography", actor.Biography);
                            actorCommand.Parameters.AddWithValue("@FamousRole", actor.FamousRole);
                            actorCommand.Parameters.AddWithValue("@Awards", actor.Awards);
                            actorCommand.Parameters.AddWithValue("@DateOfDeath", (object)actor.DateOfDeath ?? DBNull.Value);
                            actorCommand.Parameters.AddWithValue("@PersonId", actor.PersonId);
                            
                            int rowsAffectedActor = actorCommand.ExecuteNonQuery();
                            Console.WriteLine($"Rows affected in Actor table: {rowsAffectedActor}");
                       }
                        else
                        {
                        Console.WriteLine("Actor entry does not exist for this PersonId.");

                        }



                }

                    return RedirectToAction(nameof(Index));
                }

                

            
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                foreach (SqlError error in ex.Errors)
                {
                    Console.WriteLine($"Error Code: {error.Number}, Message: {error.Message}");
                }
                ModelState.AddModelError(string.Empty, "Došlo je do greške u komunikaciji sa bazom podataka.");
            }

            catch (Exception ex)
            {

                Console.WriteLine($"Greska prilikom izmene glumca {ex.Message}");
                ModelState.AddModelError(string.Empty, "Doslo je do greske prilikom azuriranja glumca");
                
            }

            List<Nationality> nationalities = GetAllNationalities();
            ViewBag.Nationalities = new SelectList(nationalities, "NationalityId","Name", actor.NationalityId);

            return View(actor);
        }

        private void PrintAllActors(SqlConnection connection)
        {
            string sql = "SELECT ActorId, PersonId, Biography, FamousRole From Actor";
            SqlCommand command = new SqlCommand(sql, connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                Console.WriteLine("Current Actors in Database");
                while(reader.Read())
                {
                    Console.WriteLine($"ActorId: {reader.GetInt32(0)}, PersonId: {reader.GetInt32(1)}, Biography: {reader.GetString(2)}, FamousRole: {reader.GetString(3)}");
                }
            }
        }

        // GET: Actor / Delete /1 
        public IActionResult Delete(int id)
        {
            Actor actor = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string sql = "SELECT p.PersonId, p.FirstName, p.LastName, p.DateOfBirth, a.Biography, a.Awards, a.FamousRole, a.DateOfDeath, p.NationalityId, n.Name AS NationalityName From Actor a JOIN Person p ON a.PersonId= p.PersonId JOIN Nationality n ON p.NationalityId = n.NationalityId WHERE a.PersonId = @PersonId";
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@PersonId", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            actor = new Actor
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
                        }
                    }
                }
            

                if (actor == null)
                {
                    return NotFound();
                }
                Console.WriteLine($"Glumac je pronadjen {actor.FirstName} {actor.LastName}");

            }

             catch (Exception ex)
            {

                Console.WriteLine($"Greska prilikom dohvatanja glumca: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Doslo je do greske prilikom ucitavanja glumca");
            }

            return View(actor);
        }

        // DELETE Actor / Delete /1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int PersonId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string deleteActorSql = "DELETE FROM Actor WHERE PersonId = @PersonId";
                    SqlCommand deleteActorCommand = new SqlCommand(deleteActorSql, connection);
                    deleteActorCommand.Parameters.AddWithValue("@PersonId", PersonId);

                    int rowsAffectredActor = deleteActorCommand.ExecuteNonQuery();
                    Console.WriteLine($"Rows affected in Actor table: {rowsAffectredActor}");

                    string deletePersonSql = "DELETE FROM Person WHERE PersonId = @PersonId";
                    SqlCommand deletePersonCommand = new SqlCommand(deletePersonSql, connection);
                    deletePersonCommand.Parameters.AddWithValue("@PersonId",PersonId);

                    int rowsAffectedPerson = deletePersonCommand.ExecuteNonQuery();
                    Console.WriteLine($"Rows affected in Person table: {rowsAffectedPerson}");
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greska prilikom brisanja glumca {ex.Message}");
                ModelState.AddModelError(string.Empty, "Doslo je do greske prilikom brisanja glumca");
                return View();
            }
        }
    }
}