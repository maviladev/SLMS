using Base.DTOs;
using SLMS.Application.Queries.Liga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.Queries.Estadisticas
{
    public class GetGolesJugadorQuery : IQuery<ApiResponse<EstadisticasJugadorDto>>
    {
        public int JugadorId { get; set; }
        public int? TorneoId { get; set; }
    }
}
