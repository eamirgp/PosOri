using Pos.App.Features.UnitOfMeasure.Models;

namespace Pos.App.Services.Interfaces
{
    public interface IUnitOfMeasureService
    {
        Task<List<UnitOfMeasureSelectModel>> GetAllUnitOfMeasuresSelectAsync();
    }
}
