using Base.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.Queries.Liga
{
    /// <summary>
    /// Query base (operaciones de lectura)
    /// </summary>
    public interface IQuery<TResponse>
    {
    }

    /// <summary>
    /// Handler para queries
    /// </summary>
    public interface IQueryHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken = default);
    }

    // ==================== QUERIES LIGA ====================

    public class GetLigaByIdQuery : IQuery<ApiResponse<LigaDto>>
    {
        public int Id { get; set; }
    }
}
