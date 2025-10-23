using Base.DTOs;
using SLMS.Application.Queries.Liga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.Queries.Torneo
{
    public class GetTorneosByLigaQuery : IQuery<ApiResponse<IEnumerable<TorneoDto>>>
    {
        public int LigaId { get; set; }
        public bool? SoloActivos { get; set; }
    }
}
