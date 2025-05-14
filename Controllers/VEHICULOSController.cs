using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoCar360.Models; // Asegúrate de incluir el namespace del modelo

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
            VEHICULOS vehiculo = db.VEHICULOS.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        // GET: VEHICULOS/Create
        public ActionResult Create()
        {
            ViewBag.Id_Color = new SelectList(db.COLORES.OrderBy(c => c.Color_Nombre), "Id_Color", "Color_Nombre");
            ViewBag.Id_Marca = new SelectList(db.MARCAS.OrderBy(m => m.Marca_Nombre), "Id_Marca", "Marca_Nombre");
            ViewBag.Id_Modelo = new SelectList(Enumerable.Empty<SelectListItem>());
            return View();
        }

        // POST: VEHICULOS/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Modelo,Id_Color,Vehiculo_Matricula,Vehiculo_Fecha_Matriculacion,Vehiculo_Bastidor,Vehiculo_KmActuales")] VEHICULOS vehiculo)
        {
            if (Session["UsuarioId"] == null)
            {
                return RedirectToAction("Index", "USUARIOS");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    vehiculo.Id_Usuario = (int)Session["UsuarioId"];
                    db.VEHICULOS.Add(vehiculo);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al guardar en la base de datos: " + ex.Message);
                }
            }
            else
            {
                // Mostrar errores de validación
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                }
            }

            ViewBag.Id_Color = new SelectList(db.COLORES.OrderBy(c => c.Color_Nombre), "Id_Color", "Color_Nombre", vehiculo.Id_Color);
            ViewBag.Id_Marca = new SelectList(db.MARCAS.OrderBy(m => m.Marca_Nombre), "Id_Marca", "Marca_Nombre");
            ViewBag.Id_Modelo = new SelectList(db.MODELOS.Where(m => m.Id_Marca == db.MODELOS.Where(x => x.Id_Modelo == vehiculo.Id_Modelo).Select(x => x.Id_Marca).FirstOrDefault()), "Id_Modelo", "Modelo_Nombre", vehiculo.Id_Modelo);
            return View(vehiculo);
        }

        // AJAX: Obtener modelos por marca
        public JsonResult GetModelosByMarca(int idMarca)
        {
            var modelos = db.MODELOS
                            .Where(m => m.Id_Marca == idMarca)
                            .OrderBy(m => m.Modelo_Nombre)
                            .Select(m => new {
                                Id_Modelo = m.Id_Modelo,
                                Modelo_Nombre = m.Modelo_Nombre
                            }).ToList();

            return Json(modelos, JsonRequestBehavior.AllowGet);
        }

        // GET: VEHICULOS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VEHICULOS vehiculo = db.VEHICULOS.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Color = new SelectList(db.COLORES, "Id_Color", "Color_Nombre", vehiculo.Id_Color);
            ViewBag.Id_Modelo = new SelectList(db.MODELOS, "Id_Modelo", "Modelo_Nombre", vehiculo.Id_Modelo);
            ViewBag.Id_Usuario = new SelectList(db.USUARIOS, "Id_Usuario", "Usuario_Nombre", vehiculo.Id_Usuario);
            return View(vehiculo);
        }

        // POST: VEHICULOS/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Vehiculo,Id_Usuario,Id_Modelo,Id_Color,Vehiculo_Matricula,Vehiculo_Fecha_Matriculacion,Vehiculo_Bastidor,Vehiculo_KmActuales")] VEHICULOS vehiculo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehiculo).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Color = new SelectList(db.COLORES, "Id_Color", "Color_Nombre", vehiculo.Id_Color);
            ViewBag.Id_Modelo = new SelectList(db.MODELOS, "Id_Modelo", "Modelo_Nombre", vehiculo.Id_Modelo);
            ViewBag.Id_Usuario = new SelectList(db.USUARIOS, "Id_Usuario", "Usuario_Nombre", vehiculo.Id_Usuario);
            return View(vehiculo);
        }

        // GET: VEHICULOS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VEHICULOS vehiculo = db.VEHICULOS.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        // POST: VEHICULOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VEHICULOS vehiculo = db.VEHICULOS.Find(id);
            db.VEHICULOS.Remove(vehiculo);
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
