using AN.Ticket.Domain.Entities.Base;

namespace AN.Ticket.Application.Interfaces.Base;
public interface IService<TDto, TEntity>
        where TEntity : IEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(Guid id);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}
