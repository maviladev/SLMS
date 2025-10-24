using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.Commands.Interfaces
{
    /// <summary>
    /// Comando base (operaciones de escritura)
    /// CQRS: Command Query Responsibility Segregation
    /// </summary>
    public interface ICommand<TResponse>
    {
    }
}
