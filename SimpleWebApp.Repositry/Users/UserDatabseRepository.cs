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
            string sqlQuery = "INSERT INTO users (Login, Password, Email, Roles) VALUES (@Login, @Password, @Email, @Roles)";

            int rowsAffected = db.Execute(sqlQuery, credential);
        }

        public CredentialsDto GetUser(long id)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            string sqlQuery = "SELECT * FROM users WHERE Id = @Id";

            return db.QuerySingle<CredentialsDto>(sqlQuery, new CredentialsDto() { Id = id });
        }

        public bool GetExist(long id)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            string sqlQuery = "SELECT * FROM users WHERE Id = @Id";

            var result = db.QuerySingle<CredentialsDto>(sqlQuery, new CredentialsDto() { Id = id });
            return result != null && result?.Login != null && result?.Password != null ? true : false;
        }

        public bool GetExist(CredentialsDto credential)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            string sqlQuery = "SELECT * FROM users WHERE Login = @Login OR Email = @Email";

            var result = db.QuerySingleOrDefault<CredentialsDto>(sqlQuery, credential);
            return result != null && result?.Login != null && result?.Password != null ? true : false;
        }

        public void UpdateUser(CredentialsDto credential)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            string sqlQuery = "UPDATE users SET Login = @Login, Password = @Password, Email = @Email, Roles = @Roles WHERE Id = @Id";

            int rowsAffected = db.Execute(sqlQuery, credential);
        }

        public void RemoveUserById(long id)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            string sqlQuery = "DELETE FROM users WHERE Id = @id";

            int rowsAffected = db.Execute(sqlQuery, new { id });
        }

        public void RemoveUserByLogin(string login)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            string sqlQuery = "DELETE FROM users WHERE Login = @login";

            int rowsAffected = db.Execute(sqlQuery, new { login });
        }

        public void RemoveUserByEmail(string email)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            string sqlQuery = "DELETE FROM users WHERE Email = @email";

            int rowsAffected = db.Execute(sqlQuery, new { email });
        }

        public bool GetExistByEmail(string email)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            string sqlQuery = "SELECT * FROM users WHERE Email = @email";

            var result = db.QuerySingleOrDefault<CredentialsDto>(sqlQuery, new { email });
            return result != null && result?.Login != null ? true : false;
        }

        public bool GetExistByLogin(string login)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            string sqlQuery = "SELECT * FROM users WHERE Login = @login";

            var result = db.QuerySingleOrDefault<CredentialsDto>(sqlQuery, new { login });
            return result != null && result?.Login != null ? true : false;
        }

        public CredentialsDto GetUserByLogin(string login)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            string sqlQuery = "SELECT * FROM users WHERE Login = @login";

            return db.QuerySingle<CredentialsDto>(sqlQuery, new { login });
        }

        public CredentialsDto GetUserByEmail(string email)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            string sqlQuery = "SELECT * FROM users WHERE Email = @email";

            return db.QuerySingle<CredentialsDto>(sqlQuery, new { email });
        }
    }
}
