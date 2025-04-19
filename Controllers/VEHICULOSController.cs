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
            if (Session["UsuarioId"] == null)
            {
                return RedirectToAction("Index", "USUARIOS");
            }

            int usuarioId = (int)Session["UsuarioId"];

            var vehiculosUsuario = db.VEHICULOS
                .Include(v => v.COLORES)
                .Include(v => v.MODELOS)
                .Include(v => v.USUARIOS)
                .Where(v => v.Id_Usuario == usuarioId);

            return View(vehiculosUsuario.ToList());
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
            // Se elimina el dropdown de usuario porque se asignará automáticamente
            return View();
        }

        // POST: VEHICULOS/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Modelo,Id_Color,Vehiculo_Matricula,Vehiculo_Fecha_Matriculacion,Vehiculo_Bastidor,Vehiculo_KmActuales")] VEHICULOS vEHICULOS)
        {
            if (Session["UsuarioId"] == null)
            {
                return RedirectToAction("Index", "USUARIOS");
            }

            if (ModelState.IsValid)
            {
                vEHICULOS.Id_Usuario = (int)Session["UsuarioId"];
                db.VEHICULOS.Add(vEHICULOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Color = new SelectList(db.COLORES, "Id_Color", "Color_Nombre", vEHICULOS.Id_Color);
            ViewBag.Id_Modelo = new SelectList(db.MODELOS, "Id_Modelo", "Modelo_Nombre", vEHICULOS.Id_Modelo);
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
            ViewBag.Id_Usuario = new SelectList(db.USUARIOS, "Id_Usuario", "Usuario_Nombre", vEHICULOS.Id_Usuario);
            return View(vEHICULOS);
        }

        // POST: VEHICULOS/Edit/5
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
            ViewBag.Id_Usuario = new SelectList(db.USUARIOS, "Id_Usuario", "Usuario_Nombre", vEHICULOS.Id_Usuario);
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
