using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace medic_api.Controllers.MedicalData.DTO
{
    public class MedicalDataUpdateRequest
    {
        public int? Pregnancies { get; set; }
        public int? Glucose { get; set; }
        public int? BloodPressure { get; set; }
        public int? SkinThickness { get; set; }
        public int? Insulin { get; set; }
        public double? DiabetesPedigreeFunction { get; set; }
        public double? Bmi { get; set; }
        public int? Age { get; set; }
        public string? UserId { get; set; }
    }
}