using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PremierStatisticsLib;

namespace PremierStatisticsDal
{
    public interface IFixturesService
    {
        IEnumerable<Fixture> Fixtures();
        IEnumerable<Fixture> FixturesByDate(IDateRange dateRange);
        IEnumerable<Fixture> FixturesByTeam(string team);
        IEnumerable<Fixture> FixturesByDateTeam(IDateRange dateRange, string team);
        IEnumerable<Fixture> FixturesForTeamCurrentWeek(string team);
        IEnumerable<Fixture> FixturesForCurrentWeek();
        GameWeek GetCurrentGameWeek();
    }
}
