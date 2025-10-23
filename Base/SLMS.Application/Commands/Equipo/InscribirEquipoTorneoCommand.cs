using Base.DTOs;
using SLMS.Application.Commands.Liga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.Commands.Equipo
{
    public class InscribirEquipoTorneoCommand : ICommand<ApiResponse<EquipoTorneoDto>>
    {
        public int EquipoId { get; set; }
        public int TorneoId { get; set; }
        public string DirectorTecnico { get; set; }
        public string UsuarioId { get; set; }
    }
}
