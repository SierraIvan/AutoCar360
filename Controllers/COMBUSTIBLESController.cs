using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AutoCar360.Controllers
{
    public class COMBUSTIBLESController : Controller
    {
        private AutoCar360Entities2 db = new AutoCar360Entities2();

        // GET: COMBUSTIBLEs
        public ActionResult Index()
        {
            return View(db.COMBUSTIBLE.ToList());
        }

        // GET: COMBUSTIBLEs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMBUSTIBLE cOMBUSTIBLE = db.COMBUSTIBLE.Find(id);
            if (cOMBUSTIBLE == null)
            {
                return HttpNotFound();
            }
            return View(cOMBUSTIBLE);
        }

        // GET: COMBUSTIBLEs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: COMBUSTIBLEs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Combustible,Combustible_Nombre")] COMBUSTIBLE cOMBUSTIBLE)
        {
            if (ModelState.IsValid)
            {
                db.COMBUSTIBLE.Add(cOMBUSTIBLE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cOMBUSTIBLE);
        }

        // GET: COMBUSTIBLEs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMBUSTIBLE cOMBUSTIBLE = db.COMBUSTIBLE.Find(id);
            if (cOMBUSTIBLE == null)
            {
                return HttpNotFound();
            }
            return View(cOMBUSTIBLE);
        }

        // POST: COMBUSTIBLEs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Combustible,Combustible_Nombre")] COMBUSTIBLE cOMBUSTIBLE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOMBUSTIBLE).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cOMBUSTIBLE);
        }

        // GET: COMBUSTIBLEs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMBUSTIBLE cOMBUSTIBLE = db.COMBUSTIBLE.Find(id);
            if (cOMBUSTIBLE == null)
            {
                return HttpNotFound();
            }
            return View(cOMBUSTIBLE);
        }

        // POST: COMBUSTIBLEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            COMBUSTIBLE cOMBUSTIBLE = db.COMBUSTIBLE.Find(id);
            db.COMBUSTIBLE.Remove(cOMBUSTIBLE);
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
    }
}
