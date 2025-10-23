using Base.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.Commands.Liga
{
    public class DeleteLigaCommand : ICommand<ApiResponse<bool>>
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
    }
}
