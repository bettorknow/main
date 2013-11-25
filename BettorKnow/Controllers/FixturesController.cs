﻿using System;
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
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) => true;
            var model = new FixtureCountModel { TotalFixtures = 0, Error =  "" };
            try
            {
                PremierStatisticsClient service = new PremierStatisticsClient();
                var stats = service.Fixtures();
                model.TotalFixtures = stats.Count();
            }
            catch (Exception e)
            {
                model.Error = e.Message;
            }
            return View(model);
        }

    }
}
