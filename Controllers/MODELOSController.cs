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
    public class MODELOSController : Controller
    {
        private AutoCar360Entities2 db = new AutoCar360Entities2();

        // GET: MODELOS
        public ActionResult Index()
        {
            var mODELOS = db.MODELOS.Include(m => m.CARROCERIAS).Include(m => m.COMBUSTIBLE).Include(m => m.ETIQUETAS).Include(m => m.MARCAS).Include(m => m.TIPOCAMBIO);
            return View(mODELOS.ToList());
        }

        // GET: MODELOS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MODELOS mODELOS = db.MODELOS.Find(id);
            if (mODELOS == null)
            {
                return HttpNotFound();
            }
            return View(mODELOS);
        }

        // GET: MODELOS/Create
        public ActionResult Create()
        {
            ViewBag.Id_Carroceria = new SelectList(db.CARROCERIAS, "Id_Carroceria", "Carroceria_Nombre");
            ViewBag.Id_Combustible = new SelectList(db.COMBUSTIBLE, "Id_Combustible", "Combustible_Nombre");
            ViewBag.Id_Etiqueta = new SelectList(db.ETIQUETAS, "Id_Etiqueta", "Etiqueta_Nombre");
            ViewBag.Id_Marca = new SelectList(db.MARCAS, "Id_Marca", "Marca_Nombre");
            ViewBag.Id_TipoCambio = new SelectList(db.TIPOCAMBIO, "Id_TipoCambio", "TipoCambio_Nombre");
            return View();
        }

        // POST: MODELOS/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Modelo,Id_Marca,Id_TipoCambio,Id_Etiqueta,Id_Combustible,Id_Carroceria,Modelo_Nombre")] MODELOS mODELOS)
        {
            if (ModelState.IsValid)
            {
                db.MODELOS.Add(mODELOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Carroceria = new SelectList(db.CARROCERIAS, "Id_Carroceria", "Carroceria_Nombre", mODELOS.Id_Carroceria);
            ViewBag.Id_Combustible = new SelectList(db.COMBUSTIBLE, "Id_Combustible", "Combustible_Nombre", mODELOS.Id_Combustible);
            ViewBag.Id_Etiqueta = new SelectList(db.ETIQUETAS, "Id_Etiqueta", "Etiqueta_Nombre", mODELOS.Id_Etiqueta);
            ViewBag.Id_Marca = new SelectList(db.MARCAS, "Id_Marca", "Marca_Nombre", mODELOS.Id_Marca);
            ViewBag.Id_TipoCambio = new SelectList(db.TIPOCAMBIO, "Id_TipoCambio", "TipoCambio_Nombre", mODELOS.Id_TipoCambio);
            return View(mODELOS);
        }

        // GET: MODELOS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MODELOS mODELOS = db.MODELOS.Find(id);
            if (mODELOS == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Carroceria = new SelectList(db.CARROCERIAS, "Id_Carroceria", "Carroceria_Nombre", mODELOS.Id_Carroceria);
            ViewBag.Id_Combustible = new SelectList(db.COMBUSTIBLE, "Id_Combustible", "Combustible_Nombre", mODELOS.Id_Combustible);
            ViewBag.Id_Etiqueta = new SelectList(db.ETIQUETAS, "Id_Etiqueta", "Etiqueta_Nombre", mODELOS.Id_Etiqueta);
            ViewBag.Id_Marca = new SelectList(db.MARCAS, "Id_Marca", "Marca_Nombre", mODELOS.Id_Marca);
            ViewBag.Id_TipoCambio = new SelectList(db.TIPOCAMBIO, "Id_TipoCambio", "TipoCambio_Nombre", mODELOS.Id_TipoCambio);
            return View(mODELOS);
        }

        // POST: MODELOS/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Modelo,Id_Marca,Id_TipoCambio,Id_Etiqueta,Id_Combustible,Id_Carroceria,Modelo_Nombre")] MODELOS mODELOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mODELOS).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Carroceria = new SelectList(db.CARROCERIAS, "Id_Carroceria", "Carroceria_Nombre", mODELOS.Id_Carroceria);
            ViewBag.Id_Combustible = new SelectList(db.COMBUSTIBLE, "Id_Combustible", "Combustible_Nombre", mODELOS.Id_Combustible);
            ViewBag.Id_Etiqueta = new SelectList(db.ETIQUETAS, "Id_Etiqueta", "Etiqueta_Nombre", mODELOS.Id_Etiqueta);
            ViewBag.Id_Marca = new SelectList(db.MARCAS, "Id_Marca", "Marca_Nombre", mODELOS.Id_Marca);
            ViewBag.Id_TipoCambio = new SelectList(db.TIPOCAMBIO, "Id_TipoCambio", "TipoCambio_Nombre", mODELOS.Id_TipoCambio);
            return View(mODELOS);
        }

        // GET: MODELOS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MODELOS mODELOS = db.MODELOS.Find(id);
            if (mODELOS == null)
            {
                return HttpNotFound();
            }
            return View(mODELOS);
        }

        // POST: MODELOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MODELOS mODELOS = db.MODELOS.Find(id);
            db.MODELOS.Remove(mODELOS);
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
