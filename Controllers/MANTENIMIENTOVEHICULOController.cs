using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace AutoCar360.Controllers
{
    public class MANTENIMIENTOVEHICULOController : Controller
    {
        private AutoCar360Entities2 db = new AutoCar360Entities2();

        // GET: MANTENIMIENTOVEHICULO
        public ActionResult Index()
        {
            int? usuarioId = (int?)Session["UsuarioId"];

            if (usuarioId == null)
            {
                return RedirectToAction("Index", "USUARIOS");
            }

            var mANTENIMIENTOVEHICULO = db.MANTENIMIENTOVEHICULO
                .Include(m => m.TIPOMANTENIMIENTO)
                .Include(m => m.VEHICULOS)
                .Where(m => m.VEHICULOS.Id_Usuario == usuarioId);

            return View(mANTENIMIENTOVEHICULO.ToList());
        }

        // GET: MANTENIMIENTOVEHICULO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int? usuarioId = (int?)Session["UsuarioId"];
            MANTENIMIENTOVEHICULO mANTENIMIENTOVEHICULO = db.MANTENIMIENTOVEHICULO
                .FirstOrDefault(m => m.Id_MantenimientoVehiculo == id && m.VEHICULOS.Id_Usuario == usuarioId);

            if (mANTENIMIENTOVEHICULO == null)
            {
                return HttpNotFound();
            }
            return View(mANTENIMIENTOVEHICULO);
        }
        // GET: MANTENIMIENTOVEHICULO/Create
        public ActionResult Create()
        {
            int? usuarioId = (int?)Session["UsuarioId"];

            if (usuarioId == null)
            {
                return RedirectToAction("Index", "USUARIOS");
            }

            ViewBag.Id_TipoMantenimiento = new SelectList(db.TIPOMANTENIMIENTO, "Id_TipoMantenimiento", "Mantenimiento_Nombre");
            ViewBag.Id_Vehiculo = new SelectList(db.VEHICULOS.Where(v => v.Id_Usuario == usuarioId), "Id_Vehiculo", "Vehiculo_Matricula");

            return View();
        }

        // POST: MANTENIMIENTOVEHICULO/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_MantenimientoVehiculo,Id_Vehiculo,Id_TipoMantenimiento,MantenimientoVehiculo_IntervaloKm")] MANTENIMIENTOVEHICULO mANTENIMIENTOVEHICULO)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.MANTENIMIENTOVEHICULO.Add(mANTENIMIENTOVEHICULO);
                        db.SaveChanges();

                        System.Diagnostics.Debug.WriteLine($"ID Generado: {mANTENIMIENTOVEHICULO.Id_MantenimientoVehiculo}");

                        var vehiculo = db.VEHICULOS
                            .FirstOrDefault(v => v.Id_Vehiculo == mANTENIMIENTOVEHICULO.Id_Vehiculo);

                        if (vehiculo == null)
                            throw new Exception("Vehículo no encontrado");

                        if (!vehiculo.Vehiculo_KmActuales.HasValue)
                            throw new Exception("El vehículo no tiene kilometraje registrado");

                        decimal proximoKm = (decimal)(vehiculo.Vehiculo_KmActuales.Value + mANTENIMIENTOVEHICULO.MantenimientoVehiculo_IntervaloKm);

                        var nuevoProximo = new PROXIMOMANTENIMIENTO
                        {
                            Id_MantenimientoVehiculo = mANTENIMIENTOVEHICULO.Id_MantenimientoVehiculo,
                            ProximoMantenimiento_Km = (int?)proximoKm
                        };

                        db.PROXIMOMANTENIMIENTO.Add(nuevoProximo);
                        db.SaveChanges();

                        transaction.Commit();
                        TempData["SuccessMessage"] = $"Mantenimiento creado. Próximo a los {proximoKm} km";

                        return RedirectToAction("Index", "PROXIMOMANTENIMIENTO"); // ✅ Redirección correcta
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        System.Diagnostics.Debug.WriteLine($"ERROR: {ex}");
                        ModelState.AddModelError("", $"Error técnico al guardar. Detalles: {ex.Message}");
                    }
                }
            }

            int? usuarioId = (int?)Session["UsuarioId"];
            ViewBag.Id_TipoMantenimiento = new SelectList(db.TIPOMANTENIMIENTO, "Id_TipoMantenimiento", "Mantenimiento_Nombre", mANTENIMIENTOVEHICULO.Id_TipoMantenimiento);
            ViewBag.Id_Vehiculo = new SelectList(db.VEHICULOS.Where(v => v.Id_Usuario == usuarioId), "Id_Vehiculo", "Vehiculo_Matricula", mANTENIMIENTOVEHICULO.Id_Vehiculo);

            return View(mANTENIMIENTOVEHICULO); // ✅ Mostrar errores en el formulario si falló
        }

        // GET: MANTENIMIENTOVEHICULO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int? usuarioId = (int?)Session["UsuarioId"];
            MANTENIMIENTOVEHICULO mANTENIMIENTOVEHICULO = db.MANTENIMIENTOVEHICULO
                .FirstOrDefault(m => m.Id_MantenimientoVehiculo == id && m.VEHICULOS.Id_Usuario == usuarioId);

            if (mANTENIMIENTOVEHICULO == null)
            {
                return HttpNotFound();
            }

            ViewBag.Id_TipoMantenimiento = new SelectList(db.TIPOMANTENIMIENTO, "Id_TipoMantenimiento", "Mantenimiento_Nombre", mANTENIMIENTOVEHICULO.Id_TipoMantenimiento);
            ViewBag.Id_Vehiculo = new SelectList(db.VEHICULOS.Where(v => v.Id_Usuario == usuarioId), "Id_Vehiculo", "Vehiculo_Matricula", mANTENIMIENTOVEHICULO.Id_Vehiculo);
            return View(mANTENIMIENTOVEHICULO);
        }

        // POST: MANTENIMIENTOVEHICULO/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_MantenimientoVehiculo,Id_Vehiculo,Id_TipoMantenimiento,MantenimientoVehiculo_IntervaloKm")] MANTENIMIENTOVEHICULO mANTENIMIENTOVEHICULO)
        {
            int? usuarioId = (int?)Session["UsuarioId"];

            if (usuarioId == null)
            {
                return RedirectToAction("Index", "USUARIOS");
            }

            // Verificar que el vehículo seleccionado pertenece al usuario
            var vehiculo = db.VEHICULOS.Find(mANTENIMIENTOVEHICULO.Id_Vehiculo);
            if (vehiculo == null || vehiculo.Id_Usuario != usuarioId)
            {
                ModelState.AddModelError("Id_Vehiculo", "El vehículo seleccionado no es válido");
            }

            if (ModelState.IsValid)
            {
                db.Entry(mANTENIMIENTOVEHICULO).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_TipoMantenimiento = new SelectList(db.TIPOMANTENIMIENTO, "Id_TipoMantenimiento", "Mantenimiento_Nombre", mANTENIMIENTOVEHICULO.Id_TipoMantenimiento);
            ViewBag.Id_Vehiculo = new SelectList(db.VEHICULOS.Where(v => v.Id_Usuario == usuarioId), "Id_Vehiculo", "Vehiculo_Matricula", mANTENIMIENTOVEHICULO.Id_Vehiculo);
            return View(mANTENIMIENTOVEHICULO);
        }

        // GET: MANTENIMIENTOVEHICULO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int? usuarioId = (int?)Session["UsuarioId"];
            MANTENIMIENTOVEHICULO mANTENIMIENTOVEHICULO = db.MANTENIMIENTOVEHICULO
                .FirstOrDefault(m => m.Id_MantenimientoVehiculo == id && m.VEHICULOS.Id_Usuario == usuarioId);

            if (mANTENIMIENTOVEHICULO == null)
            {
                return HttpNotFound();
            }
            return View(mANTENIMIENTOVEHICULO);
        }

        // POST: MANTENIMIENTOVEHICULO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int? usuarioId = (int?)Session["UsuarioId"];
            MANTENIMIENTOVEHICULO mANTENIMIENTOVEHICULO = db.MANTENIMIENTOVEHICULO
                .FirstOrDefault(m => m.Id_MantenimientoVehiculo == id && m.VEHICULOS.Id_Usuario == usuarioId);

            if (mANTENIMIENTOVEHICULO == null)
            {
                return HttpNotFound();
            }

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