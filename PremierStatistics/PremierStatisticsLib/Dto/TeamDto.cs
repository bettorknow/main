using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PremierStatisticsLib.Dto
{
    [DataContract]
    public class TeamDto
    {
        [DataMember]
        public string Name { get; set; }
    }
}
