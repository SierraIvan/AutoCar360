using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoCar360.Models
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
    [Display(Name = "Nombre de usuario")]
        public string Usuario_Nombre { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [Display(Name = "Contraseña")]
        public string Usuario_Password { get; set; }

        public DateTime Usuario_FechaRegistro { get; set; }
    }
}