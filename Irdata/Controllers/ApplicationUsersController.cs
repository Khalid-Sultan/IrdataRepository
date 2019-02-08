using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Irdata.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Logging;

namespace Irdata.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ApplicationUsers
        public ActionResult Index()
        {
            try
            {
                if (System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).AccountId == 2)
                {
                    var applicationUsers = db.Users.Include(a => a.Account);
                    return View(applicationUsers.ToList());
                }
            }
            catch
            {

            }
            return RedirectToAction("Index", "Home");
        }

        // GET: ApplicationUsers/Details/5
        public ActionResult Details(string id)
        {
            try
            {
                if (System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).AccountId == 2)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    ApplicationUser applicationUser = db.Users.Find(id);
                    if (applicationUser == null)
                    {
                        return HttpNotFound();
                    }
                    return View(applicationUser);
                }
            }
            catch
            {

            }
            return RedirectToAction("Index", "Home");
        }

        // GET: ApplicationUsers/Create
        public ActionResult Create()
        {
            try
            {
                if (System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).AccountId == 2)
                {
                    ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name");
                    return View();
                }
            }
            catch
            {

            }
            return RedirectToAction("Index", "Home");
        }

        // POST: ApplicationUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DisplayName,AccountId,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            try
            {
                if (System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).AccountId == 2)
                {
                    if (ModelState.IsValid)
                    {
                        db.Users.Add(applicationUser);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", applicationUser.AccountId);
                    return View(applicationUser);
                }
            }
            catch
            {

            }
            return RedirectToAction("Index", "Home");
        }

        // GET: ApplicationUsers/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {                
                //if (System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).AccountId == 2)
                //{
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    ApplicationUser applicationUser = db.Users.Include(s => s.Files).SingleOrDefault(s=>s.Id == id);
                    if (applicationUser == null)
                    {
                        return HttpNotFound();
                    } 
                    return View(applicationUser);
                //}
            }
            catch
            {

            }
            return RedirectToAction("Index", "Home");
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, HttpPostedFileBase upload) {
            //public ActionResult Edit([Bind(Include = "Id,DisplayName,Email,PhoneNumber,UserName")] ApplicationUser applicationUser)
            if (id == null)
            {
                return HttpNotFound();
            }
            var userToUpdate = db.Users.Find(id);
            if (TryUpdateModel(userToUpdate, "", new string[] { "DisplayName", "Email", "PhoneNumber", "UserName" }))
            {
                try
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        if (userToUpdate.Files.Any(f => f.FileType == FileType.Avatar))
                        {
                            db.Files.Remove(userToUpdate.Files.First(f => f.FileType == FileType.Avatar));
                        }
                        var avatar = new File
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Avatar,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        userToUpdate.Files = new List<File> { avatar };
                    }
                db.Entry(userToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (RetryLimitExceededException /*dex*/)
            {
                ModelState.AddModelError("", "Unable to save changes");
            }
        }
        return View(userToUpdate);
            //if (ModelState.IsValid)
            //{

            //    if (System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).AccountId == 1)
            //    {
            //        applicationUser.AccountId = 1;
            //    }
            //    else
            //    {
            //        applicationUser.AccountId = 2;
            //    }
            //    db.Entry(applicationUser).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(applicationUser);
        } 

        // GET: ApplicationUsers/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                if (System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).AccountId == 2)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    ApplicationUser applicationUser = db.Users.Find(id);
                    if (applicationUser == null)
                    {
                        return HttpNotFound();
                    }
                    return View(applicationUser);
                }
            }
            catch
            {

            }
            return RedirectToAction("Index", "Home");
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                if (System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).AccountId == 2)
                {
                    //db.Users.Remove(System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(id));                    
                    ApplicationUser applicationUser = db.Users.Find(id);
                    if (applicationUser != null)
                    {
                        db.Users.Remove(applicationUser);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    //return RedirectToAction(id);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException  dex )
            {
               Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!\n\n\n\n\n"+ dex.Message.ToString());
            }
            //Log the error (uncomment dex variable name and add a line here to write a log.
            return RedirectToAction("Delete", new { id = id, saveChangesError = true });
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
