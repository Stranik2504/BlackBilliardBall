using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp
{
    public class PredictionsManager
    {
        private readonly List<string> _predictions = new List<string> { "Привет", "как", "твои", "дела", "?" };

        public string GetRandomPrediction()
        {
            if (_predictions.Count > 0) { return _predictions[new Random().Next(0, _predictions.Count)]; }
            else { return "Когда-нибудь здесь будет саркастичный ответ, но пока вот тебе салат. 🥗"; }
        }

        public void AddPrediction(string prediction) => _predictions.Add(prediction);

        public void RemovePrediction(string prediction) => _predictions.Remove(prediction);

        public IReadOnlyList<string> GetAllPredictions() => _predictions.AsReadOnly();

        public void Edit(int numPrediction, string newPrediction)
        {
            if (numPrediction < _predictions.Count)
            {
                _predictions[numPrediction] = newPrediction;
            }
        }
    }
}
