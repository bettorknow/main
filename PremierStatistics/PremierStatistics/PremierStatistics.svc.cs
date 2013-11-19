using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Omu.ValueInjecter;
using PremierStatisticsDal;
using PremierStatisticsLib;
using PremierStatisticsLib.Dto;

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

        public List<FixtureDto> FixturesByDate(DateTime from, DateTime to)
        {
            return _fixturesService.FixturesByDate(from, to).Select(ConvertToDto).ToList();
        }

        public List<FixtureDto> FixturesByTeam(TeamDto team)
        {
            return _fixturesService.FixturesByTeam(team.Name).Select(ConvertToDto).ToList();
        }

        public List<FixtureDto> FixturesByDateTeam(DateTime from, DateTime to, TeamDto team)
        {
            return _fixturesService.FixturesByDateTeam(from, to, team.Name).Select(ConvertToDto).ToList(); 
        }

        public List<FixtureDto> FixturesForTeamCurrentWeek(TeamDto team)
        {
            return _fixturesService.FixturesForTeamCurrentWeek(team.Name).Select(ConvertToDto).ToList();
        }

        public List<FixtureDto> FixturesForCurrentWeek()
        {
            return _fixturesService.FixturesForCurrentWeek().Select(ConvertToDto).ToList();
        }

        private FixtureDto ConvertToDto(Fixture fixture)
        {
            var fixtureDto = new FixtureDto();
            fixtureDto.InjectFrom(fixture);
            return fixtureDto;
        }
    }
}
