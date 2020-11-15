using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            return Ok("This is not implemented yet.");
        }
    }
}