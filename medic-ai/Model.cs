using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;

namespace medic_ai
{
    public class Model
    {
        private readonly string _filePath;
        private ITransformer _model;
        public Model(string file)
        {
            _filePath = file;
            _loadModel();
        }

        public ModelOutput Predict(PredictionModel input)
        {
            var ctx = new MLContext();
            var model = ctx.Model.Load("model.zip", out _);
            PredictionEngine<ModelInput, ModelOutput> predictionEngine =
                ctx.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);
            ModelInput data = new ModelInput()
            {
                Age = input.Age,
                Bmi = input.Bmi,
                Glucose = input.Glucose,
                Insulin = input.Insulin,
                Pregnancies = input.Pregnancies,
                BloodPressure = input.BloodPressure,
                SkinThickness = input.SkinThickness,
                DiabetesPedigreeFunction = input.DiabetesPedigreeFunction,
            };
            ModelOutput prediction = predictionEngine.Predict(data);
            return prediction;
        }

        private void _loadModel()
        {
            var ctx = new MLContext();
            if(File.Exists("model.zip"))
            {
                _model = ctx.Model.Load("model.zip", out _);
            }
            else
            {
                InitialTraining();
            } 
        }

        public double TrainingWithDatabase(List<ModelInput> listData)
        {
            var ctx = new MLContext();
            IDataView data = ctx.Data.LoadFromEnumerable<ModelInput>(listData);
            return Training(data, ctx);
        }

        private double Training(IDataView data, MLContext ctx)
        {
            var split = ctx.Data.TrainTestSplit(data, testFraction: 0.25);

            var features = split.TrainSet.Schema
                .Select(col => col.Name)
                .Where(col => col != "Label")
                .ToArray();
            var trainer =  new LbfgsLogisticRegressionBinaryTrainer.Options()
            {
                MaximumNumberOfIterations = 100,
            };
            var pipeline = ctx.Transforms.Concatenate("Features", features).Append(ctx.BinaryClassification.Trainers.LbfgsLogisticRegression());

            var model = pipeline.Fit(split.TrainSet);

            var predictions = model.Transform(split.TestSet);

            var metrics = ctx.BinaryClassification.Evaluate(predictions);

            Console.WriteLine($"Accuracy - {metrics.Accuracy}");
            ctx.Model.Save(model, data.Schema, "model.zip");
            _model = model;
            return metrics.Accuracy;
        }
        
        public double InitialTraining()
        {
            var ctx = new MLContext();
            var data = ctx.Data.LoadFromTextFile<ModelInput>(path: _filePath, hasHeader: true, separatorChar: ',');
            return Training(data, ctx);
        }
    }
}