using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Project.Data.Context;
using Project.Data.Entities;
using Project.Data.Repositories.Abstract;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Repositories.Concrete
{
    public class Repository<T> : IRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext context;

        public Repository(AppDbContext context)
        {
            this.context = context;
        }

        private DbSet<T> Table { get => context.Set<T>(); }


        public async Task AddAsync(T entity)
        {
            Log.Information($"AddAsync called for {typeof(T).Name}");

            await Table.AddAsync(entity);
        }
        public async Task DeleteAsync(T entity)
        {
            Log.Information($"DeleteAsync called for {typeof(T).Name}");

            await Task.Run(() => Table.Remove(entity));
        }
        public async Task<T> UpdateAsync(T entity)
        {
            Log.Information($"UpdateAsync called for {typeof(T).Name}");

            await Task.Run(() => Table.Update(entity));
            return entity;
        }


        public async Task<T> FindByGuidAsync(Guid id, bool enableTracking = false)
        {
            Log.Information($"FindByGuidAsync called for {typeof(T).Name}");

            if (!enableTracking)
                Table.AsNoTracking();

            return await Table.FindAsync(id);
        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate, bool enableTracking = false)
        {
            Log.Information($"FindAsync called for {typeof(T).Name}");
            Log.Information($"predicate: {predicate.ToString()}");

            if (!enableTracking)
                Table.AsNoTracking();

            return await Table.FindAsync(predicate);
        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool enableTracking = false)
        {
            Log.Information($"GetAsync called for {typeof(T).Name}");
            Log.Information($"predicate: {predicate?.ToString()}");

            IQueryable<T> query = Table;

            if (!enableTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            return await query.SingleAsync();
        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false)
        {
            Log.Information($"GetAllAsync called for {typeof(T).Name}");
            if (predicate != null)
                Log.Information($"predicate: {predicate?.ToString()}");

            IQueryable<T> query = Table;

            if (!enableTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }
        public async Task<List<T>> GetAllByPagingAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false, int currentPage = 1, int pageSize = 10)
        {
            Log.Information($"GetAllByPagingAsync called for {typeof(T).Name}");
            if (predicate != null)
                Log.Information($"predicate: {predicate?.ToString()}");

            IQueryable<T> query = Table;

            if (!enableTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();

            return await query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }


        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            Log.Information($"AnyAsync called for {typeof(T).Name}");
            Log.Information($"predicate: {predicate.ToString()}");

            return await Table.AnyAsync(predicate);
        }
        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            Log.Information($"CountAsync called for {typeof(T).Name}");

            if (predicate != null)
            {
                Log.Information($"predicate: {predicate?.ToString()}");
                return await Table.AsNoTracking().CountAsync(predicate);
            }

            return await Table.AsNoTracking().CountAsync();
        }
    }
}
