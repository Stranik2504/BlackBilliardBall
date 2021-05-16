using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleWebApp.Classes;
using SimpleWebApp.Repository;

namespace SimpleWebApp
{
    public class PredictionsManager
    {
        private readonly IPredictionRepository _repository;
        private readonly Random _random = new();

        public PredictionsManager(IPredictionRepository repository) => _repository = repository;

        public string GetRandomPrediction()
        {
            var predictions = _repository.GetAllPrediction();

            if (predictions.Length > 0) { return predictions[_random.Next(0, predictions.Length)].PredictionString; }
            else { return "Когда-нибудь здесь будет саркастичный ответ, но пока вот тебе салат. 🥗"; }
        }

        public void AddPrediction(string prediction) => _repository.SavePrediction(prediction);

        public void RemovePrediction(string prediction) => _repository.RemovePredictionByPredictionString(prediction);

        public void RemovePrediction(long id) => _repository.RemovePredictionById(id);

        public IReadOnlyList<Prediction> GetAllPredictions() => _repository.GetAllPrediction().Select(x => new Prediction() { PredictionString = x.PredictionString, Id = x.Id }).ToList().AsReadOnly();

        public void Edit(long numPrediction, string newPrediction) => _repository.UpdatePredictionById(new PredictionDto() { Id = numPrediction, PredictionString = newPrediction });
    }
}
