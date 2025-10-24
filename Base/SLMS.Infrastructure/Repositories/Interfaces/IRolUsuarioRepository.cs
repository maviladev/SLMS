
namespace SLMS.Infrastructure.Repositories.Interfaces
{
    public interface IRolUsuarioRepository : IRepository<RolUsuario>
    {
        Task<RolUsuario> GetByTipoAsync(RolUsuarioEnum tipo);
    }
}
