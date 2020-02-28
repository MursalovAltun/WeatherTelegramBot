using System.Runtime.Serialization;

namespace Common.DTO
{
    public class TimezoneDTO
    {
        [DataMember(Name = "countryCode")]
        public string CountryCode { get; set; }

        [DataMember(Name = "countryName")]
        public string CountryName { get; set; }

        [DataMember(Name = "zoneName")]
        public string ZoneName { get; set; }

        [DataMember(Name = "abbreviation")]
        public string Abbreviation { get; set; }

        [DataMember(Name = "gmtOffset")]
        public int GmtOffset { get; set; }

        [DataMember(Name = "dst")]
        public string Dst { get; set; }

        [DataMember(Name = "timestamp")]
        public int Timestamp { get; set; }
    }
}