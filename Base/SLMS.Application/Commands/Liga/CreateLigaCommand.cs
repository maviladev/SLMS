using Base.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.Commands.Liga
{
    public class CreateLigaCommand : ICommand<ApiResponse<LigaDto>>
    {
        public string Nombre { get; set; }
        public string Logo { get; set; }
        public string Descripcion { get; set; }
        public string Pais { get; set; }
        public string UsuarioId { get; set; } // Para auditoría
    }
}
