using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PremierStatisticsLib.Interfaces;

namespace PremierStatisticsLib.Concrete
{
    public class DateRange : IDateRange
    {
        public DateRange(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }

        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
