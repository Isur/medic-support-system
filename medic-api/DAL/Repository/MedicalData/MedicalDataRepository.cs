using System;
using System.Collections.Generic;
using System.Linq;

namespace medic_api.DAL.Repository.MedicalData
{
    public class MedicalDataRepository : IMedicalDataRepository
    {
        private readonly DataContext _db;

        public MedicalDataRepository(DataContext db)
        {
            _db = db;
        }

        public string AddMedicalData(AddMedicalDataModel model)
        {
            var medical = new Models.MedicalData()
            {
                Age    = model.Age,
                Bmi = model.Bmi,
                Glucose = model.Glucose,
                Insulin = model.Insulin,
                Pregnancies = model.Pregnancies,
                UserId = model.UserId,
                BloodPressure = model.BloodPressure,
                SkinThickness = model.SkinThickness,
                DiabetesPedigreeFunction = model.DiabetesPedigreeFunction,
            };
            var newMedical = _db.MedicalDataset.Add(medical);
            _db.SaveChanges();
            return newMedical.Entity.MedicalDataId.ToString();
        }

        public Models.MedicalData GetMedicalData(string medicalDataId)
        {
            var entity = _db.MedicalDataset.FirstOrDefault(d => d.MedicalDataId == new Guid(medicalDataId));
            return entity;
        }

        public List<Models.MedicalData> GetMedicalDataList()
        {
            var entities = _db.MedicalDataset.ToList();
            return entities;
        }

        public List<Models.MedicalData> GetMedicalDataListByUser(string userId)
        {
            var entities = _db.MedicalDataset.Where(d => d.UserId == new Guid(userId));
            return entities.ToList();
        }
        
        public string UpdateMedicalData(UpdateMedicalDataModel model, string medicalDataId)
        {
            var medical = _db.MedicalDataset.FirstOrDefault(d => d.MedicalDataId == new Guid(medicalDataId));
            if (medical == null) throw new Exception("Medical Data not exists");
            if (model.Age.HasValue) medical.Age = model.Age.Value;
            if (model.Bmi.HasValue) medical.Bmi = model.Bmi.Value;
            if (model.Glucose.HasValue) medical.Glucose = model.Glucose.Value;
            if (model.Insulin.HasValue) medical.Insulin = model.Insulin.Value;
            if (model.Pregnancies.HasValue) medical.Pregnancies = model.Pregnancies.Value;
            if (model.BloodPressure.HasValue) medical.BloodPressure = model.BloodPressure.Value;
            if (model.SkinThickness.HasValue) medical.SkinThickness = model.SkinThickness.Value;
            if (model.DiabetesPedigreeFunction.HasValue) medical.DiabetesPedigreeFunction = model.DiabetesPedigreeFunction.Value;
            if (!string.IsNullOrEmpty(model.UserId)) medical.UserId = new Guid(model.UserId);
            _db.SaveChanges();
            return medical.UserId.ToString();
        }

        public string DeleteMedicalData(string medicalDataId)
        {
            var medical = _db.MedicalDataset.FirstOrDefault(d => d.MedicalDataId == new Guid(medicalDataId));
            if (medical == null) throw new Exception("Medical Data not exists");
            _db.MedicalDataset.Remove(medical);
            _db.SaveChanges();
            return medical.MedicalDataId.ToString();
        }

        public string SetPrediction(string medicalDataId, bool prediction)
        {
            var medical = _db.MedicalDataset.FirstOrDefault(d => d.MedicalDataId == new Guid(medicalDataId));
            if (medical == null) throw new Exception("Medical Data not exists");
            medical.Prediction = prediction;
            _db.SaveChanges();
            return medical.MedicalDataId.ToString();
        }

        public string SetResult(string medicalDataId, bool result)
        {
            var medical = _db.MedicalDataset.FirstOrDefault(d => d.MedicalDataId == new Guid(medicalDataId));
            if (medical == null) throw new Exception("Medical Data not exists");
            medical.Result = result;
            _db.SaveChanges();
            return medical.MedicalDataId.ToString();
        }
    }
}