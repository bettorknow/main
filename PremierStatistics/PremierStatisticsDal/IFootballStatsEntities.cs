using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremierStatisticsDal
{
    public interface IFootballStatsEntities //set on the context through the template
    {
        IDbSet<Fixture> Fixtures { get; set; }
    }
}
