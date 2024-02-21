using Bookkeepify.Data;
using Bookkeepify.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookkeepify.Services
{
    public class ProductTypeService
    {
        private readonly AppDbContext _context;

        public ProductTypeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }

        public async Task AddProductTypeAsync(ProductType productType)
        {
            _context.ProductTypes.Add(productType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductTypeAsync(int productTypeId)
        {
            var productType = await _context.ProductTypes.FindAsync(productTypeId);
            if (productType != null)
            {
                _context.ProductTypes.Remove(productType);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateProductTypeAsync(ProductType productType)
        {
            _context.Entry(productType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}
