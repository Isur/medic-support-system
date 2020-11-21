using System;
using System.IO;
using System.Net;

namespace medic_ai
{
    public class Ai
    {
        private readonly Model _model;
        public Ai()
        {
            _model = new Model(@"diabetes.csv");
        }

        public double InitialLearning()
        {
            return _model.InitialTraining();
        }

        public ModelOutput Predict(PredictionModel input)
        {
            return _model.Predict(input);
        }
    }
}