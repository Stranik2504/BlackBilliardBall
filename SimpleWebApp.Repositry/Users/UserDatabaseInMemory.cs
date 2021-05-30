using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebApp.Repository
{
    public class UserDatabaseInMemory : IUserRepository
    {
        public List<CredentialsDto> _users = new();

        public void AddUser(CredentialsDto credential)
        {
            long maxId = 0;
            _users.ForEach(x => { if (x.Id > maxId) maxId = x.Id; });
            credential.Id = maxId + 1;
            _users.Add(credential);
        }

        public CredentialsDto GetUser(long id) => _users.Where(x => x.Id == id).FirstOrDefault();

        public bool GetExist(long id)
        {
            foreach (var user in _users)
            {
                if (user.Id == id) return true;
            }

            return false;
        }

        public bool GetExist(CredentialsDto credential)
        {
            foreach (var user in _users)
            {
                if (user.Login == credential.Login || user.Email == credential.Email) return true;
            }

            return false;
        }

        public void UpdateUser(CredentialsDto credential)
        {
            for (int i = 0; i < _users.Count; i++)
            {
                if (_users[i].Id == credential.Id)
                {
                    _users[i].Login = credential.Login;
                    _users[i].Password = credential.Password;
                    _users[i].Email = credential.Email;
                    _users[i].Roles = credential.Roles;
                }
            }
        }

        public void RemoveUserById(long id)
        {
            for (int i = 0; i < _users.Count; i++)
            {
                if (_users[i].Id == id)
                {
                    _users.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveUserByLogin(string login)
        {
            for (int i = 0; i < _users.Count; i++)
            {
                if (_users[i].Login == login)
                {
                    _users.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveUserByEmail(string email)
        {
            for (int i = 0; i < _users.Count; i++)
            {
                if (_users[i].Email == email)
                {
                    _users.RemoveAt(i);
                    break;
                }
            }
        }

        public bool GetExistByEmail(string email)
        {
            foreach (var user in _users)
            {
                if (user.Email == email) return true;
            }

            return false;
        }

        public bool GetExistByLogin(string login)
        {
            foreach (var user in _users)
            {
                if (user.Login == login) return true;
            }

            return false;
        }

        public CredentialsDto GetUserByLogin(string login) => _users.Where(x => x.Login == login).FirstOrDefault();

        public CredentialsDto GetUserByEmail(string email) => _users.Where(x => x.Email == email).FirstOrDefault();
    }
}
