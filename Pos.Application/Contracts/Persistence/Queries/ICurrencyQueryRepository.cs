using Pos.Application.Features.Currency.Queries.GetAllCurrenciesSelect;

namespace Pos.Application.Contracts.Persistence.Queries
{
    public interface ICurrencyQueryRepository
    {
        Task<List<CurrencySelectDto>> GetAllCurrenciesSelectAsync();
    }
}
