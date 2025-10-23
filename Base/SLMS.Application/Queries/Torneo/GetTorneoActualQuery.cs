using Base.DTOs;
using SLMS.Application.Queries.Liga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.Queries.Torneo
{
    public class GetTorneoActualQuery : IQuery<ApiResponse<TorneoDto>>
    {
        public int LigaId { get; set; }
    }
}
