using AN.Ticket.Application.Interfaces.Base;
using AN.Ticket.Domain.Entities.Base;
using AN.Ticket.Domain.Interfaces.Base;

namespace AN.Ticket.Application.Services.Base;
public class Service<TDto, TEntity> : IService<TDto, TEntity>
        where TEntity : class, IEntity
{
    private readonly IRepository<TEntity> _repository;

    public Service(IRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _repository.SaveAsync(entity);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _repository.Update(entity);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _repository.Delete(entity);
        await Task.CompletedTask;
    }
}
