using Application.Contracts.Persistence;
using Application.Contracts;
using Infractructure.Persistence;
using Infractructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Infractructure.Services;
using Application.Contracts.Abstractions.Services;
using Org.BouncyCastle.Asn1.X509.Qualified;
using Application.Contracts.ApisExternas;
using Infractructure.ApisExternas;

namespace Infractructure
{
    public static class InfraestructureServiceRegistration
    {
        public static IServiceCollection AddInfraestructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ConexionSql"),b=>b.MigrationsAssembly("Infractructure"));
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IClienteProvider, ClienteProvider>();
            services.AddScoped<IMockapiRepository, MockapiRepository>();

            return services;
        }
    }
}
