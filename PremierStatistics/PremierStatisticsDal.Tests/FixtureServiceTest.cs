using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PremierStatisticsLib;

namespace PremierStatisticsDal.Tests
{
    [TestFixture]
    public class FixtureServiceTest
    {
        [Test]
        public void CanClassBeInstantiatedWithoutConstrctorParamater()
        {
            FixturesService fs = new FixturesService(null);
            Assert.IsNotNull(fs);
            Assert.IsInstanceOf<IFixturesService>(fs);
            Assert.IsInstanceOf<FixturesService>(fs);
        }

        [Test]
        public void CanClassBeInstantiatedWithConstrctorParamater()
        {
            FixturesService fs = new FixturesService(DateTime.Now, null);
            Assert.IsNotNull(fs);
            Assert.IsInstanceOf<IFixturesService>(fs);
            Assert.IsInstanceOf<FixturesService>(fs);
        }

        [Test, TestCaseSource(typeof(EveryDayOfYear), "TestCases")]
        public void SaturdayToFridayGameWeek(DateTime dateToTest)
        {
            FixturesService fs = new FixturesService(dateToTest, null);
            var gw = fs.GetCurrentGameWeek();
            Assert.AreEqual(gw.WeekFrom.DayOfWeek, DayOfWeek.Saturday);
            Assert.AreEqual(gw.WeekTo.DayOfWeek, DayOfWeek.Friday);
            Assert.AreEqual((gw.WeekTo - gw.WeekFrom).Days, 6);
        }

        [Test]
        public void AreAllFixturesReturned()
        {
            var db = new Mock<IFootballStatsEntities>();

            db.Setup(x => x.Fixtures).Returns(ConfigureEntities());

            FixturesService fs = new FixturesService(db.Object);
            var fixtureList = fs.Fixtures();

            Assert.That(fixtureList.Count(), Is.EqualTo(7));
        }

        private FixturesService GetConfiguredFixtureService()
        {
            var db = new Mock<IFootballStatsEntities>();
            db.Setup(x => x.Fixtures).Returns(ConfigureEntities());
            var fs = new FixturesService(db.Object);
            return fs;
        }

        [Test]
        public void AreAllFixturesByDateReturned()
        {
            var fs = GetConfiguredFixtureService();
            var range = new Mock<IDateRange>();

            range.SetupGet(x => x.From).Returns(new DateTime(2013, 1, 1));
            range.SetupGet(x => x.To).Returns(new DateTime(2013, 2, 1));

            var fixtureList = fs.FixturesByDate(range.Object);

            Assert.That(fixtureList.Count(), Is.EqualTo(4));

            range.VerifyGet(x => x.From);
            range.VerifyGet(x => x.To);

            range.Verify();
        }

        [Test]
        public void AreAllFixturesByTeamReturned()
        {
            var fs = GetConfiguredFixtureService(); 
            var fixtureList = fs.FixturesByTeam("Man Utd");

            Assert.That(fixtureList.Count(), Is.EqualTo(5));
        }

        [Test]
        public void AreAllFixturesByDateTeamReturned()
        {
            var fs = GetConfiguredFixtureService();
            var range = new Mock<IDateRange>();

            range.SetupGet(x => x.From).Returns(new DateTime(2013, 1, 1));
            range.SetupGet(x => x.To).Returns(new DateTime(2013, 2, 1));
            var fixtureList = fs.FixturesByDateTeam(range.Object, "Man Utd");

            Assert.That(fixtureList.Count(), Is.EqualTo(3));
        }

        [Test]
        public void AreAllFixturesForCurrentWeekReturned()
        {
            var db = new Mock<IFootballStatsEntities>();
            db.Setup(x => x.Fixtures).Returns(ConfigureEntities());
            var fs = new FixturesService(new DateTime(2013, 1, 1), db.Object);
            
            var fixtureList = fs.FixturesForCurrentWeek();

            Assert.That(fixtureList.Count(), Is.EqualTo(2));
        }

        [Test]
        public void AreAllFixturesForTeamCurrentWeekReturned()
        {
            var db = new Mock<IFootballStatsEntities>();
            db.Setup(x => x.Fixtures).Returns(ConfigureEntities());
            var fs = new FixturesService(new DateTime(2013, 1, 1), db.Object);

            var fixtureList = fs.FixturesForTeamCurrentWeek("Man Utd");

            Assert.That(fixtureList.Count(), Is.EqualTo(1));
        }

        private IDbSet<Fixture> ConfigureEntities()
        {
            var mockEntities = new Mock<IDbSet<Fixture>>();
            mockEntities.Setup(x => x.GetEnumerator()).Returns(FixtureList());
            
            return mockEntities.Object;
        }

        public IEnumerator<Fixture> FixtureList()
        {
            yield return new Fixture { AwayTeam = "Man Utd", Date = new DateTime(2013, 1, 1), Div = "E0", HomeTeam = "Arsenal" };
            yield return new Fixture { AwayTeam = "Man Utd", Date = new DateTime(2013, 1, 13), Div = "E0", HomeTeam = "Tottenham" };
            yield return new Fixture { AwayTeam = "Arsenal", Date = new DateTime(2013, 1, 23), Div = "E0", HomeTeam = "Man Utd" };
            yield return new Fixture { AwayTeam = "Aston Villa", Date = new DateTime(2013, 1, 1), Div = "E0", HomeTeam = "Stoke City" };
            yield return new Fixture { AwayTeam = "Man Utd", Date = new DateTime(2013, 6, 1), Div = "E0", HomeTeam = "Man City" };
            yield return new Fixture { AwayTeam = "Man City", Date = new DateTime(2013, 7, 1), Div = "E0", HomeTeam = "Arsenal" };
            yield return new Fixture { AwayTeam = "Man Utd", Date = new DateTime(2013, 7, 1), Div = "E0", HomeTeam = "Chelsea" };
        }
    }
}
