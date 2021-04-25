using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using Dapper;

namespace SimpleWebApp.Repository
{
    public class UserDatabseRepository : IUserRepository
    {
        public void AddUser(CredentialsDto credential)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            string sqlQuery = "INSERT INTO users (Login, Password) VALUES (@Login, @Password)";

            int rowsAffected = db.Execute(sqlQuery, credential);
        }

        public CredentialsDto GetUser(long id)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            string sqlQuery = $"SELECT * FROM users WHERE Id = @Id";

            return db.QuerySingle<CredentialsDto>(sqlQuery, new CredentialsDto() { Id = id });
        }

        public CredentialsDto GetExist(long id)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            string sqlQuery = $"SELECT * FROM users WHERE Id = @Id";

            var result = db.QuerySingle<CredentialsDto>(sqlQuery, new CredentialsDto() { Id = id });
            return result != null && result?.Login != null && result?.Password != null ? result : new CredentialsDto();
        }

        public CredentialsDto GetExist(CredentialsDto credential)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            string sqlQuery = $"SELECT * FROM users WHERE Login = @Login AND Password = @Password";

            var result = db.QuerySingle<CredentialsDto>(sqlQuery, credential);
            return result != null && result?.Login != null && result?.Password != null ? result : new CredentialsDto();
        }

        public void UpdateUser(CredentialsDto credential)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            string sqlQuery = $"UPDATE users SET Login = @Login, Password = @Password WHERE Id = @Id";

            int rowsAffected = db.Execute(sqlQuery, credential);
        }

        public void RemoveUserById(long id)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            string sqlQuery = $"DELETE FROM users WHERE Id = @id";

            int rowsAffected = db.Execute(sqlQuery, new { id });
        }

        public void RemoveUserByLogin(string login)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            string sqlQuery = $"DELETE FROM users WHERE Login = @login";

            int rowsAffected = db.Execute(sqlQuery, new { login });
        } 
    }
}
