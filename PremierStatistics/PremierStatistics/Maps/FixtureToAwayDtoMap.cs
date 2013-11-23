using Omu.ValueInjecter;

namespace PremierStatistics.Maps
{
    public class FixtureToAwayDtoMap : ConventionInjection
    {
        protected override bool Match(ConventionInfo c)
        {
            return c.TargetProp.Name == "Name" && c.SourceProp.Name == "AwayTeam";
        }
    }
}