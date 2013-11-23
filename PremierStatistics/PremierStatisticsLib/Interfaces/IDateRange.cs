using System;

namespace PremierStatisticsLib.Interfaces
{
    public interface IDateRange
    {
        DateTime From { get; set; }
        DateTime To { get; set; }
    }
}
