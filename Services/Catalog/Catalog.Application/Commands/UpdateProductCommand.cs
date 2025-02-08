using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Application.Commands
{
    public class UpdateProductCommand : BaseCommand, IRequest<bool>
    {
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public ProductBrand Brands { get; set; }
        public ProductType Types { get; set; }
        public decimal Price { get; set; }
    }
}
