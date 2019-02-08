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
    public class VolunteeringEventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VolunteeringEvents
        public ActionResult Index()
        {
            return View(db.VolunteeringEvents.ToList());
        }

        // GET: VolunteeringEvents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VolunteeringEvents volunteeringEvents = db.VolunteeringEvents.Find(id);
            if (volunteeringEvents == null)
            {
                return HttpNotFound();
            }
            return View(volunteeringEvents);
        }

        // GET: VolunteeringEvents/Create
        public ActionResult Create()
        {
            if (System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).AccountId == 2)
            {
                return View();
            }
            return RedirectToAction("index");
        }

        // POST: VolunteeringEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //public ActionResult Create([Bind(Include = "VolunteeringEventsId,Date,Title,Organization,Description")] VolunteeringEvents volunteeringEvents, Models.RegisterViewModel model, HttpPostedFileBase upload)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VolunteeringEvents volunteeringEvents, HttpPostedFileBase upload)
        {
            if (System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).AccountId == 2)
            {
                if (ModelState.IsValid)
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        var avatar = new VolunteerFile
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Volunteer,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        volunteeringEvents.VolunteerFiles = new List<VolunteerFile> { avatar };
                        db.VolunteeringEvents.Add(volunteeringEvents);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                return View(volunteeringEvents);
            }
            return RedirectToAction("index");
        }

        // GET: VolunteeringEvents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).AccountId == 2)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                VolunteeringEvents volunteeringEvents = db.VolunteeringEvents.Find(id);
                if (volunteeringEvents == null)
                {
                    return HttpNotFound();
                }
                return View(volunteeringEvents);
            }
            return RedirectToAction("index");
        }

        // POST: VolunteeringEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VolunteeringEventsId,Date,Title,Organization,Description")] VolunteeringEvents volunteeringEvents)
        {
            if (System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).AccountId == 2)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(volunteeringEvents).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(volunteeringEvents);
            }
            return RedirectToAction("index");
        }

        // GET: VolunteeringEvents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).AccountId == 2)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                VolunteeringEvents volunteeringEvents = db.VolunteeringEvents.Find(id);
                if (volunteeringEvents == null)
                {
                    return HttpNotFound();
                }
                return View(volunteeringEvents);
            }
            return RedirectToAction("index");
        }

        // POST: VolunteeringEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).AccountId == 2)
            {
                VolunteeringEvents volunteeringEvents = db.VolunteeringEvents.Find(id);
                db.VolunteeringEvents.Remove(volunteeringEvents);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("index");
        }
        // GET: VolunteeringEvents/Like/5
        public ActionResult Like(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VolunteeringEvents volunteeringEvents = db.VolunteeringEvents.Find(id);
            if (volunteeringEvents == null)
            {
                return HttpNotFound();
            }
            return Liked(volunteeringEvents.VolunteeringEventsId);
        }
        // POST: VolunteeringEvents/Liked/5
        public ActionResult Liked(int id)
        { 
            try
            {
                if (System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).AccountId == 2)
                {
                }
            }
            catch
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                string volDetails = "", s = "", Userid = "";
                int like = 0;
                List<VolunteerDetails> f = db.volunteerDetails.ToList();
                foreach (VolunteerDetails f2 in f)
                {
                    if (f2 != null)
                    {
                        if (f2.VolunteeringEvents.VolunteeringEventsId == id)
                        {
                            volDetails = f2.ApplicationUsersAndLikes;
                            Userid = System.Web.HttpContext.Current.User.Identity.GetUserId();
                            string[] all1 = volDetails.Split(':');
                            List<string> all2 = new List<string>();
                            foreach (string q in all1)
                            {
                                all2.Add(q);
                            }
                            List<string> all3 = new List<string>();
                            bool found = false;
                            foreach (string str in all2)
                            {
                                if (str.Contains(Userid))
                                {
                                    found = true;
                                    string[] prev2 = str.Split(',');
                                    like = Convert.ToInt32(prev2[1].Substring(0, prev2[1].Length - 1));
                                    if (like == 0) like = 1;
                                    else like = 0;
                                    all3.Add($"[{Userid},{like}]");
                                    continue;
                                }
                                all3.Add(str);
                            }
                            if (found == false)
                            {
                                like = 1;
                                all3.Add($"[{Userid},{like}]");
                            }
                            foreach (string j in all3)
                            {
                                if (all3.IndexOf(j) == all3.Count - 1) s += j;
                                else s += j + ":";
                            }
                            db.volunteerDetails.Remove(f2);
                            db.volunteerDetails.Add(new VolunteerDetails
                            {
                                VolunteerDetailsId = Convert.ToInt32(id),
                                VolunteeringEvents = db.VolunteeringEvents.Find(id),
                                ApplicationUsersAndLikes = s
                            });
                            if (like == 0) { db.VolunteeringEvents.Find(id).Likes--; }
                            else { db.VolunteeringEvents.Find(id).Likes++; }
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }

                    }
                }
                Userid = System.Web.HttpContext.Current.User.Identity.GetUserId();
                db.VolunteeringEvents.Find(Convert.ToInt32(id)).Likes += 1;
                s = $"[{Userid}, {1}]";
                db.volunteerDetails.Add(new VolunteerDetails
                {
                    VolunteerDetailsId = Convert.ToInt32(id),
                    VolunteeringEvents = db.VolunteeringEvents.Find(id),
                    ApplicationUsersAndLikes = s
                });
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
    }
}
