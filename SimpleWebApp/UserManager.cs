using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleWebApp.Classes;
using SimpleWebApp.Repository;

namespace SimpleWebApp
{
    public class UserManager
    {
        private readonly IUserRepository _repository;

        public UserManager(IUserRepository repository) => _repository = repository;

        public void AddUser(Credential credential) => _repository.AddUser(credential.ToCredentialsDto());

        public bool GetExist(Credential credential) => _repository.GetExist(credential.ToCredentialsDto());

        public bool IsCorrectParams(Credential credential)
        {
            var user = _repository.GetUserByLogin(credential.Login);

            if (user == null || user.Login == null) return false;

            if (user.Password == credential.Password) return true;

            return false;
        }

        public Credential GetUser(string login) => _repository.GetUserByLogin(login).ToCredential();
    }
}
