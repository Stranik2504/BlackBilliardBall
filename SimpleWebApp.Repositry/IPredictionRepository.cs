using System.Collections.Generic;

namespace SimpleWebApp.Repository
{
    public interface IPredictionRepository
    {
        void SavePrediction(PredictionDto prediction);
        void SavePredictions(params PredictionDto[] predictions);
        void SavePredictions(IEnumerable<PredictionDto> predictions);
    }
}
