using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleWebApp.Repository
{
    public class PredictionDatabseRepository : IPredictionRepository
    {
        public void SavePrediction(PredictionDto prediction)
        {
            throw new NotImplementedException();
        }

        public void SavePredictions(params PredictionDto[] predictions) => predictions.ToList().ForEach(x => SavePrediction(x));

        public void SavePredictions(IEnumerable<PredictionDto> predictions) => predictions.ToList().ForEach(x => SavePrediction(x));
    }
}
