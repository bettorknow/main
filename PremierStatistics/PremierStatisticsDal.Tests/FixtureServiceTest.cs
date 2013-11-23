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
using PremierStatisticsLib.Interfaces;

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
            var team = new Mock<ITeam>();
            team.Setup(x => x.Name).Returns("Man Utd");

            var fixtureList = fs.FixturesByTeam(team.Object);

            Assert.That(fixtureList.Count(), Is.EqualTo(5));
            team.VerifyGet(x => x.Name);
        }

        [Test]
        public void AreAllFixturesByDateTeamReturned()
        {
            var fs = GetConfiguredFixtureService();
            var range = new Mock<IDateRange>();
            var team = new Mock<ITeam>();

            team.Setup(x => x.Name).Returns("Man Utd");

            range.SetupGet(x => x.From).Returns(new DateTime(2013, 1, 1));
            range.SetupGet(x => x.To).Returns(new DateTime(2013, 2, 1));
            var fixtureList = fs.FixturesByDateTeam(range.Object, team.Object);

            Assert.That(fixtureList.Count(), Is.EqualTo(3));
            team.VerifyGet(x => x.Name);
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

            var team = new Mock<ITeam>();
            team.Setup(x => x.Name).Returns("Man Utd");

            var fixtureList = fs.FixturesForTeamCurrentWeek(team.Object);

            Assert.That(fixtureList.Count(), Is.EqualTo(1));
            team.VerifyAll();
        }

        [Test]
        public void DoFixturesReturnedHaveValidValues()
        {
            var db = new Mock<IFootballStatsEntities>();
            db.Setup(x => x.Fixtures).Returns(ConfigureEntities());
            var fs = new FixturesService(new DateTime(2013, 1, 1), db.Object);

            var team = new Mock<ITeam>();
            team.Setup(x => x.Name).Returns("Man Utd");

            var fixtureList = fs.FixturesForTeamCurrentWeek(team.Object);

            var firstItem = fixtureList.FirstOrDefault();

            Assert.NotNull(firstItem);

            Assert.That(firstItem.AwayTeam, Is.EqualTo("Man Utd"));
            Assert.That(firstItem.MatchDate, Is.EqualTo(new DateTime(2013, 1, 1)));
            Assert.That(firstItem.Div, Is.EqualTo("E0"));
            Assert.That(firstItem.HomeTeam, Is.EqualTo("Arsenal")); 

            team.VerifyGet(x=> x.Name);
        }

        private IDbSet<Fixture> ConfigureEntities()
        {
            var mockEntities = new Mock<IDbSet<Fixture>>();
            mockEntities.Setup(x => x.GetEnumerator()).Returns(FixtureList());

            return mockEntities.Object;
        }

        public IEnumerator<Fixture> FixtureList()
        {
            yield return new Fixture { AwayTeam = "Man Utd", MatchDate = new DateTime(2013, 1, 1), Div = "E0", HomeTeam = "Arsenal" };
            yield return new Fixture { AwayTeam = "Man Utd", MatchDate = new DateTime(2013, 1, 13), Div = "E0", HomeTeam = "Tottenham" };
            yield return new Fixture { AwayTeam = "Arsenal", MatchDate = new DateTime(2013, 1, 23), Div = "E0", HomeTeam = "Man Utd" };
            yield return new Fixture { AwayTeam = "Aston Villa", MatchDate = new DateTime(2013, 1, 1), Div = "E0", HomeTeam = "Stoke City" };
            yield return new Fixture { AwayTeam = "Man Utd", MatchDate = new DateTime(2013, 6, 1), Div = "E0", HomeTeam = "Man City" };
            yield return new Fixture { AwayTeam = "Man City", MatchDate = new DateTime(2013, 7, 1), Div = "E0", HomeTeam = "Arsenal" };
            yield return new Fixture { AwayTeam = "Man Utd", MatchDate = new DateTime(2013, 7, 1), Div = "E0", HomeTeam = "Chelsea" };
        }
    }
}
