using System;
using System.Runtime.Serialization;

namespace PremierStatisticsLib.Dto
{
    [DataContract]
    public class FixtureDto
    {
        public FixtureDto(string homeTeam, string awayTeam)
        {
            Home = new TeamDto(homeTeam);
            Away = new TeamDto(awayTeam);
        }

        [DataMember]
        public DateTime MatchDate { get; set; }
        [DataMember]
        public TeamDto Home { get; set; }
        [DataMember]
        public TeamDto Away { get; set; }
    }
}
