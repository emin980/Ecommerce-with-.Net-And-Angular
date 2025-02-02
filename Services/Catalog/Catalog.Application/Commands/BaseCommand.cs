using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Application.Commands
{
    public class BaseCommand
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
