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
        CredentialsDto GetExist(long id);
        CredentialsDto GetExist(CredentialsDto credential);
        CredentialsDto GetUser(long id);
    }
}
