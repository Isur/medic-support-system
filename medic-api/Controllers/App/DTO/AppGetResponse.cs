using System.Collections.Generic;

namespace medic_api.Controllers.DTO
{
    public class AppGetResponse
    {
        public string Host { get; set; }
        public string Path { get; set; }
        public IList<string> Ips { get; set; }
    }
}