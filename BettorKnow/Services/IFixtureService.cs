using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BettorKnow.Models;

namespace BettorKnow.Services
{
    public interface IFixtureService
    {
        FixtureCountModel GetFixturesModel(string fixture, string page);
    }
}
