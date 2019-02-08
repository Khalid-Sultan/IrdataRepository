using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Irdata.Models;
using Newtonsoft.Json;

namespace Irdata.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            int userCount = 0;
            userCount = applicationDbContext.Users.Count();
            int success = 0, failure = 0, ongoing = 0;
            List<FundMe> fundMes = applicationDbContext.FundMes.ToList();
            int total = fundMes.Count;
            foreach (FundMe f in fundMes)
            {
                if (f.status == 0)
                {
                    ongoing++;
                }
                else if (f.status == 3)
                {
                    success++;
                }
                else
                {
                    failure++;
                }
            }
            int volunteerEvents = 0;
            volunteerEvents = applicationDbContext.VolunteeringEvents.Count();
            List<DataPoint> dataPoints = new List<DataPoint>{
                //new DataPoint(userCount, "User Count", true),
                new DataPoint(success*100/total, "Successful Endeavors", false),
                new DataPoint(ongoing*100/total, "Ongoing Endeavors", false),
                new DataPoint(failure*100/total, "Failed Endeavors", false),
                //new DataPoint(volunteerEvents, "Volunteeer Events", false),
            };
            ViewBag.Users = userCount;
            ViewBag.Events = volunteerEvents;
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();
        }
    }
}