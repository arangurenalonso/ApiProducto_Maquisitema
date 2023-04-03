using Application.Contracts;
using Application.Contracts.Abstractions.Services;
using Application.Contracts.ApisExternas;
using Application.Exception;
using Application.Features.Products.Queries.GetProductById;
using Application.Features.Products.Vms;
using Application.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infractructure.ApisExternas
{
    public class MockapiRepository : IMockapiRepository
    {
        private readonly IClienteProvider _clienteProvider;
        private readonly ILogger<MockapiRepository> _logger;
        private readonly string _url = "https://642858c446fd35eb7c50efec.mockapi.io";
        public MockapiRepository(IClienteProvider clienteProvider, ILogger<MockapiRepository> logger)
        {
            _clienteProvider = clienteProvider;
            _logger = logger;
        }
        public async Task<Discont?> GetDiscontByProductIdAsync(int ProductId)
        {
            var path = "/api/v1/Discont";
            ResponseModel response = await this._clienteProvider.GetList<Discont>(_url, path, "");

            if (!response.IsSuccess)
            {
                _logger.LogError($"Error no se encontraron descuentos comuniquese con sistemas");
                return null;
            }
            else
            {
                var listDisconts = (List<Discont>)response.Result!;
                var discont = listDisconts.Where(x => x.ProductId == ProductId).FirstOrDefault();
                if (discont == null)
                {
                    _logger.LogError($"No se encontro descuento para el producto {ProductId}");
                    return null;
                }
                return discont;
            }
        }

        public async Task<Discont?> SaveDiscontByProductIdAsync(Discont discont)
        {
            var path = "/api/v1/Discont";
            ResponseModel response = await this._clienteProvider.Post<Discont>(_url, path, discont);

            if (!response.IsSuccess)
            {
               
                return null;
            }
            else
            {
                return (Discont)response.Result!;
            }
        }

        public async Task<Discont?> UpdateDiscontByProductIdAsync(Discont discont, int id)
        {
            var path = "/api/v1/Discont";
            ResponseModel response = await this._clienteProvider.Put<Discont>(_url, path, discont,id);

            if (!response.IsSuccess)
            {
                return null;
            }
            else
            {
                return (Discont)response.Result!;
            }
        }
    }
}
