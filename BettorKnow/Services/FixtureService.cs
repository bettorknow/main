using System;
using System.Linq;
using BettorKnow.Models;
using BettorKnow.PremierStatisticsService;

namespace BettorKnow.Services
{
    public class FixtureService : IFixtureService
    {
        private readonly IPremierStatistics _premierStatistics;

        public FixtureService(IPremierStatistics premierStatistics)
        {
            _premierStatistics = premierStatistics;
        }

        public FixtureCountModel GetFixturesModel(string fixture, string page)
        {
            var model = new FixtureCountModel { TotalFixtures = 0, Error = "" };
            try
            {
                var stats = _premierStatistics.Fixtures();
                model.TotalFixtures = stats.Count();
            }
            catch (Exception e)
            {
                model.Error = e.Message;
            }
            return model;
        }
    }
}