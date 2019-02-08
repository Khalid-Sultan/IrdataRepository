using Irdata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Irdata.Controllers
{
    public class VolunteerFileController : Controller
    {
        private ApplicationDbContext applicationContext = new ApplicationDbContext(); 
        // GET: /VolunteerFile/
        public ActionResult Index(int id)
        {
            try
            {
                var fileToRetrieve = applicationContext.VolunteeringFiles.Find(id);
                return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
            }
            catch
            {
            }
            return null;
        }
    }
}