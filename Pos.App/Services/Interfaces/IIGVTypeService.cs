using Pos.App.Features.IGVType.Models;

namespace Pos.App.Services.Interfaces
{
    public interface IIGVTypeService
    {
        Task<List<IGVTypeSelectModel>> GetAllIGVTypesSelectAsync();
    }
}
