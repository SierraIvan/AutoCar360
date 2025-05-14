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
    public class TIPOREVISIONESController : Controller
    {
        private AutoCar360Entities2 db = new AutoCar360Entities2();

        // GET: TIPOREVISIONES
        public ActionResult Index()
        {
            return View(db.TIPOREVISIONES.ToList());
        }

        // GET: TIPOREVISIONES/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOREVISIONES tIPOREVISIONES = db.TIPOREVISIONES.Find(id);
            if (tIPOREVISIONES == null)
            {
                return HttpNotFound();
            }
            return View(tIPOREVISIONES);
        }

        // GET: TIPOREVISIONES/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TIPOREVISIONES/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_TipoRevision,Revision_Nombre")] TIPOREVISIONES tIPOREVISIONES)
        {
            if (ModelState.IsValid)
            {
                db.TIPOREVISIONES.Add(tIPOREVISIONES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tIPOREVISIONES);
        }

        // GET: TIPOREVISIONES/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOREVISIONES tIPOREVISIONES = db.TIPOREVISIONES.Find(id);
            if (tIPOREVISIONES == null)
            {
                return HttpNotFound();
            }
            return View(tIPOREVISIONES);
        }

        // POST: TIPOREVISIONES/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_TipoRevision,Revision_Nombre")] TIPOREVISIONES tIPOREVISIONES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIPOREVISIONES).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tIPOREVISIONES);
        }

        // GET: TIPOREVISIONES/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOREVISIONES tIPOREVISIONES = db.TIPOREVISIONES.Find(id);
            if (tIPOREVISIONES == null)
            {
                return HttpNotFound();
            }
            return View(tIPOREVISIONES);
        }

        // POST: TIPOREVISIONES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TIPOREVISIONES tIPOREVISIONES = db.TIPOREVISIONES.Find(id);
            db.TIPOREVISIONES.Remove(tIPOREVISIONES);
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
