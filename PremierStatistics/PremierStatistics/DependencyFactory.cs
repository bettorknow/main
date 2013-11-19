using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using PremierStatisticsDal;

namespace PremierStatistics
{
    public class DependencyFactory
    {

        public static IUnityContainer Container { get; private set; }

        static DependencyFactory()
        {
            var container = new UnityContainer();

            container.RegisterType<IFootballStatsEntities, FootballStatsEntities>()
                     .RegisterType<IFixturesService, FixturesService>();

            Container = container;
        }

        public static T Resolve<T>()
        {
            T ret = default(T);

            if (Container.IsRegistered(typeof(T)))
            {
                ret = Container.Resolve<T>();
            }

            return ret;
        }
    }
}