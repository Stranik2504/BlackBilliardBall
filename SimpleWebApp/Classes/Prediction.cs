using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp.Classes
{
    public class Prediction
    {
        public long Id { get; set; }
        public string PredictionString { get; set; }

        public Prediction() => (Id, PredictionString) = (-1, "");
    }
}
