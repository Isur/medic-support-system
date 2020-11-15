using Newtonsoft.Json;

namespace medic_api.Controllers.MedicalData.DTO
{
    public class SetResultRequest
    {
        [JsonRequired]
        public bool Result { get; set; }
    }
}