using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
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
            if (Session["UsuarioId"] == null)
            {
                return RedirectToAction("Index", "USUARIOS");
            }

            ViewBag.Id_Color = new SelectList(db.COLORES.OrderBy(c => c.Color_Nombre), "Id_Color", "Color_Nombre");
            ViewBag.Id_Marca = new SelectList(db.MARCAS.OrderBy(m => m.Marca_Nombre), "Id_Marca", "Marca_Nombre");
            ViewBag.Id_Modelo = new SelectList(Enumerable.Empty<SelectListItem>());

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Modelo,Id_Color,Vehiculo_Matricula,Vehiculo_Fecha_Matriculacion,Vehiculo_Bastidor,Vehiculo_KmActuales,Id_Usuario")] VEHICULOS vehiculo)
        {
            if (Session["UsuarioId"] == null)
            {
                return RedirectToAction("Index", "USUARIOS");
            }

            // Asignar el ID de usuario desde la sesión
            vehiculo.Id_Usuario = (int)Session["UsuarioId"];

            // Validar que modelo y color existan
            var modeloExistente = db.MODELOS.Any(m => m.Id_Modelo == vehiculo.Id_Modelo);
            var colorExistente = db.COLORES.Any(c => c.Id_Color == vehiculo.Id_Color);

            if (!modeloExistente)
            {
                ModelState.AddModelError("Id_Modelo", "El modelo seleccionado no existe");
            }

            if (!colorExistente)
            {
                ModelState.AddModelError("Id_Color", "El color seleccionado no existe");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.VEHICULOS.Add(vehiculo);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            ModelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al guardar en la base de datos: " + ex.Message);
                }
            }

            // Log de errores de validación
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
            }

            // Recargar los dropdowns necesarios
            ViewBag.Id_Color = new SelectList(db.COLORES.OrderBy(c => c.Color_Nombre), "Id_Color", "Color_Nombre", vehiculo.Id_Color);
            ViewBag.Id_Marca = new SelectList(db.MARCAS.OrderBy(m => m.Marca_Nombre), "Id_Marca", "Marca_Nombre");

            // Cargar modelos de la marca seleccionada
            if (vehiculo.Id_Modelo > 0)
            {
                var marcaDelModelo = db.MODELOS.Where(m => m.Id_Modelo == vehiculo.Id_Modelo).Select(m => m.Id_Marca).FirstOrDefault();
                ViewBag.Id_Modelo = new SelectList(db.MODELOS.Where(m => m.Id_Marca == marcaDelModelo), "Id_Modelo", "Modelo_Nombre", vehiculo.Id_Modelo);
            }
            else
            {
                ViewBag.Id_Modelo = new SelectList(Enumerable.Empty<SelectListItem>());
            }

            return View(vehiculo);
        }

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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var vehiculo = db.VEHICULOS
                        .Include(v => v.MANTENIMIENTOVEHICULO.Select(m => m.PROXIMOMANTENIMIENTO))
                        .Include(v => v.MANTENIMIENTOVEHICULO.Select(m => m.HISTORIALMANTENIMIENTO))
                        .FirstOrDefault(v => v.Id_Vehiculo == id);

                    if (vehiculo == null)
                    {
                        return HttpNotFound();
                    }

                    foreach (var mantenimiento in vehiculo.MANTENIMIENTOVEHICULO.ToList())
                    {
                        if (mantenimiento.PROXIMOMANTENIMIENTO != null)
                        {
                            db.PROXIMOMANTENIMIENTO.RemoveRange(mantenimiento.PROXIMOMANTENIMIENTO);
                        }

                        if (mantenimiento.HISTORIALMANTENIMIENTO != null)
                        {
                            db.HISTORIALMANTENIMIENTO.RemoveRange(mantenimiento.HISTORIALMANTENIMIENTO);
                        }
                    }

                    db.MANTENIMIENTOVEHICULO.RemoveRange(vehiculo.MANTENIMIENTOVEHICULO);

                    db.VEHICULOS.Remove(vehiculo);

                    db.SaveChanges();
                    transaction.Commit();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {


                    ModelState.AddModelError("", $"No se pudo eliminar el vehículo. Error: {ex.InnerException?.Message ?? ex.Message}");
                    return View("Delete", db.VEHICULOS.Find(id));
                }
            }
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
