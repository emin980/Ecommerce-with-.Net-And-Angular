using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository, IBrandRepository, IProductTypeRepository
    {
        public ICatalogContext _context { get; }

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        async Task<Product> IProductRepository.GetProduct(string id)
        {
            return await _context
                .Products.Find(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        async Task<IEnumerable<Product>> IProductRepository.GetProducts()
        {
            return await _context
                .Products
                .Find(x => true).ToListAsync();
        }

        async Task<IEnumerable<Product>> IProductRepository.GetProductsByBrand(string brandName)
        {
            return await _context
               .Products
               .Find(x => x.Brands.Name.ToLower() == brandName.ToLower())
               .ToListAsync();
        }

        async Task<IEnumerable<Product>> IProductRepository.GetProductsByName(string name)
        {
            return await _context
                .Products
                .Find(x => x.Name.ToLower() == name.ToLower())
                .ToListAsync();
        }
        async Task<Product> IProductRepository.CreateProduct(Product product)
        {
            await _context
                .Products
                .InsertOneAsync(product);
            return product;
        }
        async Task<bool> IProductRepository.DeleteProduct(string id)
        {
            var deletedProduct = await _context
                .Products
                .DeleteOneAsync(x => x.Id == id);
            return deletedProduct.IsAcknowledged && deletedProduct.DeletedCount > 0;
        }
        async Task<bool> IProductRepository.UpdateProduct(Product product)
        {
            var updatedProduct = await _context
                .Products
                .ReplaceOneAsync(x => x.Id == product.Id, product);
            return updatedProduct.IsAcknowledged && updatedProduct.ModifiedCount > 0;
        }

        async Task<IEnumerable<ProductBrand>> IBrandRepository.GetAllBrands()
        {
            return await _context
                .Brands
                .Find(x => true).ToListAsync();
        }

        async Task<IEnumerable<ProductType>> IProductTypeRepository.GetAllTypes()
        {
            return await _context
                .Types
                .Find(x => true).ToListAsync();
        }
    }
}
