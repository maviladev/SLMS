using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Domain.Entities.Base
{
    /// <summary>
    /// Entidad base con auditoría completa y soft delete
    /// Principio: DRY (Don't Repeat Yourself)
    /// </summary>
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        // Auditoría
        public DateTime Creado { get; set; }
        public string CreadoPor { get; set; }
        public DateTime Modificado { get; set; }
        public string ModificadoPor { get; set; }

        // Soft Delete
        public bool Eliminado { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public string EliminadoPor { get; set; }
    }
}
