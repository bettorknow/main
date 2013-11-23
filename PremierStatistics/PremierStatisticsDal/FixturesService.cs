using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using PremierStatisticsLib;

namespace PremierStatisticsDal
{
    public class FixturesService : IFixturesService
    {
        private readonly IFootballStatsEntities _fixtures;
        private readonly GameWeek _gameweek;
        private readonly DateTime _today;

        [InjectionConstructor]
        public FixturesService(IFootballStatsEntities fixtures)
            : this(DateTime.Now, fixtures)
        {
        }

        public FixturesService(DateTime gameweekDate, IFootballStatsEntities fixtures)
        {
            _today = gameweekDate;
            _fixtures = fixtures;
            _gameweek = GetCurrentGameWeek();
        }

        public IEnumerable<Fixture> Fixtures()
        {
            return _fixtures.Fixtures;
        }

        public IEnumerable<Fixture> FixturesByDate(IDateRange dateRange)
        {
            return Fixtures().Where(dateFixture => dateFixture.Date >= dateRange.From && dateFixture.Date <= dateRange.To);
        }

        public IEnumerable<Fixture> FixturesByTeam(string team)
        {
            return Fixtures().Where(teamName => teamName.HomeTeam == team || teamName.AwayTeam == team);
        }

        public IEnumerable<Fixture> FixturesByDateTeam(IDateRange dateRange, string team)
        {
            return Fixtures().Where(dateFixture => dateFixture.Date >= dateRange.From && dateFixture.Date <= dateRange.To)
                                     .Where(teamName => teamName.HomeTeam == team || teamName.AwayTeam == team);
        }

        public IEnumerable<Fixture> FixturesForTeamCurrentWeek(string team)
        {
            return Fixtures().Where(dateFixture => dateFixture.Date >= _gameweek.WeekFrom && dateFixture.Date <= _gameweek.WeekTo)
                                     .Where(teamName => teamName.HomeTeam == team || teamName.AwayTeam == team);
        }

        public IEnumerable<Fixture> FixturesForCurrentWeek()
        {
            return Fixtures().Where(dateFixture => dateFixture.Date >= _gameweek.WeekFrom && dateFixture.Date <= _gameweek.WeekTo);
        }

        public GameWeek GetCurrentGameWeek()
        {
            return new GameWeek { WeekFrom = _today.Date.AddDays(-1 - (int)_today.DayOfWeek), 
                                  WeekTo = _today.AddDays(5 - (int)_today.DayOfWeek) };
        }
    }
}
