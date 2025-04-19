using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoCar360.Controllers;

namespace AutoCar360.Models
{
    public class VEHICULOS
    {
        [Key]
        public int Id_Vehiculo { get; set; }

        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }

        public int Id_Usuario { get; set; }

        [ForeignKey("Id_Usuario")]
        public virtual USUARIOS Usuario { get; set; }

    }
}
