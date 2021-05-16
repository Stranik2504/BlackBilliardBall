using System.Collections.Generic;

namespace SimpleWebApp.Repository
{
    public interface IPredictionRepository
    {
        void SavePrediction(PredictionDto prediction);
        void SavePrediction(string PredictionString);
        void SavePredictions(params PredictionDto[] predictions);
        void SavePredictions(IEnumerable<PredictionDto> predictions);

        PredictionDto GetPredictionById(long id);
        PredictionDto[] GetAllPrediction();
        void UpdatePredictionById(PredictionDto prediction);
        void RemovePredictionById(long id);
        void RemovePredictionByPredictionString(string prediction);
    }
}
