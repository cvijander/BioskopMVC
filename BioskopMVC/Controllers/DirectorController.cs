using BioskopMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace BioskopMVC.Controllers
{
    public class DirectorController : Controller
    {

        private readonly string _connectionString;

        public DirectorController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BioskopDBConnection");
        }

        // GET  Director
        public IActionResult Index()
        {
            var directors = GetAllDirectors();
            return View(directors);
        }

        private List<Director> GetAllDirectors()
        {
            List<Director> directors = new List<Director>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT d.PersonId , p.FirstName, p.LastName, d.Filmography, d.Awards, d.Style, d.YearsDirecting, d.NotableFilms FROM Director d JOIN Person p ON p.PersonId = d.PersonId";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Director director = new Director
                        {
                            PersonId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Filmography = reader.GetString(3),
                            Awards = reader.GetString(4),
                            Style = reader.GetString(5),
                            YearsDirecting = reader.GetInt32(6),
                            NotableFilms = reader.GetString(7),
                           // Movies = GetMoviesForDirector(reader.GetInt32(0), connection)
                        };

                        directors.Add(director);
                    }
                }
            }
            return directors;
        }
        /*
        private List<Movie> GetMoviesForDirector(int directorId, SqlConnection connection)
        {
            List<Movie> movies= new List<Movie>();

            string movieSql = "SELECT m.MovieId, m.Title, m.Duration, m.ReleaseDate, m.Description, m."
        }
        */
    
    }
}
