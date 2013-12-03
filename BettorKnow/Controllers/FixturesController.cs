using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using BettorKnow.Models;
using BettorKnow.PremierStatisticsService;
using BettorKnow.Services;

namespace BettorKnow.Controllers
{
    public class FixturesController : Controller
    {
        private readonly IFixtureService _fixtureService;

        public FixturesController(IFixtureService fixtureService)
        {
            _fixtureService = fixtureService;
        }

        public ActionResult Index()
        {
            var model = _fixtureService.GetFixturesModel("All", "Fixtures");
            return View(model);
        }

    }
}
