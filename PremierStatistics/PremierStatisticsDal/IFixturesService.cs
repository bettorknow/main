using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremierStatisticsDal
{
    public interface IFixturesService
    {
        IEnumerable<Fixture> Fixtures();
        IEnumerable<Fixture> FixturesByDate(DateTime from, DateTime to);
        IEnumerable<Fixture> FixturesByTeam(string team);
        IEnumerable<Fixture> FixturesByDateTeam(DateTime from, DateTime to, string team);
        IEnumerable<Fixture> FixturesForTeamCurrentWeek(string team);
        IEnumerable<Fixture> FixturesForCurrentWeek();
        GameWeek GetCurrentGameWeek();
    }
}
