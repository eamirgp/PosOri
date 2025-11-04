using MediatR;
using Pos.Application.Contracts.Persistence.Queries;

namespace Pos.Application.Features.Currency.Queries.GetAllCurrenciesSelect
{
    public class GetAllCurrenciesSelectRequestHandler : IRequestHandler<GetAllCurrenciesSelectRequest, List<CurrencySelectDto>>
    {
        private readonly ICurrencyQueryRepository _currencyQueryRepository;

        public GetAllCurrenciesSelectRequestHandler(ICurrencyQueryRepository currencyQueryRepository)
        {
            _currencyQueryRepository = currencyQueryRepository;
        }

        public async Task<List<CurrencySelectDto>> Handle(GetAllCurrenciesSelectRequest request, CancellationToken cancellationToken)
        {
            return await _currencyQueryRepository.GetAllCurrenciesSelectAsync();
        }
    }
}
