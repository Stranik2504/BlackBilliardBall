using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleWebApp.Repository
{
    public class PredictionDatabaseInMemory : IPredictionRepository
    {
        private readonly List<PredictionDto> _predictions = new();

        public void SavePrediction(PredictionDto prediction)
        {
            long maxId = 0;
            _predictions.ForEach(x => { if (x.Id > maxId) maxId = x.Id; });
            prediction.Id = maxId + 1;
            _predictions.Add(prediction);
        }

        public void SavePrediction(string PredictionString) => SavePrediction(new PredictionDto() { PredictionString = PredictionString });

        public void SavePredictions(params PredictionDto[] predictions) => _predictions.AddRange(predictions);

        public void SavePredictions(IEnumerable<PredictionDto> predictions) => _predictions.AddRange(predictions);

        public PredictionDto[] GetAllPrediction() => _predictions.ToArray();

        public PredictionDto GetPredictionById(long id) => _predictions.Where(x => x.Id == id).FirstOrDefault();

        public void RemovePredictionById(long id)
        {
            for (int i = 0; i < _predictions.Count; i++)
            {
                if (_predictions[i].Id == id)
                {
                    _predictions.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemovePredictionByPredictionString(string prediction)
        {
            for (int i = 0; i < _predictions.Count; i++)
            {
                if (_predictions[i].PredictionString == prediction)
                {
                    _predictions.RemoveAt(i);
                    break;
                }
            }
        }

        public void UpdatePredictionById(PredictionDto prediction) => GetPredictionById(prediction.Id).PredictionString = prediction.PredictionString;
    }
}
