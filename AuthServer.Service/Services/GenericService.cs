using AuthServer.Core.Repositories;
using AuthServer.Core.Services;
using AuthServer.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DTOs;
using System.Linq.Expressions;

namespace AuthServer.Service.Services
{
    public class GenericService<TEntity, TDto> : IServiceGeneric<TEntity, TDto> where TEntity : class where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IGenericRepository<TEntity> _repository;

        public GenericService(IUnitOfWork unitOfWork, IGenericRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<Response<TDto>> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                return Response<TDto>.Fail("Product not found", 404, true);
            var productDto = ObjectMapper.Mapper.Map<TDto>(product);
            return Response<TDto>.Success(productDto, 200);
        }

        public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            var products = ObjectMapper.Mapper.Map<List<TDto>>(await _repository.GetAllAsync());
            return Response<IEnumerable<TDto>>.Success(products, 200);
        }

        public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var list = _repository.Where(predicate);
            return Response<IEnumerable<TDto>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await list.ToListAsync()), 200);

        }

        public async Task<Response<TDto>> AddAsync(TDto entity)
        {
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            var newEntityDto = ObjectMapper.Mapper.Map<TDto>(newEntity);
            return Response<TDto>.Success(newEntityDto, 200);
        }

        public async Task<Response<NoDataDTO>> Remove(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return Response<NoDataDTO>.Fail("Product not found", 404, true);
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            return Response<NoDataDTO>.Success(204);
        }

        public async Task<Response<NoDataDTO>> Update(TDto entity, int id)
        {
            var newEntity = await _repository.GetByIdAsync(id);
            if (newEntity == null)
                return Response<NoDataDTO>.Fail("Product not found", 404, true);
            var updatedEntity = ObjectMapper.Mapper.Map(entity, newEntity);
            _repository.Update(updatedEntity);
            await _unitOfWork.CommitAsync();
            return Response<NoDataDTO>.Success(204);
        }
    }
}
