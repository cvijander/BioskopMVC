using BioskopMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;

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

                        nationalities.Add(nationality);
                    }
                }
            }

            
            return nationalities;
        }

        public IActionResult Index()
        {
            try
            {
                var nationalities = GetNationalities();
                return View(nationalities);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Greska prilikom prikaza nacionalnosti : " +ex.Message);
                return View("Greska", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
           
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
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();
                        string sql = "INSERT INTO Nationality (Name) VALUES (@Name)";
                        SqlCommand command = new SqlCommand(sql, connection);
                        command.Parameters.AddWithValue("@Name", nationality.Name);

                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            Console.WriteLine("Nacionalnost je uspesno kreirana.");
                        }
                        else
                        {
                            Console.WriteLine("Greska, Nacionalnost nije kreirana");
                        }

                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("Greska prilikom kreiranja nacionalnosti: " + ex.Message);
                        ModelState.AddModelError(string.Empty, "Greska se desila prilikom snimanja nacionalnosti. ");
                        return View(nationality);
                    }

                }
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
                string sql = "SELECT NationalityId, Name FROM Nationality WHERE NationalityId = @Id";
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
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();
                        string sql = "UPDATE Nationality SET Name = @Name WHERE NationalityId = @Id";
                        SqlCommand command = new SqlCommand(sql, connection);
                        command.Parameters.AddWithValue("@Name", nationality.Name);
                        command.Parameters.AddWithValue("@Id", nationality.NationalityId);

                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            Console.WriteLine("Nacionalnost je uspesno izmenjena.");
                        }
                        else
                        {
                            Console.WriteLine("Greska, Nacionalnost nije izmenjena ");
                        }

                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("Greska prilikom izmene nacionalnosti: " + ex.Message);
                        ModelState.AddModelError(string.Empty, " Greska se desila prilikom izmenje nacionalnosti. ");
                        return View(nationality);
                    }

                }
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
                string sql = "SELECT NationalityId, Name FROM Nationality WHERE NationalityId = @Id";
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
                try
                {
                    connection.Open();
                    string sql = "DELETE FROM Nationality WHERE NationalityId = @Id";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine("Nacionalnost je uspesno obrisana");
                    }
                    else
                    {
                        Console.WriteLine("Greska, nacionalnost nije obrisana.");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Greska prilikom brisanja nacionalnosti: " + ex.Message);
                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }

            }
        }

        // Partial view 
        public IActionResult NationalityDropDown()
        {
            var nationalities = GetNationalities();
            return PartialView("_NationalityDropdownPartial", nationalities);
        }

    }
}
