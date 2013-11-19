using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PremierStatisticsDal.Tests
{
    public class EveryDayOfYear
    {
        public static IEnumerable TestCases
        {
            get
            {
                DateTime dt = new DateTime(2013,1,1);
                for (int i = 0; i < 365; i++)
                {
                    yield return new TestCaseData(dt.AddDays(i));
                }
            }
        }  

    }
}
