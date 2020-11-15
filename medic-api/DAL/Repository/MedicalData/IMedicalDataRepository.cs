using System.Collections.Generic;

namespace medic_api.DAL.Repository.MedicalData
{
    public interface IMedicalDataRepository
    {
        public string AddMedicalData(AddMedicalDataModel model);
        public Models.MedicalData GetMedicalData(string medicalDataId);
        public List<Models.MedicalData> GetMedicalDataList();
        public List<Models.MedicalData> GetMedicalDataListByUser(string userId);
        public string UpdateMedicalData(UpdateMedicalDataModel model, string medicalDataId);
        public string DeleteMedicalData(string medicalDataId);
        public string SetPrediction(string medicalDataId, bool prediction);
        public string SetResult(string medicalDataId, bool result);
    }
}