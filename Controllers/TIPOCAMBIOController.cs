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
    public class TIPOCAMBIOController : Controller
    {
        private AutoCar360Entities2 db = new AutoCar360Entities2();

        // GET: TIPOCAMBIOs
        public ActionResult Index()
        {
            return View(db.TIPOCAMBIO.ToList());
        }

        // GET: TIPOCAMBIOs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOCAMBIO tIPOCAMBIO = db.TIPOCAMBIO.Find(id);
            if (tIPOCAMBIO == null)
            {
                return HttpNotFound();
            }
            return View(tIPOCAMBIO);
        }

        // GET: TIPOCAMBIOs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TIPOCAMBIOs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_TipoCambio,TipoCambio_Nombre")] TIPOCAMBIO tIPOCAMBIO)
        {
            if (ModelState.IsValid)
            {
                db.TIPOCAMBIO.Add(tIPOCAMBIO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tIPOCAMBIO);
        }

        // GET: TIPOCAMBIOs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOCAMBIO tIPOCAMBIO = db.TIPOCAMBIO.Find(id);
            if (tIPOCAMBIO == null)
            {
                return HttpNotFound();
            }
            return View(tIPOCAMBIO);
        }

        // POST: TIPOCAMBIOs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_TipoCambio,TipoCambio_Nombre")] TIPOCAMBIO tIPOCAMBIO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIPOCAMBIO).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tIPOCAMBIO);
        }

        // GET: TIPOCAMBIOs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOCAMBIO tIPOCAMBIO = db.TIPOCAMBIO.Find(id);
            if (tIPOCAMBIO == null)
            {
                return HttpNotFound();
            }
            return View(tIPOCAMBIO);
        }

        // POST: TIPOCAMBIOs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TIPOCAMBIO tIPOCAMBIO = db.TIPOCAMBIO.Find(id);
            db.TIPOCAMBIO.Remove(tIPOCAMBIO);
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
