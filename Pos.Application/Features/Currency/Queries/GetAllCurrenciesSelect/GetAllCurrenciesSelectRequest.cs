using MediatR;

namespace Pos.Application.Features.Currency.Queries.GetAllCurrenciesSelect
{
    public record GetAllCurrenciesSelectRequest() : IRequest<List<CurrencySelectDto>>;
}
