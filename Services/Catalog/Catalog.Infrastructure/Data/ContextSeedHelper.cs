using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;
namespace Catalog.Infrastructure.Data
{
    public static class ContextSeedHelper<T> where T : BaseEntity
    {
        public static void InsertSeedData(IMongoCollection<T> collection, string jsonFileNameWithExtension)
        {
            string path = Path.Combine("D:\\Udemy Courses\\Ecommerce-with-.Net-And-Angular\\Services\\Catalog\\Catalog.Infrastructure\\Data\\SeedData", jsonFileNameWithExtension);
            var collectionJsonData = File.ReadAllText(path);
            var collectionJsonDataList = JsonSerializer.Deserialize<List<T>>(collectionJsonData);
            if (collectionJsonDataList != null && collectionJsonDataList.Count >= 1)
            {
                var searchNewCollectionNames = collectionJsonDataList.Select(x => x.Name).Distinct().ToList();
                var filter = Builders<T>.Filter.In(x => x.Name, searchNewCollectionNames);
                var existingData = collection.Find(filter).ToList();
                var existingNames = existingData.Select(x => x.Name).ToHashSet();
                var nonExistDataList = collectionJsonDataList
                                .Where(x => !existingNames.Contains(x.Name))
                                .ToList();

                if (nonExistDataList.Any())
                {
                    if (nonExistDataList.Count <= 20)
                    {
                        foreach (var item in nonExistDataList)
                        {
                            collection.InsertOneAsync(item);
                        }
                    }
                    else
                    {
                        collection.InsertManyAsync(nonExistDataList);
                    }
                }
            }
            else
            {
                throw new Exception($"Cannot read collection on [{jsonFileNameWithExtension}] filename.");
            }

        }
    }
}
