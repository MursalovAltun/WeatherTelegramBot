using System.Runtime.Serialization;

namespace Common.DTO
{
    public class GeoCodeDTO
    {
        [DataMember(Name = "city")]
        public string City { get; set; }

        [DataMember(Name = "prov")]
        public string Province { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }
    }
}