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
                string sql = "SELECT PersonId, FirstName, LastName,DateOfBirth, NationalityId FROM Person";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Person person = new Person
                        {
                            PersonId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            DateOfBirth = reader.GetDateTime(3),
                            NationalityId = reader.GetInt32(4)
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
                string sql = "SELECT NationalityId, Name FROM Nationalities";
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
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();

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
                            Console.WriteLine("Upit je uspesno izvrsen, broj izmenjenih redova: " + result);
                        }
                        else
                        {
                            Console.WriteLine("Upit nije uspeo, broj izmenjenih redova: " + result);
                        }
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("Greska pri unosu u bazu: " + ex.Message);
                        ModelState.AddModelError(string.Empty, "Doslo je do greske prilikom snimanja podataka");
                        return View(person);
                    }
                    
                }

                return RedirectToAction(nameof(GetPeople));
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
        public IActionResult Edit(int id)
        {
            Person person = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT PersonId, FirstName, LastName, DateOfBirth, NationalityId FROM Person WHERE PersonId = @Id ";
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
                            NationalityId = reader.GetInt32(4)
                        };

                    }
                }
            }

            if(person ==null)
            {
                return NotFound();
            }

            List<Nationality> nationalities = GetAllNationalities();
            ViewBag.Nationalities = new SelectList(nationalities, "NationalityId", "Name");

            return View(person);
        }

        // POST:  Person/Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Person SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, NationalityId = @NationalityId WHERE PersonId = @Id";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@FirstName", person.FirstName);
                    command.Parameters.AddWithValue("@LastName", person.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", person.DateOfBirth);
                    command.Parameters.AddWithValue("@NationalityId", person.NationalityId);
                    command.Parameters.AddWithValue("@Id", person.PersonId);

                    command.ExecuteNonQuery();
                }

                return RedirectToAction(nameof(GetPeople));
            }

            List<Nationality>nationalities = GetAllNationalities();
            ViewBag.Nationalities = new SelectList(nationalities, "NationalityId", "Name");

            return View(person);
        }



        // GET  Person /Delete 
        public IActionResult Delete(int id)
        {
            Person person = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT PersonId, FirstName, LastName,DateOfBirth, NationalityId FROM Person WHERE PersonId = @Id";
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
                            NationalityId = reader.GetInt32(4)

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

        

        // POST Person / Delete 
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Person WHERE PersonId = @Id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }

            return RedirectToAction(nameof(GetPeople));
        }

    }
}
