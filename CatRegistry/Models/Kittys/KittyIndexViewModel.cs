using Microsoft.AspNetCore.Mvc;

namespace CatRegistry.Models.Kittys
{
    public class KittyIndexViewModel
    {
        public Guid KittyId { get; set; }
        public string KittySpeciesName { get; set; }
        public string KittyRegionOfOrigin { get; set; }
        public string KittyDescription { get; set; }
        public List<IFormFile> Files { get; set; }
        public List<KittyImageViewModel> Image { get; set; } = new List<KittyImageViewModel>();
    }
}
