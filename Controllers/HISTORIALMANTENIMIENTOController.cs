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
    public class HISTORIALMANTENIMIENTOController : Controller
    {
        private AutoCar360Entities2 db = new AutoCar360Entities2();

        // GET: HISTORIALMANTENIMIENTO
        public ActionResult Index()
        {
            var hISTORIALMANTENIMIENTO = db.HISTORIALMANTENIMIENTO.Include(h => h.MANTENIMIENTOVEHICULO);
            return View(hISTORIALMANTENIMIENTO.ToList());
        }

        // GET: HISTORIALMANTENIMIENTO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HISTORIALMANTENIMIENTO hISTORIALMANTENIMIENTO = db.HISTORIALMANTENIMIENTO.Find(id);
            if (hISTORIALMANTENIMIENTO == null)
            {
                return HttpNotFound();
            }
            return View(hISTORIALMANTENIMIENTO);
        }

        // GET: HISTORIALMANTENIMIENTO/Create
        public ActionResult Create()
        {
            ViewBag.Id_MantenimientoVehiculo = new SelectList(db.MANTENIMIENTOVEHICULO, "Id_MantenimientoVehiculo", "Id_MantenimientoVehiculo");
            return View();
        }

        // POST: HISTORIALMANTENIMIENTO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_HistorialMantenimiento,Id_MantenimientoVehiculo,HistorialMantenimiento_Fecha,HistorialMantenimiento_Taller,HistorialMantenimiento_Precio,HistorialMantenimiento_Km,HistorialMantenimiento_Comentarios")] HISTORIALMANTENIMIENTO hISTORIALMANTENIMIENTO)
        {
            if (ModelState.IsValid)
            {
                db.HISTORIALMANTENIMIENTO.Add(hISTORIALMANTENIMIENTO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_MantenimientoVehiculo = new SelectList(db.MANTENIMIENTOVEHICULO, "Id_MantenimientoVehiculo", "Id_MantenimientoVehiculo", hISTORIALMANTENIMIENTO.Id_MantenimientoVehiculo);
            return View(hISTORIALMANTENIMIENTO);
        }

        // GET: HISTORIALMANTENIMIENTO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HISTORIALMANTENIMIENTO hISTORIALMANTENIMIENTO = db.HISTORIALMANTENIMIENTO.Find(id);
            if (hISTORIALMANTENIMIENTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_MantenimientoVehiculo = new SelectList(db.MANTENIMIENTOVEHICULO, "Id_MantenimientoVehiculo", "Id_MantenimientoVehiculo", hISTORIALMANTENIMIENTO.Id_MantenimientoVehiculo);
            return View(hISTORIALMANTENIMIENTO);
        }

        // POST: HISTORIALMANTENIMIENTO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_HistorialMantenimiento,Id_MantenimientoVehiculo,HistorialMantenimiento_Fecha,HistorialMantenimiento_Taller,HistorialMantenimiento_Precio,HistorialMantenimiento_Km,HistorialMantenimiento_Comentarios")] HISTORIALMANTENIMIENTO hISTORIALMANTENIMIENTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hISTORIALMANTENIMIENTO).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_MantenimientoVehiculo = new SelectList(db.MANTENIMIENTOVEHICULO, "Id_MantenimientoVehiculo", "Id_MantenimientoVehiculo", hISTORIALMANTENIMIENTO.Id_MantenimientoVehiculo);
            return View(hISTORIALMANTENIMIENTO);
        }

        // GET: HISTORIALMANTENIMIENTO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HISTORIALMANTENIMIENTO hISTORIALMANTENIMIENTO = db.HISTORIALMANTENIMIENTO.Find(id);
            if (hISTORIALMANTENIMIENTO == null)
            {
                return HttpNotFound();
            }
            return View(hISTORIALMANTENIMIENTO);
        }

        // POST: HISTORIALMANTENIMIENTO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HISTORIALMANTENIMIENTO hISTORIALMANTENIMIENTO = db.HISTORIALMANTENIMIENTO.Find(id);
            db.HISTORIALMANTENIMIENTO.Remove(hISTORIALMANTENIMIENTO);
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
