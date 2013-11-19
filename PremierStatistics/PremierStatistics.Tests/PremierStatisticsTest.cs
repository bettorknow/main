using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PremierStatisticsDal;
using PremierStatisticsLib;
using PremierStatisticsLib.Dto;

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

            //fixturesService.Setup(x => x.Fixtures()).Returns(GetFixtures());

            //var fixtures = ps.Fixtures();
            //Assert.That(fixtures.Count(), Is.EqualTo(2));
        }

        /*[Test]
        public void FixturesByDateReturnsExpectedList()
        {
            var fixturesService = new Mock<IFixturesService>();
            var ps = new PremierStatistics(fixturesService.Object);

            var dateFrom = new DateTime(2013, 1, 1);
            var dateTo = new DateTime(2013, 1, 2);

            fixturesService.Setup(x => x.FixturesByDate(dateFrom, dateTo)).Returns(GetFixtures());

            var fixtures = ps.FixturesByDate(dateFrom, dateTo);
            Assert.That(fixtures.Count(), Is.EqualTo(1));
        }

        private IEnumerable<Fixture> GetFixtures()
        {
            var fixture1 = new Fixture { AwayTeam = "Man City", Date = new DateTime(2013, 1, 1), Div = "E01", HomeTeam = "Arsenal" };
            var fixture2 = new Fixture { AwayTeam = "Man Utd", Date = new DateTime(2013, 1, 20), Div = "E01", HomeTeam = "Chelsea" };
            var fixtures = new List<Fixture> { fixture1, fixture2 };
            return fixtures;
        }*/

    }
}
