using SchedulerMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace SchedulerMVC.Controllers
{
    public class PlansController : Controller
    {
        private PlanContext db = new PlanContext();

        public ActionResult AllNote(string searchNote)
        {
            var plans = from m in db.Plans
                select m;

            if (!String.IsNullOrEmpty(searchNote))
            {
                plans = plans.Where(s => s.Note.Contains(searchNote));
            }

            return View(plans);
        }

        public ActionResult OverdueIndex()
        {
            return View(db.Plans.Where(t => t.Date < DateTime.Today).ToList());
        }

        
        public ActionResult TomorrowIndex()
        {
            var tommorrowDate = DateTime.Today.AddDays(1);

            return View(db.Plans.Where(t => t.Date == tommorrowDate).ToList());
        }

        
        public ActionResult TodayIndex()
        {
            return View(db.Plans.Where(t => t.Date == DateTime.Today).ToList());
        }

        
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Plan plan = db.Plans.Find(id);
        //    if (plan == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(plan);
        //}

        
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        
        public ActionResult Create([Bind(Include = "Id,Note,Date")] Plan plan)
        {
            if (ModelState.IsValid)
            {
                db.Plans.Add(plan);
                db.SaveChanges();
                return RedirectToAction("AllNote");
            }

            return View(plan);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan plan = db.Plans.Find(id);
            if (plan == null)
            {
                return HttpNotFound();
            }
            return View(plan);
        }

        
        [HttpPost]
        
        public ActionResult Edit([Bind(Include = "Id,Note,Date")] Plan plan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AllNote");
            }
            return View(plan);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan plan = db.Plans.Find(id);
            if (plan == null)
            {
                return HttpNotFound();
            }
            return View(plan);
        }


        [HttpPost, ActionName("Delete")]

        public ActionResult Delete(int id)
        {
            Plan plan = db.Plans.Find(id);
            db.Plans.Remove(plan);
            db.SaveChanges();
            return RedirectToAction("AllNote");
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
