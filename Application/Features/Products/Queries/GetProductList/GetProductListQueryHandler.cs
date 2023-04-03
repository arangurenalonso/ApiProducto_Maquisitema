namespace Application.Features.Products.Queries.GetProductList
{
    using Application.Contracts;
    using Application.Features.Products.Vms;
    using AutoMapper;
    using Domain.Entities;
    using MediatR;
    using System.Linq.Expressions;

    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, IReadOnlyList<ProductDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetProductListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<ProductDTO>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Product, object>>>();
            includes.Add(product => product.Category!);
            var products = await _unitOfWork.Repository<Product>().GetAsync(
                null,
                null,
                includes,
                true//No mantenga los datos en memoria
                );

            var productsVm = _mapper.Map<IReadOnlyList<ProductDTO>>(products);

            return productsVm;
        }
    }
}
