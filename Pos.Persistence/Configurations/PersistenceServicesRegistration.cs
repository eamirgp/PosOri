using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pos.Application.Contracts.Persistence;
using Pos.Application.Contracts.Persistence.Queries;
using Pos.Persistence.Context;
using Pos.Persistence.Repository;
using Pos.Persistence.Repository.Queries;

namespace Pos.Persistence.Configurations
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PosDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DB")));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductQueryRepository, ProductQueryRepository>();
            services.AddScoped<IIGVTypeRepository, IGVTypeRepository>();
            services.AddScoped<IIGVTypeQueryRepository, IGVTypeQueryRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryQueryRepository, CategoryQueryRepository>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            services.AddScoped<IWarehouseQueryRepository, WarehouseQueryRepository>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IInventoryQueryRepository, InventoryQueryRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonQueryRepository, PersonQueryRepository>();
            services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
            services.AddScoped<IDocumentTypeQueryRepository, DocumentTypeQueryRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IPurchaseQueryRepository, PurchaseQueryRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<IVoucherTypeRepository, VoucherTypeRepository>();
            services.AddScoped<IVoucherTypeQueryRepository, VoucherTypeQueryRepository>();
            services.AddScoped<IVoucherSerieRepository, VoucherSerieRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<ICurrencyQueryRepository, CurrencyQueryRepository>();
            services.AddScoped<IUnitOfMeasureRepository, UnitOfMeasureRepository>();
            services.AddScoped<IUnitOfMeasureQueryRepository, UnitOfMeasureQueryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
