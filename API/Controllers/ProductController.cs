namespace API.Controllers
{
    using Application.Features.Products.Commands.CreateProduct;
    using Application.Features.Products.Commands.UpdateProduct;
    using Application.Features.Products.Queries.GetProductById;
    using Application.Features.Products.Queries.GetProductList;
    using Application.Features.Products.Queries.GetProductStatus;
    using Application.Features.Products.Vms;
    using Application.Models;
    using Domain.Enum;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using System.Net;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;
        public ProductController(IMediator mediator, IMemoryCache memoryCache)
        {
            _mediator = mediator;
            _cache = memoryCache;
        }


        [HttpGet( Name = "GetProducts")]
        [ProducesResponseType(typeof(IReadOnlyList<ProductDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<ProductDTO>>> Get()
        {
            var query = new GetProductListQuery();
            var products = await _mediator.Send(query);

            return Ok(products);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        [ProducesResponseType(typeof(ProductDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductDTO>> Get (int id)
        {
            var query = new GetProductByIdQuery(id);
            var product = await _mediator.Send(query);

            return Ok(product);
        }

        [HttpPost(Name = "CreateProduct")]
        [ProducesResponseType(typeof(ProductDTO), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ProductDTO>> Create([FromBody] CreateProductCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPut(Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ProductDTO> Update([FromBody] UpdateProductCommand command)
        {
            return await _mediator.Send(command);
        }


        [HttpGet("Status", Name = "GetProductStatuses")]
        public async Task<ActionResult<List<Status>>> GetProductStatuses()
        {
            if (_cache.TryGetValue("ProductStatuses", out List<Status> cachedStatuses))
            {
                return cachedStatuses;
            }


            var query = new GetProductStatusQuery();
            var statuses= await _mediator.Send(query);

            MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(5)
            };

            _cache.Set("ProductStatuses", statuses, cacheOptions);

            return statuses;
        }


    }
}
