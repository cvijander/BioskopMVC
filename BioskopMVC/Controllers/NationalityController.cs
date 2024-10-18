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

        public IActionResult GetNationalities()
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

            // 
            return PartialView("_NationalityDropdownPartial", nationalities);
        }
    }
}
