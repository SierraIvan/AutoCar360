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
    public class MANTENIMIENTOVEHICULOSController : Controller
    {
        private AutoCar360Entities2 db = new AutoCar360Entities2();

        // GET: MANTENIMIENTOVEHICULOes
        public ActionResult Index()
        {
            var mANTENIMIENTOVEHICULO = db.MANTENIMIENTOVEHICULO.Include(m => m.TIPOMANTENIMIENTO).Include(m => m.VEHICULOS);
            return View(mANTENIMIENTOVEHICULO.ToList());
        }

        // GET: MANTENIMIENTOVEHICULOes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MANTENIMIENTOVEHICULO mANTENIMIENTOVEHICULO = db.MANTENIMIENTOVEHICULO.Find(id);
            if (mANTENIMIENTOVEHICULO == null)
            {
                return HttpNotFound();
            }
            return View(mANTENIMIENTOVEHICULO);
        }

        // GET: MANTENIMIENTOVEHICULOes/Create
        public ActionResult Create()
        {
            int usuarioId = (int)Session["UsuarioId"];

            var vehiculosDelUsuario = db.VEHICULOS
                .Where(v => v.Id_Usuario == usuarioId)
                .Select(v => new { v.Id_Vehiculo, v.Vehiculo_Matricula })
                .ToList();

            ViewBag.Id_TipoMantenimiento = new SelectList(db.TIPOMANTENIMIENTO, "Id_TipoMantenimiento", "Mantenimiento_Nombre");
            ViewBag.Id_Vehiculo = new SelectList(vehiculosDelUsuario, "Id_Vehiculo", "Vehiculo_Matricula");

            return View();
        }

        // POST: MANTENIMIENTOVEHICULOes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_MantenimientoVehiculo,Id_Vehiculo,Id_TipoMantenimiento,MantenimientoVehiculo_IntervaloKm")] MANTENIMIENTOVEHICULO mANTENIMIENTOVEHICULO)
        {
            if (ModelState.IsValid)
            {
                db.MANTENIMIENTOVEHICULO.Add(mANTENIMIENTOVEHICULO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            int usuarioId = (int)Session["UsuarioId"];
            var vehiculosDelUsuario = db.VEHICULOS
                .Where(v => v.Id_Usuario == usuarioId)
                .Select(v => new { v.Id_Vehiculo, v.Vehiculo_Matricula })
                .ToList();

            ViewBag.Id_TipoMantenimiento = new SelectList(db.TIPOMANTENIMIENTO, "Id_TipoMantenimiento", "Mantenimiento_Nombre", mANTENIMIENTOVEHICULO.Id_TipoMantenimiento);
            ViewBag.Id_Vehiculo = new SelectList(vehiculosDelUsuario, "Id_Vehiculo", "Vehiculo_Matricula", mANTENIMIENTOVEHICULO.Id_Vehiculo);

            return View(mANTENIMIENTOVEHICULO);
        }


        // GET: MANTENIMIENTOVEHICULOes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MANTENIMIENTOVEHICULO mANTENIMIENTOVEHICULO = db.MANTENIMIENTOVEHICULO.Find(id);
            if (mANTENIMIENTOVEHICULO == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_TipoMantenimiento = new SelectList(db.TIPOMANTENIMIENTO, "Id_TipoMantenimiento", "Mantenimiento_Nombre", mANTENIMIENTOVEHICULO.Id_TipoMantenimiento);
            ViewBag.Id_Vehiculo = new SelectList(db.VEHICULOS, "Id_Vehiculo", "Vehiculo_Matricula", mANTENIMIENTOVEHICULO.Id_Vehiculo);
            return View(mANTENIMIENTOVEHICULO);
        }

        // POST: MANTENIMIENTOVEHICULOes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_MantenimientoVehiculo,Id_Vehiculo,Id_TipoMantenimiento,MantenimientoVehiculo_IntervaloKm")] MANTENIMIENTOVEHICULO mANTENIMIENTOVEHICULO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mANTENIMIENTOVEHICULO).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_TipoMantenimiento = new SelectList(db.TIPOMANTENIMIENTO, "Id_TipoMantenimiento", "Mantenimiento_Nombre", mANTENIMIENTOVEHICULO.Id_TipoMantenimiento);
            ViewBag.Id_Vehiculo = new SelectList(db.VEHICULOS, "Id_Vehiculo", "Vehiculo_Matricula", mANTENIMIENTOVEHICULO.Id_Vehiculo);
            return View(mANTENIMIENTOVEHICULO);
        }

        // GET: MANTENIMIENTOVEHICULOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MANTENIMIENTOVEHICULO mANTENIMIENTOVEHICULO = db.MANTENIMIENTOVEHICULO.Find(id);
            if (mANTENIMIENTOVEHICULO == null)
            {
                return HttpNotFound();
            }
            return View(mANTENIMIENTOVEHICULO);
        }

        // POST: MANTENIMIENTOVEHICULOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MANTENIMIENTOVEHICULO mANTENIMIENTOVEHICULO = db.MANTENIMIENTOVEHICULO.Find(id);
            db.MANTENIMIENTOVEHICULO.Remove(mANTENIMIENTOVEHICULO);
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
