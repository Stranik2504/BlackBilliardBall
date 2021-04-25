using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebApp.Repository
{
    public class CredentialsDto
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public CredentialsDto() => (Id, Login, Password) = (-1, "", "");
    }
}
