using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebApp.Repository
{
    public interface IUserRepository
    {
        void AddUser(CredentialsDto credential);
        void UpdateUser(CredentialsDto credential);
        void RemoveUserById(long id);
        void RemoveUserByLogin(string login);
        void RemoveUserByEmail(string email);
        bool GetExist(long id);
        bool GetExistByEmail(string email);
        bool GetExistByLogin(string login);
        bool GetExist(CredentialsDto credential);
        CredentialsDto GetUser(long id);
        CredentialsDto GetUserByLogin(string login);
        CredentialsDto GetUserByEmail(string email);
    }
}
