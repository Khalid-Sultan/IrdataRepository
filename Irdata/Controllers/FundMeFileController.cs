using Irdata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Irdata.Controllers
{
    public class FundMeFileController : Controller
    {
        private ApplicationDbContext applicationContext = new ApplicationDbContext();
        //
        // GET: /FundMeFile/
        public ActionResult Index(int id)
        {
            try
            {
                var fileToRetrieve = applicationContext.FundMeFiles.Find(id);
                return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
            }
            catch
            {
            }
            return null;
        } 
    }
}