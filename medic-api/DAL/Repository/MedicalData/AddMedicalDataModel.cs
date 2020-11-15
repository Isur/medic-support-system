using System;

namespace medic_api.DAL.Repository.MedicalData
{
    public class AddMedicalDataModel
    {
        public int Pregnancies { get; set; }
        public int Glucose { get; set; }
        public int BloodPressure { get; set; }
        public int SkinThickness { get; set; }
        public int Insulin { get; set; }
        public double DiabetesPedigreeFunction { get; set; }
        public double Bmi { get; set; }
        public int Age { get; set; }
        public Guid UserId { get; set; }
    }
}