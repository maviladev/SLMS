using Base.DTOs;
using SLMS.Application.Commands.Liga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.Commands.Torneo
{
    public class CreateTorneoCommand : ICommand<ApiResponse<TorneoDto>>
    {
        public int LigaId { get; set; }
        public string Nombre { get; set; }
        public string Logo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int NumeroJornadas { get; set; }
        public string UsuarioId { get; set; }
    }
}
