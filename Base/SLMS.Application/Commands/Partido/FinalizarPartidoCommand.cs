using Base.DTOs;
using SLMS.Application.Commands.Liga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.Commands.Partido
{
    public class FinalizarPartidoCommand : ICommand<ApiResponse<PartidoDto>>
    {
        public int PartidoId { get; set; }
        public int GolesLocal { get; set; }
        public int GolesVisitante { get; set; }
        public string UsuarioId { get; set; }
    }
}
