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
            int? usuarioId = (int?)Session["UsuarioId"];

            if (usuarioId == null)
            {
                return RedirectToAction("Index", "USUARIOS");
            }

            ViewBag.Id_TipoRevision = new SelectList(db.TIPOREVISIONES, "Id_TipoRevision", "Revision_Nombre");
            ViewBag.Id_Vehiculo = new SelectList(db.VEHICULOS.Where(v => v.Id_Usuario == usuarioId), "Id_Vehiculo", "Vehiculo_Matricula");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_RevisionesVehiculo,Id_TipoRevision,Id_Vehiculo,RevisionesVehiculo_Intervalokm,RevisionesVehiculo_IntervaloTiempo")] REVISIONESVEHICULO rEVISIONESVEHICULO)
        {
            // Verificar errores de validación
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                System.Diagnostics.Debug.WriteLine($"Error de validación: {error.ErrorMessage}");
            }

            if (ModelState.IsValid)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        // 1. Guardar la revisión principal
                        db.REVISIONESVEHICULO.Add(rEVISIONESVEHICULO);
                        db.SaveChanges();

                        System.Diagnostics.Debug.WriteLine($"ID Generado: {rEVISIONESVEHICULO.Id_RevisionesVehiculo}");

                        // 2. Obtener el vehículo asociado
                        var vehiculo = db.VEHICULOS.FirstOrDefault(v => v.Id_Vehiculo == rEVISIONESVEHICULO.Id_Vehiculo);
                        if (vehiculo == null)
                        {
                            throw new Exception("Vehículo no encontrado");
                        }

                        if (!vehiculo.Vehiculo_KmActuales.HasValue)
                        {
                            throw new Exception("El vehículo no tiene kilometraje registrado");
                        }

                        // 3. Calcular y guardar próxima revisión
                        decimal proximoKm = (decimal)(vehiculo.Vehiculo_KmActuales.Value + rEVISIONESVEHICULO.RevisionesVehiculo_Intervalokm);

                        var nuevoProximo = new PROXIMASREVISIONES
                        {
                            Id_RevisionesVehiculo = rEVISIONESVEHICULO.Id_RevisionesVehiculo,
                            ProximaRevision_intervaloKm = (int)proximoKm,
                            ProximaRevision_intervaloTiempo = rEVISIONESVEHICULO.RevisionesVehiculo_IntervaloTiempo
                        };

                        db.PROXIMASREVISIONES.Add(nuevoProximo);
                        db.SaveChanges();

                        // 4. Crear registro en el historial
                        var nuevohistorial = new HISTORIALREVISIONES
                        {
                            Id_RevisionesVehiculo = rEVISIONESVEHICULO.Id_RevisionesVehiculo,
                            HistorialRevisiones_km = vehiculo.Vehiculo_KmActuales.Value,
                            HistorialRevisiones_Fecha = DateTime.Now,
                            HistorialRevisiones_Taller = "Taller desconocido",
                            HistorialRevisiones_Precio = 0,
                            HistorialRevisiones_Comentarios = "Sin comentarios",
                        };

                        db.HISTORIALREVISIONES.Add(nuevohistorial);
                        db.SaveChanges();

                        // Confirmar la transacción
                        transaction.Commit();

                        return RedirectToAction("Index", "PROXIMASREVISIONES");
                    }
                    catch (Exception ex)
                    {
                        // Hacer rollback y registrar el error
                        transaction.Rollback();
                        System.Diagnostics.Debug.WriteLine($"ERROR COMPLETO: {ex.ToString()}");

                        if (ex.InnerException != null)
                        {
                            System.Diagnostics.Debug.WriteLine($"INNER EXCEPTION: {ex.InnerException.ToString()}");
                            ModelState.AddModelError("", $"Error interno: {ex.InnerException.Message}");
                        }

                        ModelState.AddModelError("", $"Error técnico al guardar. Detalles: {ex.Message}");
                    }
                }
            }

            // Si llegamos aquí, hubo un error - recargar los SelectList
            int? usuarioId = (int?)Session["UsuarioId"];
            ViewBag.Id_TipoRevision = new SelectList(db.TIPOREVISIONES, "Id_TipoRevision", "Revision_Nombre", rEVISIONESVEHICULO.Id_TipoRevision);
            ViewBag.Id_Vehiculo = new SelectList(db.VEHICULOS.Where(v => v.Id_Usuario == usuarioId), "Id_Vehiculo", "Vehiculo_Matricula", rEVISIONESVEHICULO.Id_Vehiculo);
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
