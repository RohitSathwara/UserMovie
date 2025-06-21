//using UserMovie.Models.Domain;
//using UserMovie.Models.ViewModel;

//namespace UserMovie.Services
//{
//    public class AccountService : IAccountService
//    {
//        private readonly DBData _dbData;

//        public AccountService(DBData dbData)
//        {
//            _dbData = dbData;
//        }

//        public bool CheckEmailExists(string email)
//        {
//            return _dbData.CheckEmailExists(email);
//        }

//        public void RegisterUser(RegisterViewModel model)
//        {
//            var user = new User
//            {
//                FullName = model.FullName,
//                Email = model.Email,
//                Mobile = model.Mobile,
//                Password = model.Password
//            };

//            _dbData.InsertUser(user);
//        }

//        public User AuthenticateUser(string email, string password)
//        {
//            return _dbData.AuthenticateUser(email, password);
//        }
//    }
//}

using UserMovie.Helpers;
using UserMovie.Models.Domain;
using UserMovie.Models.ViewModel;

namespace UserMovie.Services
{
    public class AccountService : IAccountService
    {
        private readonly DBData _dbData;

        public AccountService(DBData dbData)
        {
            _dbData = dbData;
        }

        public bool CheckEmailExists(string email)
        {
            return _dbData.CheckEmailExists(email);
        }

        public void RegisterUser(RegisterViewModel model)
        {
            var encryptedPassword = EncryptionHelper.Encrypt(model.Password);

            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                Mobile = model.Mobile,
                Password = encryptedPassword
            };

            _dbData.InsertUser(user);
        }

        public User AuthenticateUser(string email, string password)
        {
            var user = _dbData.GetUserByEmail(email);
            if (user == null)
                return null;

            var decryptedPassword = EncryptionHelper.Decrypt(user.Password);
            return decryptedPassword == password ? user : null;
        }
    }
}
