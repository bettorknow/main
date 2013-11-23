using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PremierStatisticsDal;
using PremierStatisticsLib;
using PremierStatisticsLib.Concrete;
using PremierStatisticsLib.Dto;
using PremierStatisticsLib.Interfaces;
using PremierStatisticsLib.Service;

namespace PremierStatistics.Tests
{
    [TestFixture]
    public class PremierStatisticsTest
    {
        [Test]
        public void CanClassBeInstantiated()
        {
            var ps = new PremierStatistics();
            Assert.IsInstanceOf<PremierStatistics>(ps);
            Assert.IsInstanceOf<IPremierStatistics>(ps);
        }

        [Test]
        public void FixturesReturnsFullList()
        {
            var fixturesService = new Mock<IFixturesService>();
            var ps = new PremierStatistics(fixturesService.Object);

            fixturesService.Setup(x => x.Fixtures()).Returns(GetFixtures());

            var fixtures = ps.Fixtures();
            Assert.That(fixtures.Count(), Is.EqualTo(2));
        }

        [Test]
        public void FixturesByDateReturnsExpectedList()
        {
            var fixturesService = new Mock<IFixturesService>();
            var ps = new PremierStatistics(fixturesService.Object);

            var dateFrom = new DateTime(2013, 1, 1);
            var dateTo = new DateTime(2013, 1, 2);
            var range = new DateRange(dateFrom, dateTo);

            fixturesService.Setup(x => x.FixturesByDate(range)).Returns(GetFixtures().Where(dateFixture => dateFixture.MatchDate >= dateFrom && dateFixture.MatchDate <= dateTo));

            var fixtures = ps.FixturesByDate(range);

            fixturesService.VerifyAll();

            Assert.That(fixtures.Count(), Is.EqualTo(1));
        }

        [Test]
        public void FixturesByTeamReturnsExpectedList()
        {
            var fixturesService = new Mock<IFixturesService>();
            var ps = new PremierStatistics(fixturesService.Object);

            var team = new TeamDto("Man Utd");


            fixturesService.Setup(x => x.FixturesByTeam(team)).Returns(GetFixtures().Where(teamToMatch => teamToMatch.AwayTeam == "Man Utd" || teamToMatch.HomeTeam == "Man Utd"));

            var fixtures = ps.FixturesByTeam(team);

            fixturesService.VerifyAll();

            Assert.That(fixtures.Count(), Is.EqualTo(2));
        }

        [Test]
        public void FixturesByTeamAndDateReturnsExpectedList()
        {
            var fixturesService = new Mock<IFixturesService>();
            var ps = new PremierStatistics(fixturesService.Object);

            var team = new TeamDto("Man Utd");
            var dateFrom = new DateTime(2013, 1, 1);
            var dateTo = new DateTime(2013, 1, 2);
            var range = new DateRange(dateFrom, dateTo);

            fixturesService.Setup(x => x.FixturesByDateTeam(range, team)).Returns(GetFixtures()
                .Where(teamName => teamName.AwayTeam == "Man Utd" || teamName.HomeTeam == "Man Utd")
                .Where(dateFixture => dateFixture.MatchDate >= dateFrom && dateFixture.MatchDate <= dateTo));

            var fixtures = ps.FixturesByDateTeam(range, team);

            fixturesService.VerifyAll();

            Assert.That(fixtures.Count(), Is.EqualTo(1));
        }

        [Test]
        public void FixturesForCurrentWeekReturnsExpectedList()
        {
            var fixturesService = new Mock<IFixturesService>();
            var ps = new PremierStatistics(fixturesService.Object);

            fixturesService.Setup(x => x.FixturesForCurrentWeek()).Returns(GetTodaysFixtures());
            ps.FixturesForCurrentWeek();

            fixturesService.VerifyAll();
        }

        [Test]
        public void FixturesForTeamCurrentWeekReturnsExpectedList()
        {
            var fixturesService = new Mock<IFixturesService>();
            var ps = new PremierStatistics(fixturesService.Object);

            var team = new TeamDto("Man Utd");
            fixturesService.Setup(x => x.FixturesForTeamCurrentWeek(team)).Returns(GetTodaysFixtures());
            ps.FixturesForTeamCurrentWeek(team);

            fixturesService.VerifyAll();
        }

        [Test]
        public void FixturesHaveExpectedValues()
        {
            var fixturesService = new Mock<IFixturesService>();
            var ps = new PremierStatistics(fixturesService.Object);

            fixturesService.Setup(x => x.Fixtures()).Returns(GetFixtures());

            var fixtures = ps.Fixtures();
            Assert.That(fixtures[0].Away.Name, Is.SameAs("Man City"));
            Assert.That(fixtures[1].Away.Name, Is.SameAs("Man Utd"));

            Assert.That(fixtures[0].MatchDate, Is.EqualTo(new DateTime(2013, 1, 1)));
            Assert.That(fixtures[1].MatchDate, Is.EqualTo(new DateTime(2013, 1, 3)));

            Assert.That(fixtures[0].Home.Name, Is.SameAs("Man Utd"));
            Assert.That(fixtures[1].Home.Name, Is.SameAs("Chelsea"));
        }

        private IEnumerable<Fixture> GetFixtures()
        {
            var fixture1 = new Fixture { AwayTeam = "Man City", MatchDate = new DateTime(2013, 1, 1), Div = "E01", HomeTeam = "Man Utd" };
            var fixture2 = new Fixture { AwayTeam = "Man Utd", MatchDate = new DateTime(2013, 1, 3), Div = "E01", HomeTeam = "Chelsea" };
            var fixtures = new List<Fixture> { fixture1, fixture2 };
            return fixtures;
        }

        private IEnumerable<Fixture> GetTodaysFixtures()
        {
            var fixture1 = new Fixture { AwayTeam = "Man City", MatchDate = DateTime.Now, Div = "E01", HomeTeam = "Man Utd" };
            var fixture2 = new Fixture { AwayTeam = "Man Utd", MatchDate = DateTime.Now.AddMonths(3), Div = "E01", HomeTeam = "Chelsea" };
            var fixtures = new List<Fixture> { fixture1, fixture2 };
            return fixtures;
        }

    }
}
