using CatRegistry.Models.Kittys;

namespace CatRegistry.Domain
{
    public class Kitty
    {
        public Guid KittyId { get; set; }
        public string KittySpeciesName { get; set; }
        public string KittyRegionOfOrigin { get; set; }
        public string KittyDescription { get; set; }
    }
}
