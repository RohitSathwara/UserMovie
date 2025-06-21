using UserMovie.Models.ViewModel;

namespace UserMovie.Services
{
    public interface IMovieService
    {
        List<MovieViewModel> GetAll();
        MovieViewModel GetById(int id);
        void Add(MovieViewModel model);
        void Update(MovieViewModel model);
        void Delete(int id);
        void DeleteMultiple(List<int> ids);
    }
}
