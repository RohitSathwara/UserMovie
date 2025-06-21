using System.Data;
using System.Data.SqlClient;
using UserMovie.Models.Domain;

namespace UserMovie
{
    public class DBData
    {
        private readonly string _connStr;

        public DBData(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString("DefaultConnection");
        }

        public bool CheckEmailExists(string email)
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Email = @Email", conn);
            cmd.Parameters.AddWithValue("@Email", email);
            conn.Open();
            return (int)cmd.ExecuteScalar() > 0;
        }

        public void InsertUser(User user)
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand("sp_InsertUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FullName", user.FullName);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Mobile", user.Mobile);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public User GetUserByEmail(string email)
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand("sp_GetUserByEmail", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", email);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    Id = (int)reader["Id"],
                    FullName = reader["FullName"].ToString(),
                    Email = reader["Email"].ToString(),
                    Mobile = reader["Mobile"].ToString(),
                    Password = reader["Password"].ToString()
                };
            }
            return null;
        }


        public List<User> GetAllUsers()
        {
            var list = new List<User>();
            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand("sp_GetAllUsers", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new User
                {
                    Id = (int)reader["Id"],
                    FullName = reader["FullName"].ToString(),
                    Email = reader["Email"].ToString(),
                    Mobile = reader["Mobile"].ToString()
                });
            }
            return list;
        }

        public List<Movie> GetAllMovies()
        {
            var list = new List<Movie>();
            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand("sp_GetMovies", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Movie
                {
                    Id = (int)reader["Id"],
                    Title = reader["Title"].ToString(),
                    Genre = reader["Genre"].ToString(),
                    ReleaseDate = Convert.ToDateTime(reader["ReleaseDate"])
                });
            }
            return list;
        }

        public void InsertMovie(Movie movie)
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand("sp_AddMovie", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Title", movie.Title);
            cmd.Parameters.AddWithValue("@Genre", movie.Genre);
            cmd.Parameters.AddWithValue("@ReleaseDate", movie.ReleaseDate);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void UpdateMovie(Movie movie)
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand("sp_UpdateMovie", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", movie.Id);
            cmd.Parameters.AddWithValue("@Title", movie.Title);
            cmd.Parameters.AddWithValue("@Genre", movie.Genre);
            cmd.Parameters.AddWithValue("@ReleaseDate", movie.ReleaseDate);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public Movie GetMovieById(int id)
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand("sp_GetMovieById", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Movie
                {
                    Id = (int)reader["Id"],
                    Title = reader["Title"].ToString(),
                    Genre = reader["Genre"].ToString(),
                    ReleaseDate = Convert.ToDateTime(reader["ReleaseDate"])
                };
            }
            return null;
        }


        public void DeleteMovie(int id)
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand("DELETE FROM Movies WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
