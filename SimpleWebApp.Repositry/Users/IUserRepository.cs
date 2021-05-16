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
        CredentialsDto GetExist(long id);
        CredentialsDto GetExistByEmail(string email);
        CredentialsDto GetExistByLogin(string login);
        CredentialsDto GetExist(CredentialsDto credential);
        CredentialsDto GetUser(long id);
        CredentialsDto GetUserByLogin(string login);
        CredentialsDto GetUserByEmail(string email);
    }
}
