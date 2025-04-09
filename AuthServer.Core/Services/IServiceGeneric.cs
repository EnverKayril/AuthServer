using SharedLibrary.DTOs;
using System.Linq.Expressions;

namespace AuthServer.Core.Services
{
    public interface IServiceGeneric<TEntity, TDto> where TEntity : class where TDto : class
    {
        Task<Response<TDto>> GetByIdAsync(int id);
        Task<Response<IEnumerable<TEntity>>> GetAllAsync();
        Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate);
        Task<Response<TDto>> AddAsync(TEntity entity);
        Task<Response<NoDataDTO>> Remove(TEntity entity);
        Task<Response<NoDataDTO>> Update(TEntity entity);
    }
}
