﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RHWebApplication.Database;

public class DAL<T> where T : class
{
    private readonly ApplicationContext _context;

    public DAL(ApplicationContext _context)
    {
        this._context = _context;
    }

    public async Task<IEnumerable<T>> ListAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }
    public async Task AddAsync(T t)
    {
        await _context.Set<T>().AddAsync(t);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(T t)
    {
        _context.Set<T>().Update(t);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(T t)
    {
        _context.Set<T>().Remove(t);
        await _context.SaveChangesAsync();
    }
    public async Task<T?> FindByAsync(Expression<Func<T, bool>> e)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(e);
    }
}