using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoCar360.Models;
using AutoCar360.Controllers;

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


        public ActionResult Create()
        {
            return View();
        }


    }
}
