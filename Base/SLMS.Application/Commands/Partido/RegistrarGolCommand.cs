using Base.DTOs;
using SLMS.Application.Commands.Liga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.Commands.Partido
{
    public class RegistrarGolCommand : ICommand<ApiResponse<EstadisticaDto>>
    {
        public int PartidoId { get; set; }
        public int JugadorId { get; set; }
        public int Minuto { get; set; }
        public int? JugadorAsistenteId { get; set; }
        public string Descripcion { get; set; }
    }
}
