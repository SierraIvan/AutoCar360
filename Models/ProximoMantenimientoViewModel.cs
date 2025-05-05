using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoCar360.Models
{
    public class ProximoMantenimientoViewModel
    {
        public int Id { get; set; }
        public string Matricula { get; set; }
        public string TipoMantenimiento { get; set; }
        public int? ProximosKm { get; set; }
        public int IdMantenimientoVehiculo { get; set; }
    }
}