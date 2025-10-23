using Base.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.Commands.Liga
{
    // ==================== INTERFACES BASE ====================

    /// <summary>
    /// Comando base (operaciones de escritura)
    /// CQRS: Command Query Responsibility Segregation
    /// </summary>
    public interface ICommand<TResponse>
    {
    }

    /// <summary>
    /// Handler para comandos
    /// </summary>
    public interface ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken = default);
    }

    // ==================== COMMANDS LIGA ====================

    public class CreateLigaCommand : ICommand<ApiResponse<LigaDto>>
    {
        public string Nombre { get; set; }
        public string Logo { get; set; }
        public string Descripcion { get; set; }
        public string Pais { get; set; }
        public string UsuarioId { get; set; } // Para auditoría
    }
}
