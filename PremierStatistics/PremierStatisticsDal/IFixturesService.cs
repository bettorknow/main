using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PremierStatisticsLib;
using PremierStatisticsLib.Interfaces;

namespace PremierStatisticsDal
{
    public interface IFixturesService
    {
        IEnumerable<Fixture> Fixtures();
        IEnumerable<Fixture> FixturesByDate(IDateRange dateRange);
        IEnumerable<Fixture> FixturesByTeam(ITeam team);
        IEnumerable<Fixture> FixturesByDateTeam(IDateRange dateRange, ITeam team);
        IEnumerable<Fixture> FixturesForTeamCurrentWeek(ITeam team);
        IEnumerable<Fixture> FixturesForCurrentWeek();
        GameWeek GetCurrentGameWeek();
    }
}
