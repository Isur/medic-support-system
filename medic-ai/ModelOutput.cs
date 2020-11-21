using Microsoft.ML.Data;

namespace medic_ai
{
    public class ModelOutput
    {
        [ColumnName("Prediction")]
        public bool Prediction { get; set; }
        [ColumnName("Score")]
        public float Score { get; set; }
    }
}