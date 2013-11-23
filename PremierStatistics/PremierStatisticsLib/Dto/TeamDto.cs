using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PremierStatisticsLib.Interfaces;

namespace PremierStatisticsLib.Dto
{
    [DataContract]
    public class TeamDto : ITeam
    {
        public TeamDto(string name)
        {
            Name = name;
        }

        [DataMember]
        public string Name { get; set; }
    }
}
