using BioskopMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace BioskopMVC.Controllers
{
    public class MovieController : Controller
    {
        private readonly string _connectionString;

        public MovieController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BioskopDBConnection");
        }

        //GET  Movie
        public IActionResult Index()
        {
            var movies = GetAllMovies();
            return View(movies);
        }

        private List<Movie> GetAllMovies()
        {
            List<Movie> movies = new List<Movie>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT m.MovieId, m.Title, m.Duration, m.ReleaseDate, m.Description, m.ProductionCompany, m.Rating, m.PosterUrl, m.isActive, d.PersonId AS DirectorId, p.FirstName AS DirectorFirstName, p.LastName AS DirectorLastName  FROM Movie m JOIN Director d ON m.DirectorId = d.PersonId JOIN Person p ON d.PersonId = p.PersonId";

                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Movie movie = new Movie
                        {
                            MovieId = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Duration = reader.GetTimeSpan(2),
                            ReleaseDate = reader.GetDateTime(3),
                            Description = reader.GetString(4),
                            ProductionCompany = reader.GetString(5),
                            Rating = reader.GetDouble(6),
                            PosterUrl = reader.IsDBNull(7) ? null : reader.GetString(7),
                            isActive = reader.GetBoolean(8),
                            Director = new Director
                            {
                                PersonId = reader.GetInt32(9),
                                FirstName = reader.GetString(10),
                                LastName = reader.GetString(11),
                            },
                            Actors = GetActorsForMovie(reader.GetInt32(0), connection)
                        };

                        movies.Add(movie);
                    }
                }
            }

            return movies;
        }

        private List<Actor> GetActorsForMovie(int movieId, SqlConnection connection)
        {
            List<Actor> actors = new List<Actor>();

            string actorSql = "SELECT a.PersonId , p.FirstName, p.LastName, FROM Actor a JOIN MovieActor ma ON a.PersonId = ma.ActorId JOIN Person p ON a.PersonId = p.PersonId WHERE ma.MovieId = @MovieId";

            SqlCommand actorCommand = new SqlCommand(actorSql, connection);
            actorCommand.Parameters.AddWithValue("@MovieId", movieId);

            using (SqlDataReader reader = actorCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Actor actor = new Actor
                    {
                        PersonId = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                    };

                    actors.Add(actor);
                }

            }

            return actors;
        }

    }
}