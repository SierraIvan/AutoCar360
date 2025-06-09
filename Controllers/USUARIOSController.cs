using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoCar360.Models;

namespace AutoCar360.Controllers
{
    public class USUARIOSController : Controller
    {
        private AutoCar360Entities2 db = new AutoCar360Entities2();

        // GET: USUARIOS (Login)
        public ActionResult Index()
        {
            return View();
        }

        // POST: USUARIOS (Login)
        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.USUARIOS.FirstOrDefault(u =>
                    u.Usuario_Nombre == model.Usuario_Nombre &&
                    u.Usuario_Password == model.Usuario_Password);

                if (user != null)
                {
                    Session["UsuarioId"] = user.Id_Usuario;
                    Session["UsuarioNombre"] = user.Usuario_Nombre;

                    return RedirectToAction("Index", "VEHICULOS");
                }

                ViewBag.Error = "Usuario o contraseña incorrectos.";
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        // GET: USUARIOS/Create (Registro)
        public ActionResult Create()
        {
            return View();
        }

        // POST: USUARIOS/Create (Registro)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(USUARIOS usuario)
        {
            if (ModelState.IsValid)
            {
                // Verifica si ya existe un usuario con ese nombre y contraseña
                bool yaExiste = db.USUARIOS.Any(u =>
                    u.Usuario_Nombre == usuario.Usuario_Nombre &&
                    u.Usuario_Password == usuario.Usuario_Password);

                if (yaExiste)
                {
                    ModelState.AddModelError("", "Ya existe un usuario con esa contraseña.");
                    return View(usuario);
                }

                try
                {
                    usuario.Usuario_FechaRegistro = DateTime.Now;
                    db.USUARIOS.Add(usuario);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al crear el usuario: " + ex.Message);
                }
            }

            return View(usuario);
        }
    }
}
