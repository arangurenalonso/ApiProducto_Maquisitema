namespace Application.Features.Products.Queries.GetProductById
{
    using Application.Contracts;
    using Application.Contracts.Abstractions.Services;
    using Application.Contracts.ApisExternas;
    using Application.Exception;
    using Application.Features.Products.Vms;
    using Application.Models;
    using AutoMapper;
    using Domain.Entities;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using System.Linq.Expressions;

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductByIdDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMockapiRepository _mockapiRepository;
        private readonly ILogger<GetProductByIdQueryHandler> _logger;

        public GetProductByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IMockapiRepository mockapiRepository, ILogger<GetProductByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mockapiRepository = mockapiRepository;
            _logger = logger;
        }

        public async Task<ProductByIdDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {

            var includes = new List<Expression<Func<Product, object>>>();
            includes.Add(product => product.Category!);
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(request.ProductId, includes);

            if (product == null)
            {
                _logger.LogError($"No se encontro el producto id {request.ProductId}");
                throw new NotFoundException(nameof(Product), request.ProductId);
            }
            var productByIdDTO = _mapper.Map<ProductByIdDTO>(product);
            var discont = await this._mockapiRepository.GetDiscontByProductIdAsync(request.ProductId);

            if (discont == null)
            {
                _logger.LogError($"No se encontro descuento para el producto {request.ProductId}");
            }
            productByIdDTO.Discont = discont;
            productByIdDTO.DiscontPercent = discont?.Percent??0;
            productByIdDTO.FinalPrice = productByIdDTO.Price * (100 - productByIdDTO.DiscontPercent) / 100;


            return productByIdDTO;
        }
    }
}
