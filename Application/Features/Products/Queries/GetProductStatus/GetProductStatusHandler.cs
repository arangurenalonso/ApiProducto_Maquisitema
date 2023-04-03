using Application.Contracts;
using Application.Features.Products.Queries.GetProductList;
using Application.Features.Products.Vms;
using Application.Models;
using AutoMapper;
using Domain.Enum;
using MediatR;
using Microsoft.OpenApi.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetProductStatus
{
    public class GetProductStatusHandler : IRequestHandler<GetProductStatusQuery, List<Status>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetProductStatusHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<Status>> Handle(GetProductStatusQuery request, CancellationToken cancellationToken)
        {
            var statuses = new List<Status>();
            foreach (ProductStatus status in Enum.GetValues(typeof(ProductStatus)))
            {
                var statusObj = new Status();
                statusObj.Key = (int)status;
                statusObj.Value = status.GetDisplayName();
                statuses.Add(statusObj);
            }
            return statuses;
        }
    }
}
