using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace medic_api.DAL.Models
{
    [Table("medicaldata")]
    public partial class MedicalData
    {
        [Column("id")]
        public Guid MedicalDataId { get; set; }
        [Column("pregnancies")]
        public int Pregnancies { get; set; }
        [Column("glucose")]
        public int Glucose { get; set; }
        [Column("bloodpressure")]
        public int BloodPressure { get; set; }
        [Column("skinthickness")]
        public int SkinThickness { get; set; }
        [Column("insulin")]
        public int Insulin { get; set; }
        [Column("diabetespedigreefunction")]
        public double DiabetesPedigreeFunction { get; set; }
        [Column("bmi")]
        public double Bmi { get; set; }
        [Column("age")]
        public int Age { get; set; }
        [Column("prediction")]
        public bool? Prediction { get; set; }
        [Column("result")]
        public bool? Result { get; set; }
        [Column("userid")]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}