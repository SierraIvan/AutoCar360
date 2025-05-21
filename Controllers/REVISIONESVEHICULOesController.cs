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
    public class REVISIONESVEHICULOesController : Controller
    {
        private AutoCar360Entities2 db = new AutoCar360Entities2();

        // GET: REVISIONESVEHICULOes
        public ActionResult Index()
        {
            var rEVISIONESVEHICULO = db.REVISIONESVEHICULO.Include(r => r.TIPOREVISIONES).Include(r => r.VEHICULOS);
            return View(rEVISIONESVEHICULO.ToList());
        }

        // GET: REVISIONESVEHICULOes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REVISIONESVEHICULO rEVISIONESVEHICULO = db.REVISIONESVEHICULO.Find(id);
            if (rEVISIONESVEHICULO == null)
            {
                return HttpNotFound();
            }
            return View(rEVISIONESVEHICULO);
        }

        // GET: REVISIONESVEHICULOes/Create
        public ActionResult Create()
        {
            ViewBag.Id_TipoRevision = new SelectList(db.TIPOREVISIONES, "Id_TipoRevision", "Revision_Nombre");
            ViewBag.Id_Vehiculo = new SelectList(db.VEHICULOS, "Id_Vehiculo", "Vehiculo_Matricula");
            return View();
        }

        // POST: REVISIONESVEHICULOes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_RevisionesVehiculo,Id_TipoRevision,Id_Vehiculo,RevisionesVehiculo_Intervalokm,RevisionesVehiculo_IntervaloTiempo")] REVISIONESVEHICULO rEVISIONESVEHICULO)
        {
            if (ModelState.IsValid)
            {
                db.REVISIONESVEHICULO.Add(rEVISIONESVEHICULO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_TipoRevision = new SelectList(db.TIPOREVISIONES, "Id_TipoRevision", "Revision_Nombre", rEVISIONESVEHICULO.Id_TipoRevision);
            ViewBag.Id_Vehiculo = new SelectList(db.VEHICULOS, "Id_Vehiculo", "Vehiculo_Matricula", rEVISIONESVEHICULO.Id_Vehiculo);
            return View(rEVISIONESVEHICULO);
        }

        // GET: REVISIONESVEHICULOes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REVISIONESVEHICULO rEVISIONESVEHICULO = db.REVISIONESVEHICULO.Find(id);
            if (rEVISIONESVEHICULO == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_TipoRevision = new SelectList(db.TIPOREVISIONES, "Id_TipoRevision", "Revision_Nombre", rEVISIONESVEHICULO.Id_TipoRevision);
            ViewBag.Id_Vehiculo = new SelectList(db.VEHICULOS, "Id_Vehiculo", "Vehiculo_Matricula", rEVISIONESVEHICULO.Id_Vehiculo);
            return View(rEVISIONESVEHICULO);
        }

        // POST: REVISIONESVEHICULOes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_RevisionesVehiculo,Id_TipoRevision,Id_Vehiculo,RevisionesVehiculo_Intervalokm,RevisionesVehiculo_IntervaloTiempo")] REVISIONESVEHICULO rEVISIONESVEHICULO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rEVISIONESVEHICULO).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_TipoRevision = new SelectList(db.TIPOREVISIONES, "Id_TipoRevision", "Revision_Nombre", rEVISIONESVEHICULO.Id_TipoRevision);
            ViewBag.Id_Vehiculo = new SelectList(db.VEHICULOS, "Id_Vehiculo", "Vehiculo_Matricula", rEVISIONESVEHICULO.Id_Vehiculo);
            return View(rEVISIONESVEHICULO);
        }

        // GET: REVISIONESVEHICULOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REVISIONESVEHICULO rEVISIONESVEHICULO = db.REVISIONESVEHICULO.Find(id);
            if (rEVISIONESVEHICULO == null)
            {
                return HttpNotFound();
            }
            return View(rEVISIONESVEHICULO);
        }

        // POST: REVISIONESVEHICULOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            REVISIONESVEHICULO rEVISIONESVEHICULO = db.REVISIONESVEHICULO.Find(id);
            db.REVISIONESVEHICULO.Remove(rEVISIONESVEHICULO);
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
