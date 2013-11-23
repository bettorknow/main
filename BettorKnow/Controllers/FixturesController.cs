using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BettorKnow.Models;
using BettorKnow.PremierStatisticsService;

namespace BettorKnow.Controllers
{
    public class FixturesController : Controller
    {
        //
        // GET: /Fixtures/

        public ActionResult Index()
        {
            PremierStatisticsClient service = new PremierStatisticsClient();
            var stats = service.Fixtures();
            var model = new FixtureCountModel() { TotalFixtures = stats.Count() };
            return View(model);
        }

    }
}
