using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleWebApp.Classes;
using SimpleWebApp.Repository;

namespace SimpleWebApp.Classes
{
    public static class Addition
    {
        public static CredentialsDto ToCredentialsDto(this Credential credential) => new CredentialsDto() { Id = credential.Id, Login = credential.Login, Password = credential.Password, Email = credential.Email, Roles = credential.Roles };
        public static Credential ToCredential(this CredentialsDto credential) => new Credential() { Id = credential.Id, Login = credential.Login, Password = credential.Password, Email = credential.Email, Roles = credential.Roles };
    }
}
