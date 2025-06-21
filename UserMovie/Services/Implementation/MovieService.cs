using UserMovie.Models.Domain;
using UserMovie.Models.ViewModel;

namespace UserMovie.Services
{
    public class MovieService : IMovieService
    {
        private readonly DBData _dbData;

        public MovieService(DBData dbData)
        {
            _dbData = dbData;
        }

        public List<MovieViewModel> GetAll()
        {
            return _dbData.GetAllMovies()
                      .Select(m => new MovieViewModel
                      {
                          Id = m.Id,
                          Title = m.Title,
                          Genre = m.Genre,
                          ReleaseDate = m.ReleaseDate
                      }).ToList();
        }

        public MovieViewModel GetById(int id)
        {
            var movie = _dbData.GetMovieById(id);
            if (movie == null) return null;

            return new MovieViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Genre = movie.Genre,
                ReleaseDate = movie.ReleaseDate
            };
        }

        public void Add(MovieViewModel model)
        {
            var movie = new Movie
            {
                Title = model.Title,
                Genre = model.Genre,
                ReleaseDate = model.ReleaseDate
            };
            _dbData.InsertMovie(movie);
        }

        public void Update(MovieViewModel model)
        {
            var movie = new Movie
            {
                Id = model.Id,
                Title = model.Title,
                Genre = model.Genre,
                ReleaseDate = model.ReleaseDate
            };
            _dbData.UpdateMovie(movie);
        }

        public void Delete(int id)
        {
            _dbData.DeleteMovie(id);
        }

        public void DeleteMultiple(List<int> ids)
        {
            foreach (var id in ids)
                _dbData.DeleteMovie(id);
        }
    }
}
