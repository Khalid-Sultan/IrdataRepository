using Irdata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Irdata.Controllers
{
    public class FileController:Controller
    {
        private ApplicationDbContext applicationContext = new ApplicationDbContext();
        //
        // GET: /File/
        public ActionResult Index(int id)
        {
            try
            {
                var fileToRetrieve = applicationContext.Files.Find(id);
                return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
            }
            catch
            {
            }
            return null;
        }
    }
}