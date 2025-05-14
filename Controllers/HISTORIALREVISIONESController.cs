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
    public class HISTORIALREVISIONESController : Controller
    {
        private AutoCar360Entities2 db = new AutoCar360Entities2();

        // GET: HISTORIALREVISIONES
        public ActionResult Index()
        {
            var hISTORIALREVISIONES = db.HISTORIALREVISIONES.Include(h => h.REVISIONESVEHICULO);
            return View(hISTORIALREVISIONES.ToList());
        }

        // GET: HISTORIALREVISIONES/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HISTORIALREVISIONES hISTORIALREVISIONES = db.HISTORIALREVISIONES.Find(id);
            if (hISTORIALREVISIONES == null)
            {
                return HttpNotFound();
            }
            return View(hISTORIALREVISIONES);
        }

        // GET: HISTORIALREVISIONES/Create
        public ActionResult Create()
        {
            ViewBag.Id_RevisionesVehiculo = new SelectList(db.REVISIONESVEHICULO, "Id_RevisionesVehiculo", "Id_RevisionesVehiculo");
            return View();
        }

        // POST: HISTORIALREVISIONES/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_HistorialRevisiones,Id_RevisionesVehiculo,HistorialRevisiones_Fecha,HistorialRevisiones_Taller,HistorialRevisiones_Precio,HistorialRevisiones_km,HistorialRevisiones_Comentarios")] HISTORIALREVISIONES hISTORIALREVISIONES)
        {
            if (ModelState.IsValid)
            {
                db.HISTORIALREVISIONES.Add(hISTORIALREVISIONES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_RevisionesVehiculo = new SelectList(db.REVISIONESVEHICULO, "Id_RevisionesVehiculo", "Id_RevisionesVehiculo", hISTORIALREVISIONES.Id_RevisionesVehiculo);
            return View(hISTORIALREVISIONES);
        }

        // GET: HISTORIALREVISIONES/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HISTORIALREVISIONES hISTORIALREVISIONES = db.HISTORIALREVISIONES.Find(id);
            if (hISTORIALREVISIONES == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_RevisionesVehiculo = new SelectList(db.REVISIONESVEHICULO, "Id_RevisionesVehiculo", "Id_RevisionesVehiculo", hISTORIALREVISIONES.Id_RevisionesVehiculo);
            return View(hISTORIALREVISIONES);
        }

        // POST: HISTORIALREVISIONES/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_HistorialRevisiones,Id_RevisionesVehiculo,HistorialRevisiones_Fecha,HistorialRevisiones_Taller,HistorialRevisiones_Precio,HistorialRevisiones_km,HistorialRevisiones_Comentarios")] HISTORIALREVISIONES hISTORIALREVISIONES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hISTORIALREVISIONES).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_RevisionesVehiculo = new SelectList(db.REVISIONESVEHICULO, "Id_RevisionesVehiculo", "Id_RevisionesVehiculo", hISTORIALREVISIONES.Id_RevisionesVehiculo);
            return View(hISTORIALREVISIONES);
        }

        // GET: HISTORIALREVISIONES/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HISTORIALREVISIONES hISTORIALREVISIONES = db.HISTORIALREVISIONES.Find(id);
            if (hISTORIALREVISIONES == null)
            {
                return HttpNotFound();
            }
            return View(hISTORIALREVISIONES);
        }

        // POST: HISTORIALREVISIONES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HISTORIALREVISIONES hISTORIALREVISIONES = db.HISTORIALREVISIONES.Find(id);
            db.HISTORIALREVISIONES.Remove(hISTORIALREVISIONES);
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
