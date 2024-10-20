using BioskopMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace BioskopMVC.Controllers
{
    public class NationalityController : Controller
    {

        private readonly string _connectionString;

        public NationalityController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BioskopDBConnection");
        }

        public List<Nationality> GetNationalities()
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
                            Name = reader.GetString(1)
                        };

                        nationalities.Add(nationality);
                    }
                }
            }

            
            return nationalities;
        }

        public IActionResult Index()
        {
            var nationalities = GetNationalities();
            return View(nationalities);
        }

        // GET     Nacionality / Create
        public IActionResult Create()
        {
            return View();
        }

        // POST    Nationality / Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Nationality nationality)
        {
            if(ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Nationalities (Name) VALUES (@Name)";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@Name", nationality.Name);

                    command.ExecuteNonQuery();
                }

                return RedirectToAction(nameof(GetNationalities));
            }

            return View(nationality);
        }

        // GET  Natinality Edit
        public IActionResult Edit(int id)
        {
            Nationality nationality = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT NationalityId, Name FROM Nationalities WHERE NationalityId = @Id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        nationality = new Nationality
                        {
                            NationalityId = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                    }
                }
                  
            }
            if(nationality == null)
            {
                return NotFound();
            }

            return View(nationality);
        }

        // POST   Nacionality / Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Nationality nationality)
        {
            if( ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Nationalities SET Name = @Name Where NationalityId = @Id";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@Name", nationality.Name);
                    command.Parameters.AddWithValue("@Id", nationality.NationalityId);

                    command.ExecuteNonQuery();
                }

                return RedirectToAction(nameof(GetNationalities));
            }

            return View(nationality);
        }

        // GET Nationality / Delete 

        public IActionResult Delete(int id)
        {
            Nationality nationality = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT NationalityId, Name FROM Nationalities WHERE NationalityId = @Id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        nationality = new Nationality
                        {
                            NationalityId = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                    }
                }
            }

            if(nationality ==null)
            {
                return NotFound();
            }

            return View(nationality);
        }

        // POST Nationality / Delete

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Nationalities WHERE NationalityId = @Id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }

            
            return RedirectToAction(nameof(GetNationalities));
        }


    }
}
