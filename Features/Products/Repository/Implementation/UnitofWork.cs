﻿using ProductAPI.VSA.Data;
using ProductAPI.VSA.Features.Products.Repository.Interface;

namespace ProductAPI.VSA.Features.Products.Repository.Implementation
{
    public class UnitofWork : IUnitofWork
    {
        private readonly AppDbContext _context;
        public UnitofWork(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> SaveChangesAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result;
        }
    }
}
