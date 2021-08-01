using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;
        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(sale => sale.Date >= minDate);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(sale => sale.Date <= maxDate);
            }
            return await result.Include(seller => seller.Seller)
            .Include(dep => dep.Seller.Department)
            .OrderByDescending(date => date.Date)
            .ToListAsync();
        }
        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(sale => sale.Date >= minDate);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(sale => sale.Date <= maxDate);
            }
            return await result.Include(seller => seller.Seller)
            .Include(dep => dep.Seller.Department)
            .OrderByDescending(date => date.Date)
            .GroupBy(dep => dep.Seller.Department)
            .ToListAsync();
        }
    }
}