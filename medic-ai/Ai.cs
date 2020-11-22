using System;
using System.Collections.Generic;
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

        public double DatabaseLearning(List<ModelInput> data)
        {
            return _model.TrainingWithDatabase(data);
        }
    }
}