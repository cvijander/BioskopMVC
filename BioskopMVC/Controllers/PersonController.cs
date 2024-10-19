using BioskopMVC.Models;
using Microsoft.AspNetCore.Mvc;
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

                        people.Add(person);
                    }
                }
            }
                return View(people);
        }



        // GET Person / Create
        public IActionResult Create()
        {
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
                    connection.Open();
                    string sql = "INSERT INTO Person (FirstName, LastName, DateOfBirth, NationalityId ) VALUES (@FirstName, @LastName, @DateOfBirth, @NationalityId )";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@FirstName", person.FirstName);
                    command.Parameters.AddWithValue("@LastName", person.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", person.DateOfBirth);
                    command.Parameters.AddWithValue("NationalityId", person.NationalityId);

                    command.ExecuteNonQuery();
                }

                return RedirectToAction(nameof(GetPeople));
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
                    string sql = "UPDATE Person GET FirstName = @FirstName, LastName = @LastName,DateOfBith = @DateOfBirth, NationalityId = @NationalityId WHERE PersonId = @Id";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@FirstName", person.FirstName);
                    command.Parameters.AddWithValue("LastName", person.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", person.DateOfBirth);
                    command.Parameters.AddWithValue("@NationlityId", person.NationalityId);
                    command.Parameters.AddWithValue("@Id", person.PersonId);

                    command.ExecuteNonQuery();
                }

                return RedirectToAction(nameof(GetPeople));
            }
            return View(person);
        }



        // GET  Person /Delete 
        public IActionResult Delete(int id)
        {
            Person person = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT PersonId, FirstName, LastName,DateOfBirth, NationlityId FROM Person WHERE PersonId = @Id";
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
                string sql = "DELETE Person WHERE PersonId = @Id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }

            return RedirectToAction(nameof(GetPeople));
        }

    }
}
