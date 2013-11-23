using Omu.ValueInjecter;

namespace PremierStatistics.Maps
{
    public class FixtureToHomeDtoMap : ConventionInjection
    {
        protected override bool Match(ConventionInfo c)
        {
            return c.TargetProp.Name == "Name" && c.SourceProp.Name == "HomeTeam";
        }
    }
}