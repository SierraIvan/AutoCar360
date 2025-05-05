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
    public class PROXIMOMANTENIMIENTOController : Controller
    {
        private AutoCar360Entities2 db = new AutoCar360Entities2();

        // GET: PROXIMOMANTENIMIENTO
        public ActionResult Index()
        {
            if (Session["UsuarioId"] == null)
            {
                return RedirectToAction("Index", "USUARIOS");
            }

            int usuarioId = (int)Session["UsuarioId"];

            var proximosMantenimientos = db.PROXIMOMANTENIMIENTO
                .Include(p => p.MANTENIMIENTOVEHICULO)
                .Include(p => p.MANTENIMIENTOVEHICULO.VEHICULOS)
                .Include(p => p.MANTENIMIENTOVEHICULO.TIPOMANTENIMIENTO)
                .Where(p => p.MANTENIMIENTOVEHICULO.VEHICULOS.Id_Usuario == usuarioId)
                .ToList();

            return View(proximosMantenimientos);
        }

        // GET: PROXIMOMANTENIMIENTO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROXIMOMANTENIMIENTO pROXIMOMANTENIMIENTO = db.PROXIMOMANTENIMIENTO.Find(id);
            if (pROXIMOMANTENIMIENTO == null)
            {
                return HttpNotFound();
            }
            return View(pROXIMOMANTENIMIENTO);
        }

        // GET: PROXIMOMANTENIMIENTO/Create
        public ActionResult Create()
        {
            ViewBag.Id_MantenimientoVehiculo = new SelectList(db.MANTENIMIENTOVEHICULO, "Id_MantenimientoVehiculo", "Id_MantenimientoVehiculo");
            return View();
        }

        // POST: PROXIMOMANTENIMIENTO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_ProximoMantenimiento,Id_MantenimientoVehiculo,ProximoMantenimiento_Km")] PROXIMOMANTENIMIENTO pROXIMOMANTENIMIENTO)
        {
            if (ModelState.IsValid)
            {
                db.PROXIMOMANTENIMIENTO.Add(pROXIMOMANTENIMIENTO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_MantenimientoVehiculo = new SelectList(db.MANTENIMIENTOVEHICULO, "Id_MantenimientoVehiculo", "Id_MantenimientoVehiculo", pROXIMOMANTENIMIENTO.Id_MantenimientoVehiculo);
            return View(pROXIMOMANTENIMIENTO);
        }

        // GET: PROXIMOMANTENIMIENTO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROXIMOMANTENIMIENTO pROXIMOMANTENIMIENTO = db.PROXIMOMANTENIMIENTO.Find(id);
            if (pROXIMOMANTENIMIENTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_MantenimientoVehiculo = new SelectList(db.MANTENIMIENTOVEHICULO, "Id_MantenimientoVehiculo", "Id_MantenimientoVehiculo", pROXIMOMANTENIMIENTO.Id_MantenimientoVehiculo);
            return View(pROXIMOMANTENIMIENTO);
        }

        // POST: PROXIMOMANTENIMIENTO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_ProximoMantenimiento,Id_MantenimientoVehiculo,ProximoMantenimiento_Km")] PROXIMOMANTENIMIENTO pROXIMOMANTENIMIENTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pROXIMOMANTENIMIENTO).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_MantenimientoVehiculo = new SelectList(db.MANTENIMIENTOVEHICULO, "Id_MantenimientoVehiculo", "Id_MantenimientoVehiculo", pROXIMOMANTENIMIENTO.Id_MantenimientoVehiculo);
            return View(pROXIMOMANTENIMIENTO);
        }

        // GET: PROXIMOMANTENIMIENTO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROXIMOMANTENIMIENTO pROXIMOMANTENIMIENTO = db.PROXIMOMANTENIMIENTO.Find(id);
            if (pROXIMOMANTENIMIENTO == null)
            {
                return HttpNotFound();
            }
            return View(pROXIMOMANTENIMIENTO);
        }

        // POST: PROXIMOMANTENIMIENTO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PROXIMOMANTENIMIENTO pROXIMOMANTENIMIENTO = db.PROXIMOMANTENIMIENTO.Find(id);
            db.PROXIMOMANTENIMIENTO.Remove(pROXIMOMANTENIMIENTO);
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
