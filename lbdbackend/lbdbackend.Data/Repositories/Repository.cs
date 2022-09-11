using lbdbackend.Core.Entities;
using lbdbackend.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace lbdbackend.Data.Repositories {
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context) {
            _context = context;
        }

        public async Task AddAsync(TEntity entity) {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task<int> CommitAsync() {
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression) {
            return await _context.Set<TEntity>().AnyAsync(expression);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, params string[] includes) {
            IQueryable<TEntity> query = _context.Set<TEntity>().Where(expression);

            if (includes != null && includes.Length > 0) {
                foreach (string include in includes) {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync();
        }
        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, params string[] includes) {
            IQueryable<TEntity> query = _context.Set<TEntity>().Where(expression);

            if (includes != null && includes.Length > 0) {
                foreach (string include in includes) {
                    query = query.Include(include);
                }
            }

            return await query.ToListAsync();
        }
        public async Task<int> GetCount(Expression<Func<TEntity, bool>> expression) {
            IQueryable<TEntity> query = _context.Set<TEntity>().Where(expression);
            return await query.CountAsync();
        }



        public async Task RemoveOrRestore(int? id) {
            var item = GetAsync(e => e.ID == id);
            if (item.Result != null) {
                if (item.Result.IsDeleted) {
                    item.Result.IsDeleted = false;
                    item.Result.DeletedAt = null;
                }
                else {
                    item.Result.IsDeleted = true;
                    item.Result.DeletedAt = DateTime.UtcNow;
                }
            }
        }

        public async Task<TEntity> GetLast() {
            return _context.Set<TEntity>().OrderByDescending(x => !x.IsDeleted).FirstOrDefault();

    }


}
}
