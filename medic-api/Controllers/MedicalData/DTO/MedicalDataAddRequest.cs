using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace medic_api.Controllers.MedicalData.DTO
{
    public class MedicalDataAddRequest
    {
        [JsonRequired]
        public int Pregnancies { get; set; }
        [JsonRequired]
        public int Glucose { get; set; }
        [JsonRequired]
        public int BloodPressure { get; set; }
        [JsonRequired]
        public int SkinThickness { get; set; }
        [JsonRequired]
        public int Insulin { get; set; }
        [JsonRequired]
        public double DiabetesPedigreeFunction { get; set; }
        [JsonRequired]
        public double Bmi { get; set; }
        [JsonRequired]
        public int Age { get; set; }
        [JsonRequired]
        public string UserId { get; set; }
    }
}