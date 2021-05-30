using System;
using Xunit;
using SimpleWebApp.Repository;

namespace SimpleWebApp.Tests
{
    public class UsersRepositoryTests
    {
        [Fact]
        public void CreateUserTest()
        {
            UserDatabseRepository repository = new UserDatabseRepository();
            repository.AddUser(new CredentialsDto() { Login = "somelogin", Password = "password", Email = "somemail@mail.com", Roles = CredentialsDto.User });
        }

        [Fact]
        public void GetUserTest()
        {
            UserDatabseRepository repository = new UserDatabseRepository();
            var user = repository.GetUserByEmail("somemail@mail.com");
        }

        [Fact]
        public void UpdateUserTest()
        {
            UserDatabseRepository repository = new UserDatabseRepository();
            var user = repository.GetUserByEmail("somemail@mail.com");
            repository.UpdateUser(new CredentialsDto() { Login = "login", Id = user.Id, Email = "somemail@mail.com", Roles = CredentialsDto.Admin });
        }

        [Fact]
        public void DeleteUserTest()
        {
            UserDatabseRepository repository = new UserDatabseRepository();
            repository.RemoveUserByEmail("somemail@mail.com");
        }
    }
}
