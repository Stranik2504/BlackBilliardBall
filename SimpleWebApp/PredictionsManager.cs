using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp
{
    public class PredictionsManager
    {
        private readonly List<string> _predictions = new List<string> { "Привет", "как", "твои", "дела", "?" };

        public string GetRandomPrediction() => _predictions[new Random().Next(0, _predictions.Count)];

        public void AddPrediction(string prediction) => _predictions.Add(prediction);

        public void RemovePrediction(string prediction) => _predictions.Remove(prediction);
    }
}
