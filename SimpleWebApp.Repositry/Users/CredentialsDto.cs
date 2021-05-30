using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebApp.Repository
{
    public class CredentialsDto
    {
        public const string User = "User";
        public const string Admin = "Admin";

        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }

        public CredentialsDto() => (Id, Login, Password, Email, Roles) = (-1, "", "", "", "");
    }
}
