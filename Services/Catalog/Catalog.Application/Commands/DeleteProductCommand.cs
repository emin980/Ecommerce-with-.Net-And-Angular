using MediatR;

namespace Catalog.Application.Commands
{
    public class DeleteProductCommand : BaseCommand, IRequest<bool>
    {
        public DeleteProductCommand(string id)
        {
            Id = id;
        }
    }
}
