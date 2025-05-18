using CatRegistry.Domain;
using CatRegistry.Dto;

namespace CatRegistry.ServiceInterface
{
    public interface IKittyServices
    {
        Task<Kitty> Create(KittyDto dto);
        Task<Kitty> Delete(Guid id);
        Task<Kitty> DetailsAsync(Guid id);

    }
}
