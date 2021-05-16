using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using Dapper;

namespace SimpleWebApp.Repository
{
    public class PredictionDatabseRepository : IPredictionRepository
    {
        public void SavePrediction(PredictionDto prediction)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            string sqlQuery = "INSERT INTO predictions (PredictionString) VALUES (@PredictionString)";

            int rowsAffected = db.Execute(sqlQuery, prediction);
        }

        public void SavePrediction(string PredictionString) => SavePrediction(new PredictionDto() { PredictionString = PredictionString });

        public void SavePredictions(params PredictionDto[] predictions) => predictions.ToList().ForEach(x => SavePrediction(x));

        public void SavePredictions(IEnumerable<PredictionDto> predictions) => predictions.ToList().ForEach(x => SavePrediction(x));

        public PredictionDto GetPredictionById(long id)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            string sqlQuery = $"SELECT * FROM predictions WHERE Id = @Id";

            return db.QuerySingle<PredictionDto>(sqlQuery, new PredictionDto() { Id = id });
        }

        public PredictionDto[] GetAllPrediction()
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            string sqlQuery = $"SELECT * FROM predictions";

            return db.Query<PredictionDto>(sqlQuery).ToArray();
        }

        public void UpdatePredictionById(PredictionDto prediction)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            string sqlQuery = $"UPDATE predictions SET PredictionString = @PredictionString WHERE Id = @Id";

            int rowsAffected = db.Execute(sqlQuery, prediction);
        }

        public void RemovePredictionById(long id)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            string sqlQuery = $"DELETE FROM predictions WHERE Id = @id";

            int rowsAffected = db.Execute(sqlQuery, new { id });
        }

        public void RemovePredictionByPredictionString(string prediction)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            string sqlQuery = $"DELETE FROM predictions WHERE PredictionString = @prediction";

            int rowsAffected = db.Execute(sqlQuery, new { prediction });
        }
    }
}
