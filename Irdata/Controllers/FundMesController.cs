using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Irdata.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Irdata.Controllers
{
    public class FundMesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public DateTime dateTime(string s)
        {
            //MDY
            string[] s2 = s.Split('/');
            int Month = Convert.ToInt32(s2[0]);
            int Day = Convert.ToInt32(s2[1]);
            int Year = Convert.ToInt32(s2[2]);
            DateTime date = new DateTime(Year, Month, Day);
            //return new DateTime(Convert.ToInt32(s2[2]), Convert.ToInt32(s2[0]), Convert.ToInt32(s2[1]));
            return date;
        }
        // GET: FundMes
        public ActionResult Index()
        {
            List<FundMe> fundMes = db.FundMes.ToList();
            foreach (FundMe fundMe in fundMes)
            {
                DateTime dateTime2 = dateTime(fundMe.date);
                dateTime2.AddDays(fundMe.duration * 7);
                DateTime Now = DateTime.Now;
                int yearDifference = dateTime2.Year - Now.Year;
                int monthDifference = dateTime2.Month - Now.Month;
                int dayDifference = dateTime2.Day - Now.Day;
                if (yearDifference < 0)
                {
                    if (fundMe.CurrentFunds < fundMe.TargetFunds)
                    {
                        if (fundMe.status != 3) db.FundMes.Find(fundMe.FundMeId).status = 1;
                    }
                    else
                    {
                        if (fundMe.status != 3) db.FundMes.Find(fundMe.FundMeId).status = 2;
                    }
                }
                else if(yearDifference==0)
                {
                    if (monthDifference < 0)
                    {
                        if (fundMe.CurrentFunds < fundMe.TargetFunds)
                        {
                            if (fundMe.status != 3) db.FundMes.Find(fundMe.FundMeId).status = 1;
                        }
                        else
                        {
                            if (fundMe.status != 3) db.FundMes.Find(fundMe.FundMeId).status = 2;
                        }
                    }
                    else if(monthDifference==0)
                    {
                        if (dayDifference < 0)
                        {
                            if (fundMe.CurrentFunds < fundMe.TargetFunds)
                            {
                                if (fundMe.status != 3) db.FundMes.Find(fundMe.FundMeId).status = 1;
                            }
                            else
                            {
                                if (fundMe.status != 3) db.FundMes.Find(fundMe.FundMeId).status = 2;
                            }
                        }
                        else
                        {
                            if (fundMe.CurrentFunds < fundMe.TargetFunds)
                            {
                                db.FundMes.Find(fundMe.FundMeId).status = 0;
                            }
                            else
                            {
                                db.FundMes.Find(fundMe.FundMeId).status = 3;
                            }
                        }
                    }
                    else
                    {
                        db.FundMes.Find(fundMe.FundMeId).status = 0;
                    }
                }
                else
                {
                    db.FundMes.Find(fundMe.FundMeId).status = 0;
                }
                //db.SaveChanges();
            }
            db.SaveChanges();
            return View(db.FundMes.ToList());
        }

        // GET: FundMes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FundMe fundMe = db.FundMes.Find(id);
            if (fundMe == null)
            {
                return HttpNotFound();
            }
            return View(fundMe);
        }

        // GET: FundMes/Create
        public ActionResult Create()
        {
            try
            {
                if (System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).AccountId == 1)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }

            }
            catch
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: FundMes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FundMe fundMe, HttpPostedFileBase upload)
        {

            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var avatar = new FundMeFile
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Fund,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    fundMe.FundMeFiles = new List<FundMeFile> { avatar };
                    fundMe.date = DateTime.Now.ToShortDateString();
                    fundMe.CurrentFunds = 0;
                    fundMe.status = 0;
                    //System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).FundMes.Add(fundMe);
                    db.FundMes.Add(fundMe);
                    ApplicationUser applicationUser = db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
                    applicationUser.FundMes.Add(fundMe);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(fundMe);
        }

        // GET: FundMes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FundMe fundMe = db.FundMes.Find(id);
            if (fundMe == null)
            {
                return HttpNotFound();
            }
            return View(fundMe);
        }

        // POST: FundMes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FundMeId,date,duration,Title,Description,TargetFunds,CurrentFunds")] FundMe fundMe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fundMe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fundMe);
        }

        // GET: FundMes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FundMe fundMe = db.FundMes.Find(id);
            if (fundMe == null)
            {
                return HttpNotFound();
            }
            return View(fundMe);
        }

        // POST: FundMes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FundMe fundMe = db.FundMes.Find(id);
            db.FundMes.Remove(fundMe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        // GET: FundMes/Pledge/5
        public ActionResult Pledge(int? id)
        {
            try
            {
                if (System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).AccountId == 1 || System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).AccountId == 2)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    FundMe fundMe = db.FundMes.Find(id);
                    if (fundMe == null)
                    {
                        return HttpNotFound();
                    }

                    List<Funder> f = db.funders.ToList();
                    foreach (Funder f2 in f)
                    {
                        if (f2.FundMe.FundMeId == id && f2 != null)
                        {
                            string funders = f2.ApplicationUsersAndPledge;
                            string Userid = System.Web.HttpContext.Current.User.Identity.GetUserId();
                            string[] all1 = funders.Split(':');
                            List<string> all2 = new List<string>();
                            foreach (string q in all1)
                            {
                                all2.Add(q);
                            }
                            string prevPledge = "";
                            foreach (string str in all2)
                            {
                                if (str.Contains(Userid))
                                {
                                    string[] prev2 = str.Split(',');
                                    prevPledge = prev2[1].Substring(0, prev2[1].Length - 1);
                                    ViewBag.mess = prevPledge;
                                    return View(fundMe);
                                }
                            }
                        }
                    }
                    return View(fundMe);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: FundMes/Donate/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pledge(int id, int pledgeInput)
        {
            if (ModelState.IsValid)
            {
                string funders = "", s = "", Userid = "", pledge = "";
                int difference = 0;
                List<Funder> f = db.funders.ToList();
                foreach (Funder f2 in f)
                {
                    if (f2.FundMe.FundMeId == id && f2 != null)
                    {
                        funders = f2.ApplicationUsersAndPledge;
                        Userid = System.Web.HttpContext.Current.User.Identity.GetUserId();
                        pledge = $"{pledgeInput}";
                        string[] all1 = funders.Split(':');
                        List<string> all2 = new List<string>();
                        foreach (string q in all1)
                        {
                            all2.Add(q);
                        }
                        int prevPledge = 0;
                        List<string> all3 = new List<string>();
                        bool found = false;
                        foreach (string str in all2)
                        {
                            if (str.Contains(Userid))
                            {
                                found = true;
                                string[] prev2 = str.Split(',');
                                prevPledge = Convert.ToInt32(prev2[1].Substring(0, prev2[1].Length - 1));
                                int currPledge = Convert.ToInt32(pledge);
                                difference = currPledge - prevPledge;
                                //if (currPledge > prevPledge)
                                //{
                                prevPledge += (currPledge - prevPledge);
                                //}
                                //else
                                //{
                                prevPledge -= (prevPledge - currPledge);
                                //}
                                all3.Add($"[{Userid},{prevPledge}]");
                                continue;
                            }
                            all3.Add(str);
                        }
                        if (found == false)
                        {
                            difference += Convert.ToInt32(pledge);
                            all3.Add($"[{Userid},{pledge}]");
                        }
                        foreach (string j in all3)
                        {
                            if (all3.IndexOf(j) == all3.Count - 1) s += j;
                            else s += j + ":";
                        }
                        db.funders.Remove(f2);
                        db.funders.Add(new Funder
                        {
                            FunderId = id,
                            FundMe = db.FundMes.Find(id),
                            ApplicationUsersAndPledge = s
                        });
                        db.FundMes.Find(id).CurrentFunds += difference;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                Userid = System.Web.HttpContext.Current.User.Identity.GetUserId();
                difference += Convert.ToInt32(pledgeInput);
                db.FundMes.Find(id).CurrentFunds += difference;
                pledge = $"{pledgeInput}";
                s = $"[{Userid}, {pledge}]";
                db.funders.Add(new Funder
                {
                    FunderId = id,
                    FundMe = db.FundMes.Find(id),
                    ApplicationUsersAndPledge = s
                });
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: FundMes/ViewPledgers/5
        public ActionResult ViewPledgers(int? id)
        {
            try
            {
                if (System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).AccountId == 1 || System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).AccountId == 2)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    List<Funder> funderList = db.funders.ToList();
                    List<Funder> newFunderList = new List<Funder>();
                    foreach (Funder funder in funderList)
                    {
                        if (funder.FundMe.FundMeId == id)
                        {
                            newFunderList.Add(funder);
                            break;
                        }
                    }
                    if (newFunderList == null)
                    {
                        return HttpNotFound();
                    }
                    return View(newFunderList);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
