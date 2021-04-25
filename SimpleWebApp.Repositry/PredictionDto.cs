using System;

namespace SimpleWebApp.Repository
{
    public class PredictionDto
    {
        public long Id { get; set; }
        public string PredictionString { get; set; }

        public PredictionDto() => (Id, PredictionString) = (-1, "");
    }
}