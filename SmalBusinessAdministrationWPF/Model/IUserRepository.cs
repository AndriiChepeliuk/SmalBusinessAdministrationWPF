using System.Collections.Generic;
using System.Net;

namespace SmallBusinessAdministrationWPF.Model
{
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        void Add(UserModel userModel);
        void Edit(UserModel userModel);
        void Remove(int id);
        UserModel GetById(int id);
        UserModel GetByUserName(string userName);
        IEnumerable<UserModel> GetAllUsers();
    }
}
