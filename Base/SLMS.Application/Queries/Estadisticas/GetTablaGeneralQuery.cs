using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.Queries.Estadisticas
{
    public class GetTablaGeneralQuery : IQuery<ApiResponse<IEnumerable<TablaPosicionDto>>>
    {
        public int TorneoId { get; set; }
}
}
