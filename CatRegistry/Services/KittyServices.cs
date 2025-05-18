using CatRegistry.Data;
using CatRegistry.Domain;
using CatRegistry.Dto;
using CatRegistry.ServiceInterface;
using Microsoft.EntityFrameworkCore;

namespace CatRegistry.Services
{
    public class KittyServices : IKittyServices
    {
        private readonly KittyContext _context;
        private readonly IFileServices _fileServices;

        public KittyServices(KittyContext context, IFileServices fileServices)
        {
            _context = context;
            _fileServices = fileServices;
        }
        public async Task<Kitty> DetailsAsync(Guid id)
        {
            var result = await _context.Kittys
                .FirstOrDefaultAsync(x => x.KittyId == id);
            return result;
        }

        public async Task<Kitty> Create(KittyDto dto)
        {
            Kitty kitty = new();

            kitty.KittyId = Guid.NewGuid();
            kitty.KittyRegionOfOrigin = dto.KittyRegionOfOrigin;
            kitty.KittyDescription = dto.KittyDescription;
            kitty.KittySpeciesName = dto.KittySpeciesName;

            if (dto.Files != null)
            {
                _fileServices.UploadFileToDatabase(dto, kitty);
            }
            await _context.Kittys.AddAsync(kitty);
            await _context.SaveChangesAsync();

            return kitty;
        }
        public async Task<Kitty> Delete(Guid id)
        {
            var result = await _context.Kittys
                .FirstOrDefaultAsync(x => x.KittyId == id);
            _context.Kittys.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
