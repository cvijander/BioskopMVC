using BioskopMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace BioskopMVC.Controllers
{
    public class PersonController : Controller
    {
        private readonly string _connectionString;

        public PersonController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BioskopDBConnection");
        }
             
        public IActionResult Index()
        {
            var persons = GetAllPersons();
            return View(persons);
        }

        private List<Person> GetAllPersons()
        {
            List<Person> persons = new List<Person>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT p.PersonId, p.FirstName, p.LastName,p.DateOfBirth, p.NationalityId, n.Name AS NationalityName FROM Person p JOIN Nationality n ON p.NationalityId = n.NationalityId"; ;
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Person person = new Person
                        {
                            PersonId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            DateOfBirth = reader.GetDateTime(3),
                            NationalityId = reader.GetInt32(4),
                            Nationality = new Nationality
                            {
                                Name = reader.GetString(5)
                            }
                        };

                        persons.Add(person);
                    }
                }
            }
            return persons;
        }

        //GET person 
        public IActionResult GetPeople()
        {
             // lista osoba 
            List<Person> people = new List<Person>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT PersonId, FirstName, LastName, DateOfBirth, NationalityId FROM Person";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Person person = new Person
                        {
                            PersonId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            DateOfBirth = reader.GetDateTime(3),
                            NationalityId = reader.GetInt32(4)
                        };

                        people.Add(person);
                    }
                }
            }
                return View(people);
        }


        // Nacionality 
        private List<Nationality> GetAllNationalities()
        {
            List<Nationality> nationalities = new List<Nationality>();

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
                            Name = reader.GetString(1),
                        };

                        nationalities.Add(nationality);
                    }
                }

            }

            return nationalities;
        }
        


        // GET Person / Create
        public IActionResult Create()
        {
            List<Nationality> nationalities = GetAllNationalities();

            ViewBag.Nationalities = new SelectList(nationalities,"NationalityId","Name");

            return View();
        }

        // POST: Person / Create  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Person person)
        {

            ModelState.Remove("Nationality");
            Console.WriteLine($"Izabrana Nacionalnost: {person.NationalityId}");

            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();

                        Console.WriteLine("Uspesno povezan sa bazom");
                        Console.WriteLine($"FirstName: {person.FirstName}");
                        Console.WriteLine($"LastName: {person.LastName}");
                        Console.WriteLine($"DateOfBirth,{person.DateOfBirth}");
                        Console.WriteLine($"NationalityId,{person.NationalityId}");

                        string sql = "INSERT INTO Person (FirstName, LastName, DateOfBirth, NationalityId) VALUES (@FirstName, @LastName, @DateOfBirth, @NationalityId)";
                        SqlCommand command = new SqlCommand(sql, connection);
                        command.Parameters.AddWithValue("@FirstName", person.FirstName);
                        command.Parameters.AddWithValue("@LastName", person.LastName);
                        command.Parameters.AddWithValue("@DateOfBirth", person.DateOfBirth);
                        command.Parameters.AddWithValue("@NationalityId", person.NationalityId);

                        int result = command.ExecuteNonQuery();

                        if(result > 0)
                        {
                            Console.WriteLine("Osoba je uspesno kreirana");
                        }
                        else
                        {
                            Console.WriteLine("Kreiranje osobe nije uspelo");
                        }
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("Greska prilikom snimanja osobe: " + ex.Message);
                        ModelState.AddModelError(string.Empty, "Doslo je do greske prilikom snimanja podataka");
                        return View(person);
                    }
                    
                }

                return RedirectToAction(nameof(Index));
            }

            List<Nationality> nationalities = GetAllNationalities();
            ViewBag.Nationalities = new SelectList(nationalities, "NationalityId", "Name");

              
             foreach (var modelState in ModelState)
             {
                Console.WriteLine($"Key: {modelState.Key}");

                foreach(var error in modelState.Value.Errors)
                {
                    Console.WriteLine("Greska " + error.ErrorMessage);
                } 

            }
            

            return View(person);
        }


        // GET:  Person/Edit 
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                Person person = null;

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string sql = "SELECT p.PersonId, p.FirstName, p.LastName, p.DateOfBirth, p.NationalityId, n.Name AS NationalityName FROM Person p JOIN Nationality n ON p.NationalityId = n.NationalityId  WHERE p.PersonId = @Id ";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            person = new Person
                            {
                                PersonId = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                DateOfBirth = reader.GetDateTime(3),
                                NationalityId = reader.GetInt32(4),
                                Nationality = new Nationality
                                {
                                    Name =reader.GetString(5)
                                }
                            };

                        }
                    }
                }

                if (person == null)
                {
                    return NotFound();
                }

                List<Nationality> nationalities = GetAllNationalities();
                ViewBag.Nationalities = new SelectList(nationalities, "NationalityId", "Name");

                return View(person);
            }

            catch (Exception ex)
            {

                Console.WriteLine($"Greska: {ex.Message}");
                return View("Error");
            }

        }


        // POST:  Person/Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Person person)
        {
            ModelState.Remove("Nationality");
            try
            {
                if (ModelState.IsValid)
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {

                        Console.WriteLine($"PersonId: {person.PersonId}");
                        Console.WriteLine($"FirstName: {person.FirstName}");
                        Console.WriteLine($"LastName: {person.LastName}");
                        Console.WriteLine($"DateOfBirth: {person.DateOfBirth}");
                        Console.WriteLine($"NationalityId: {person.NationalityId}");

                        connection.Open();
                        string sql = "UPDATE Person SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, NationalityId = @NationalityId WHERE PersonId = @Id";
                        SqlCommand command = new SqlCommand(sql, connection);
                        command.Parameters.AddWithValue("@FirstName", person.FirstName);
                        command.Parameters.AddWithValue("@LastName", person.LastName);
                        command.Parameters.AddWithValue("@DateOfBirth", person.DateOfBirth);
                        command.Parameters.AddWithValue("@NationalityId", person.NationalityId);
                        command.Parameters.AddWithValue("@Id", person.PersonId);

                        int result = command.ExecuteNonQuery();
                        if(result > 0)
                        {
                            Console.WriteLine("Podaci su uspesno azurirani");
                        }
                        else
                        {
                            Console.WriteLine("Azuriranje nije uspelo");
                        }
                    }

                    return RedirectToAction(nameof(Index));
                }

                List<Nationality> nationalities = GetAllNationalities();
                ViewBag.Nationalities = new SelectList(nationalities, "NationalityId", "Name");

                return View(person);

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Greska prilikom azuriranja osobe: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Doslo je do greske prilikom azuriranja osobe");
                return View(person);
            }
        }

        // GET  Person /Delete 
        
        public IActionResult Delete(int id)
        {
            Person person = null;
            

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string sql = "SELECT p.PersonId, p.FirstName, p.LastName, p.DateOfBirth, p.NationalityId, n.Name AS NationalityName FROM Person p JOIN Nationality n ON p.NationalityId = n.NationalityId  WHERE p.PersonId = @Id";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            person = new Person
                            {
                                PersonId = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                DateOfBirth = reader.GetDateTime(3),
                                NationalityId = reader.GetInt32(4),
                                Nationality = new Nationality
                                {
                                    Name = reader.GetString(5)
                                }

                            };
                        }
                    }
                }


                if (person == null)
                {
                    return NotFound();
                }

                return View(person);

            }
            catch (Exception ex )
            {

                Console.WriteLine($"Greska prilikom ucitavanja osobe: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }          
                        
        }

        // POST Person / Delete 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    Console.WriteLine($"Pokusaj brisanja osobe sa id: {id}");

                    string sql = "DELETE FROM Person WHERE PersonId = @Id";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine("Osoba je uspesno obrisana");
                    }
                    else
                    {
                        Console.WriteLine("Brisanje osobe nije uspelo");
                        
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Greska prilikom brisanja osobe: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Doslo je do greske prilikom brisanja osobe");

                var person = GetPersonById(id);
                if(person ==null)
                {
                    return NotFound();
                }

                return View("Delete", person);
                
            }

        }

        private Person GetPersonById(int id)
        {
            Person person = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT p.PersonId, p.FirstName, p.LastName, p.DateOfBirth, p.NationalityId, n.Name AS NationalityName " +
                             "FROM Person p " +
                             "JOIN Nationality n ON p.NationalityId = n.NationalityId " +
                             "WHERE p.PersonId = @Id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        person = new Person
                        {
                            PersonId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            DateOfBirth = reader.GetDateTime(3),
                            NationalityId = reader.GetInt32(4),
                            Nationality = new Nationality
                            {
                                Name = reader.GetString(5)
                            }
                        };
                    }
                }
            }

            return person;
        }
        

    }
}
