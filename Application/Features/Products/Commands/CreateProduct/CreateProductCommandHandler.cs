namespace Application.Features.Products.Commands.CreateProduct
{
    using Application.Contracts;
    using Application.Contracts.ApisExternas;
    using Application.Exception;
    using Application.Features.Products.Vms;
    using Application.Models;
    using AutoMapper;
    using Domain.Entities;
    using MediatR;
    using Microsoft.Extensions.Logging;
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductCommandHandler> _logger;
        private readonly IMockapiRepository _mockapiRepository;


        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateProductCommandHandler> logger, IMockapiRepository mockapiRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _mockapiRepository = mockapiRepository;
        }

        public async Task<ProductDTO> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

            var categoryEntity = await _unitOfWork.Repository<Category>().GetByIdAsync(request.CategoryId);
            if (categoryEntity == null)
            {
                _logger.LogError($"No se encontro el streamer id {request.CategoryId}");
                throw new NotFoundException(nameof(Category), request.CategoryId);
            }

            var productEntity = _mapper.Map<Product>(request);
            _unitOfWork.Repository<Product>().AddEntity(productEntity);
            await _unitOfWork.Complete();
            _logger.LogInformation($"Create Product Id: {productEntity.Id}");

            var discont = await this._mockapiRepository.GetDiscontByProductIdAsync(productEntity.Id);

            if (discont == null)
            {
                var newDiscont = new Discont { Percent= request.DiscontPercent, ProductId= productEntity.Id };
                await _mockapiRepository.SaveDiscontByProductIdAsync(newDiscont);
            }
            else
            {
                await _mockapiRepository.UpdateDiscontByProductIdAsync(discont,discont.Id);
            }

            return _mapper.Map<ProductDTO>(productEntity);
        }
    }
}
