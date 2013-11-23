using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Omu.ValueInjecter;
using PremierStatistics.Maps;
using PremierStatisticsDal;
using PremierStatisticsLib.Dto;
using PremierStatisticsLib.Interfaces;
using PremierStatisticsLib.Service;

namespace PremierStatistics
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class PremierStatistics : IPremierStatistics
    {
        private readonly IFixturesService _fixturesService;

        public PremierStatistics(IFixturesService premierStatistics)
        {
            _fixturesService = premierStatistics;
        }

        public PremierStatistics() 
            : this(DependencyFactory.Resolve<IFixturesService>())
        {
        }

        public List<FixtureDto> Fixtures()
        {
            return _fixturesService.Fixtures().Select(ConvertToDto).ToList();
        }

        public List<FixtureDto> FixturesByDate(IDateRange dateRange)
        {
            return _fixturesService.FixturesByDate(dateRange).Select(ConvertToDto).ToList();
        }

        public List<FixtureDto> FixturesByTeam(ITeam team)
        {
            return _fixturesService.FixturesByTeam(team).Select(ConvertToDto).ToList();
        }

        public List<FixtureDto> FixturesByDateTeam(IDateRange dateRange, ITeam team)
        {
            return _fixturesService.FixturesByDateTeam(dateRange, team).Select(ConvertToDto).ToList(); 
        }

        public List<FixtureDto> FixturesForTeamCurrentWeek(ITeam team)
        {
            return _fixturesService.FixturesForTeamCurrentWeek(team).Select(ConvertToDto).ToList();
        }

        public List<FixtureDto> FixturesForCurrentWeek()
        {
            return _fixturesService.FixturesForCurrentWeek().Select(ConvertToDto).ToList();
        }

        private FixtureDto ConvertToDto(Fixture fixture)
        {
            var fixtureDto = new FixtureDto(fixture.HomeTeam, fixture.AwayTeam);
            fixtureDto.InjectFrom(fixture);
            fixtureDto.Home.InjectFrom<FixtureToHomeDtoMap>(fixture);
            fixtureDto.Away.InjectFrom<FixtureToAwayDtoMap>(fixture);
            return fixtureDto;
        }
    }
}
