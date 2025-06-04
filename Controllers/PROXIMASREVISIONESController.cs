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
    public class PROXIMASREVISIONESController : Controller
    {
        private AutoCar360Entities2 db = new AutoCar360Entities2();

        // GET: PROXIMASREVISIONES
        public ActionResult Index()
        {

            if (Session["UsuarioId"] == null)
            {
                return RedirectToAction("Index", "USUARIOS");
            }

            int usuarioId = (int)Session["UsuarioId"];

            var pROXIMASREVISIONES = db.PROXIMASREVISIONES.Include(p => p.REVISIONESVEHICULO)
                .Include(p => p.REVISIONESVEHICULO.VEHICULOS)
                .Include(p => p.REVISIONESVEHICULO.TIPOREVISIONES)
                .Where(p => p.REVISIONESVEHICULO.VEHICULOS.Id_Usuario == usuarioId)
                .ToList();
            return View(pROXIMASREVISIONES);
        }

        // GET: PROXIMASREVISIONES/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROXIMASREVISIONES pROXIMASREVISIONES = db.PROXIMASREVISIONES.Find(id);
            if (pROXIMASREVISIONES == null)
            {
                return HttpNotFound();
            }
            return View(pROXIMASREVISIONES);
        }

        // GET: PROXIMASREVISIONES/Create
        public ActionResult Create()
        {
            ViewBag.Id_RevisionesVehiculo = new SelectList(db.REVISIONESVEHICULO, "Id_RevisionesVehiculo", "Id_RevisionesVehiculo");
            return View();
        }

        // POST: PROXIMASREVISIONES/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_ProximaRevision,Id_RevisionesVehiculo,ProximaRevision_intervaloKm,ProximaRevision_intervaloTiempo")] PROXIMASREVISIONES pROXIMASREVISIONES)
        {
            if (ModelState.IsValid)
            {
                db.PROXIMASREVISIONES.Add(pROXIMASREVISIONES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_RevisionesVehiculo = new SelectList(db.REVISIONESVEHICULO, "Id_RevisionesVehiculo", "Id_RevisionesVehiculo", pROXIMASREVISIONES.Id_RevisionesVehiculo);
            return View(pROXIMASREVISIONES);
        }

        // GET: PROXIMASREVISIONES/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROXIMASREVISIONES pROXIMASREVISIONES = db.PROXIMASREVISIONES.Find(id);
            if (pROXIMASREVISIONES == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_RevisionesVehiculo = new SelectList(db.REVISIONESVEHICULO, "Id_RevisionesVehiculo", "Id_RevisionesVehiculo", pROXIMASREVISIONES.Id_RevisionesVehiculo);
            return View(pROXIMASREVISIONES);
        }

        // POST: PROXIMASREVISIONES/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_ProximaRevision,Id_RevisionesVehiculo,ProximaRevision_intervaloKm,ProximaRevision_intervaloTiempo")] PROXIMASREVISIONES pROXIMASREVISIONES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pROXIMASREVISIONES).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_RevisionesVehiculo = new SelectList(db.REVISIONESVEHICULO, "Id_RevisionesVehiculo", "Id_RevisionesVehiculo", pROXIMASREVISIONES.Id_RevisionesVehiculo);
            return View(pROXIMASREVISIONES);
        }

        // GET: PROXIMASREVISIONES/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROXIMASREVISIONES pROXIMASREVISIONES = db.PROXIMASREVISIONES.Find(id);
            if (pROXIMASREVISIONES == null)
            {
                return HttpNotFound();
            }
            return View(pROXIMASREVISIONES);
        }

        // POST: PROXIMASREVISIONES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PROXIMASREVISIONES pROXIMASREVISIONES = db.PROXIMASREVISIONES.Find(id);
            db.PROXIMASREVISIONES.Remove(pROXIMASREVISIONES);
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
