using Catalog.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;


namespace Catalog.Infrastructure.Data
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; set; }

        public IMongoCollection<ProductBrand> Brands { get; set; }

        public IMongoCollection<ProductType> Types { get; set; }

        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            Brands = database.GetCollection<ProductBrand>(configuration.GetValue<string>("DatabaseSettings:BrandsCollection"));
            Types = database.GetCollection<ProductType>(configuration.GetValue<string>("DatabaseSettings:TypesCollection"));
            ContextSeedHelper<Product>.InsertSeedData(Products, "products.json");
            ContextSeedHelper<ProductBrand>.InsertSeedData(Brands, "brands.json");
            ContextSeedHelper<ProductType>.InsertSeedData(Types, "types.json");
        }

    }
}
