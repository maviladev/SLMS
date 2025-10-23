using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Domain.Entities.Base
{
    /// <summary>
    /// Entidad base con estado
    /// </summary>
    public abstract class BaseEntityWithState : BaseEntity
    {
        public EstadoEntidad Estado { get; set; }
    }
}
