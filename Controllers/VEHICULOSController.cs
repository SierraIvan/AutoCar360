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
    public class VEHICULOSController : Controller
    {
        private AutoCar360Entities2 db = new AutoCar360Entities2();

        // GET: VEHICULOS
        public ActionResult Index()
        {
            var vEHICULOS = db.VEHICULOS.Include(v => v.COLORES).Include(v => v.MODELOS).Include(v => v.USUARIOS);
            return View(vEHICULOS.ToList());
        }

        // GET: VEHICULOS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VEHICULOS vEHICULOS = db.VEHICULOS.Find(id);
            if (vEHICULOS == null)
            {
                return HttpNotFound();
            }
            return View(vEHICULOS);
        }

        // GET: VEHICULOS/Create
        public ActionResult Create()
        {
            ViewBag.Id_Color = new SelectList(db.COLORES, "Id_Color", "Color_Nombre");
            ViewBag.Id_Modelo = new SelectList(db.MODELOS, "Id_Modelo", "Modelo_Nombre");
            ViewBag.Id_Modelo = new SelectList(db.USUARIOS, "Id_Usuario", "Usuario_Nombre");
            return View();
        }

        // POST: VEHICULOS/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Vehiculo,Id_Usuario,Id_Modelo,Id_Color,Vehiculo_Matricula,Vehiculo_Fecha_Matriculacion,Vehiculo_Bastidor,Vehiculo_KmActuales")] VEHICULOS vEHICULOS)
        {
            if (ModelState.IsValid)
            {
                db.VEHICULOS.Add(vEHICULOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Color = new SelectList(db.COLORES, "Id_Color", "Color_Nombre", vEHICULOS.Id_Color);
            ViewBag.Id_Modelo = new SelectList(db.MODELOS, "Id_Modelo", "Modelo_Nombre", vEHICULOS.Id_Modelo);
            ViewBag.Id_Modelo = new SelectList(db.USUARIOS, "Id_Usuario", "Usuario_Nombre", vEHICULOS.Id_Modelo);
            return View(vEHICULOS);
        }

        // GET: VEHICULOS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VEHICULOS vEHICULOS = db.VEHICULOS.Find(id);
            if (vEHICULOS == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Color = new SelectList(db.COLORES, "Id_Color", "Color_Nombre", vEHICULOS.Id_Color);
            ViewBag.Id_Modelo = new SelectList(db.MODELOS, "Id_Modelo", "Modelo_Nombre", vEHICULOS.Id_Modelo);
            ViewBag.Id_Modelo = new SelectList(db.USUARIOS, "Id_Usuario", "Usuario_Nombre", vEHICULOS.Id_Modelo);
            return View(vEHICULOS);
        }

        // POST: VEHICULOS/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Vehiculo,Id_Usuario,Id_Modelo,Id_Color,Vehiculo_Matricula,Vehiculo_Fecha_Matriculacion,Vehiculo_Bastidor,Vehiculo_KmActuales")] VEHICULOS vEHICULOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vEHICULOS).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Color = new SelectList(db.COLORES, "Id_Color", "Color_Nombre", vEHICULOS.Id_Color);
            ViewBag.Id_Modelo = new SelectList(db.MODELOS, "Id_Modelo", "Modelo_Nombre", vEHICULOS.Id_Modelo);
            ViewBag.Id_Modelo = new SelectList(db.USUARIOS, "Id_Usuario", "Usuario_Nombre", vEHICULOS.Id_Modelo);
            return View(vEHICULOS);
        }

        // GET: VEHICULOS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VEHICULOS vEHICULOS = db.VEHICULOS.Find(id);
            if (vEHICULOS == null)
            {
                return HttpNotFound();
            }
            return View(vEHICULOS);
        }

        // POST: VEHICULOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VEHICULOS vEHICULOS = db.VEHICULOS.Find(id);
            db.VEHICULOS.Remove(vEHICULOS);
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
