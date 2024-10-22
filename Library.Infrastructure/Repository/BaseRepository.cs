using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Create(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            _context.Add(entity);
        }
        public void Update(T entity)
        {
            entity.Update();
            _context.Update(entity);
        }
        public void Delete(T entity)
        {
            entity.Delete();
            _context.Update(entity);
        }
        public void Restore(T entity)
        {
            entity.Restore();
            _context.Update(entity);
        }
        public async Task<T> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public async Task<List<T>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }
    }
}
