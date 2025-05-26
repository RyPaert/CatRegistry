using CatRegistry.Models.Kittys;

namespace CatRegistry.Dto
{
    public class KittyDto
    {
        public Guid KittyId { get; set; }
        public string KittySpeciesName { get; set; }
        public string KittyRegionOfOrigin { get; set; }
        public string KittyDescription { get; set; }
        public List<IFormFile> Files { get; set; }
        public IEnumerable<FileToDatabaseDto> Image { get; set; } = new List<FileToDatabaseDto>();


    }
}
