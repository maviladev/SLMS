using Base.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.Queries.Liga
{
    public class GetAllLigasQuery : IQuery<ApiResponse<IEnumerable<LigaDto>>>
    {
        public bool? SoloActivas { get; set; }
        public string Pais { get; set; }
    }
}
