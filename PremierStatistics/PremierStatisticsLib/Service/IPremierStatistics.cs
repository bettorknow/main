using System.Collections.Generic;
using System.ServiceModel;
using PremierStatisticsLib.Dto;
using PremierStatisticsLib.Interfaces;

namespace PremierStatisticsLib.Service
{
    [ServiceContract]
    public interface IPremierStatistics
    {
        [OperationContract]
        List<FixtureDto> Fixtures();
        [OperationContract]
        List<FixtureDto> FixturesByDate(IDateRange dateRange);
        [OperationContract]
        List<FixtureDto> FixturesByTeam(ITeam team);
        [OperationContract]
        List<FixtureDto> FixturesByDateTeam(IDateRange dateRange, ITeam team);
        [OperationContract]
        List<FixtureDto> FixturesForTeamCurrentWeek(ITeam team);
        [OperationContract]
        List<FixtureDto> FixturesForCurrentWeek();
    }
}
