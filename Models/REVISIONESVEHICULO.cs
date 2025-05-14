using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoCar360.Models
{
    public class REVISIONESVEHICULO
    {
        [Key] // Define la clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incremental
        public int Id { get; set; }

        // Ejemplos de otras propiedades (ajústalas a tu modelo real)
        public int ID_TipoRevision { get; set; }

        public int Id_Vehiculo { get; set; }

        public int IntevaloKm { get; set; }

        public int IntevaloTiempo { get; set; }

        // Relaciones (opcional)
        // public Vehiculo Vehiculo { get; set; }
    }
}
