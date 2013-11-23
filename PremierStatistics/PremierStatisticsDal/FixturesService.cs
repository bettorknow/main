using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using PremierStatisticsLib.Interfaces;

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
            return Fixtures().Where(dateFixture => dateFixture.MatchDate >= dateRange.From && dateFixture.MatchDate <= dateRange.To);
        }

        public IEnumerable<Fixture> FixturesByTeam(ITeam team)
        {
            return Fixtures().Where(teamName => teamName.HomeTeam == team.Name || teamName.AwayTeam == team.Name);
        }

        public IEnumerable<Fixture> FixturesByDateTeam(IDateRange dateRange, ITeam team)
        {
            return Fixtures().Where(dateFixture => dateFixture.MatchDate >= dateRange.From && dateFixture.MatchDate <= dateRange.To)
                                     .Where(teamName => teamName.HomeTeam == team.Name || teamName.AwayTeam == team.Name);
        }

        public IEnumerable<Fixture> FixturesForTeamCurrentWeek(ITeam team)
        {
            return Fixtures().Where(dateFixture => dateFixture.MatchDate >= _gameweek.WeekFrom && dateFixture.MatchDate <= _gameweek.WeekTo)
                                     .Where(teamName => teamName.HomeTeam == team.Name || teamName.AwayTeam == team.Name);
        }

        public IEnumerable<Fixture> FixturesForCurrentWeek()
        {
            return Fixtures().Where(dateFixture => dateFixture.MatchDate >= _gameweek.WeekFrom && dateFixture.MatchDate <= _gameweek.WeekTo);
        }

        public GameWeek GetCurrentGameWeek()
        {
            return new GameWeek { WeekFrom = _today.Date.AddDays(-1 - (int)_today.DayOfWeek), 
                                  WeekTo = _today.AddDays(5 - (int)_today.DayOfWeek) };
        }
    }
}
