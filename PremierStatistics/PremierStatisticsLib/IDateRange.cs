using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremierStatisticsLib
{
    public interface IDateRange
    {
        DateTime From { get; set; }
        DateTime To { get; set; }
    }
}
