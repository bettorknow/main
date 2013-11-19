using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PremierStatisticsLib.Dto;

namespace PremierStatisticsLib
{
    [ServiceContract]
    public interface IPremierStatistics
    {
        [OperationContract]
        List<FixtureDto> Fixtures();
        [OperationContract]
        List<FixtureDto> FixturesByDate(DateTime from, DateTime to);
        [OperationContract]
        List<FixtureDto> FixturesByTeam(TeamDto team);
        [OperationContract]
        List<FixtureDto> FixturesByDateTeam(DateTime from, DateTime to, TeamDto team);
        [OperationContract]
        List<FixtureDto> FixturesForTeamCurrentWeek(TeamDto team);
        [OperationContract]
        List<FixtureDto> FixturesForCurrentWeek();
    }
}
