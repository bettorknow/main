using System;
using System.Runtime.Serialization;

namespace PremierStatisticsLib.Dto
{
    [DataContract]
    public class FixtureDto
    {
        [DataMember]
        public DateTime DateTime { get; set; }
        [DataMember]
        public TeamDto Home { get; set; }
        [DataMember]
        public TeamDto Away { get; set; }
    }
}
