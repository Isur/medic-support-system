using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using medic_ai;
using medic_api.Controllers.MedicalData.DTO;
using medic_api.DAL.Repository.MedicalData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace medic_api.Controllers.MedicalData
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "Patient")]
    public class MedicalDataController : ControllerBase
    {
        private readonly IMedicalDataRepository _medicalDataRepository;

        public MedicalDataController(IMedicalDataRepository medicalDataRepository)
        {
            _medicalDataRepository = medicalDataRepository;
        }

        [HttpGet]
        public ActionResult<List<DAL.Models.MedicalData>> Get()
        {
            var medicalDataset = _medicalDataRepository.GetMedicalDataList();
            return Ok(medicalDataset);
        }
        
        [HttpGet, Route("me")]
        public ActionResult<List<DAL.Models.MedicalData>> GetMyMedicalData()
        {
            var userId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
            var medicalDataset = _medicalDataRepository.GetMedicalDataListByUser(userId);
            return Ok(medicalDataset);
        }

        [HttpGet, Route("{id}")]
        public ActionResult<DAL.Models.MedicalData> Get(string id)
        {
            var medicalData = _medicalDataRepository.GetMedicalData(id);
            return Ok(medicalData);
        }

        [HttpPost]
        [Authorize(Policy = "Doctor")]
        public ActionResult<string> Get([FromBody] MedicalDataAddRequest body)
        {
            AddMedicalDataModel model = new AddMedicalDataModel()
            {
                Age = body.Age,
                Bmi = body.Bmi,
                Glucose = body.Glucose,
                Insulin = body.Insulin,
                Pregnancies = body.Pregnancies,
                BloodPressure = body.BloodPressure,
                SkinThickness = body.SkinThickness,
                UserId = new Guid(body.UserId),
                DiabetesPedigreeFunction = body.DiabetesPedigreeFunction,
            };
            var newId = _medicalDataRepository.AddMedicalData(model);
            return Ok(newId);
        }

        [HttpPatch, Route("{id}")]
        [Authorize(Policy = "Doctor")]
        public ActionResult<string> Patch(string id, [FromBody] MedicalDataUpdateRequest body)
        {
            UpdateMedicalDataModel model = new UpdateMedicalDataModel()
            {
                Age = body.Age,
                Bmi = body.Bmi,
                Glucose = body.Glucose,
                Insulin = body.Insulin,
                Pregnancies = body.Pregnancies,
                BloodPressure = body.BloodPressure,
                SkinThickness = body.SkinThickness,
                UserId = body.UserId,
                DiabetesPedigreeFunction = body.DiabetesPedigreeFunction,
            };

            var updatedId = _medicalDataRepository.UpdateMedicalData(model, id);

            return Ok(updatedId);
        }

        [HttpPost, Route("{id}/result")]
        [Authorize(Policy = "Doctor")]
        public ActionResult<string> SetResult(string id, [FromBody] SetResultRequest body)
        {
            var resultId = _medicalDataRepository.SetResult(id, body.Result);
            return Ok(resultId);
        }
        
        [HttpPost, Route("{id}/prediction")]
        [Authorize(Policy = "Doctor")]
        public ActionResult<string> SetResult(string id)
        {
            Ai ai = new Ai();
            var data = _medicalDataRepository.GetMedicalData(id);
            PredictionModel model = new PredictionModel()
            {
                Age = data.Age,
                Bmi = (float) data.Bmi,
                Glucose = data.Glucose,
                Insulin = data.Insulin,
                Pregnancies = data.Pregnancies,
                BloodPressure = data.BloodPressure,
                SkinThickness = data.SkinThickness,
                DiabetesPedigreeFunction = (float) data.DiabetesPedigreeFunction,

            };
            var result = ai.Predict(model);
            _medicalDataRepository.SetPrediction(id, result.Prediction);
            return Ok(result);
        }

        [HttpPost]
        [Route("learn")]
        [Authorize(Policy = "Admin")]
        public ActionResult<double> Learn()
        {
            Ai ai = new Ai();
            var data = _medicalDataRepository.GetMedicalDataList().Where(d => d.Result != null).ToList();
            var preparedList = new List<ModelInput>();
            foreach (var medicalData in data)
            {
                var item = new ModelInput()
                {
                    Age = medicalData.Age,
                    Bmi = (float)medicalData.Bmi,
                    Glucose = medicalData.Glucose,
                    Insulin = medicalData.Insulin,
                    Outcome = medicalData.Result != null && (bool) medicalData.Result,
                    Pregnancies = medicalData.Pregnancies,
                    BloodPressure = medicalData.BloodPressure,
                    SkinThickness = medicalData.SkinThickness,
                    DiabetesPedigreeFunction = (float) medicalData.DiabetesPedigreeFunction,
                };
                preparedList.Add(item);
            }

            if (preparedList.Count < 15)
            {
                return Problem("Not enough data");
            }
            var accuracy = ai.DatabaseLearning(preparedList);
            return Ok(accuracy);
        }

        [HttpPost]
        [Route("learnfromfile")]
        [Authorize(Policy = "Admin")]
        public ActionResult<double> LearnFromFile()
        {
            Ai ai = new Ai();
            var accuracy = ai.InitialLearning();
            return Ok(accuracy);
        }
    }
}