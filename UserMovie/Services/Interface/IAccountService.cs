using UserMovie.Models.Domain;
using UserMovie.Models.ViewModel;

namespace UserMovie.Services
{
    public interface IAccountService
    {
        bool CheckEmailExists(string email);
        void RegisterUser(RegisterViewModel model);
        User AuthenticateUser(string email, string password);
    }
}
