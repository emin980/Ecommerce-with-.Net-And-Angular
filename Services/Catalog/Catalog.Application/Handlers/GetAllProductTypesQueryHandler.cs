using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetAllProductTypesQueryHandler : IRequestHandler<GetAllProductTypesQuery, IList<ProductTypeResponse>>
    {
        private readonly IProductTypeRepository _productTypeRepository;

        public GetAllProductTypesQueryHandler(IProductTypeRepository productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }
        public async Task<IList<ProductTypeResponse>> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
        {
            var productTypeList = await _productTypeRepository.GetAllTypes();

            var productTypeResponseList = ProductMapper.Mapper.Map<IList<ProductTypeResponse>>(productTypeList);
            return productTypeResponseList;
        }
    }
}
