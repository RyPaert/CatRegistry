using CatRegistry.Models.Kittys;

namespace CatRegistry.Dto
{
    public class FileToDatabaseDto
    {
        public Guid ID { get; set; }
        public Guid ImageID { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public Guid? KittyId { get; set; }
    }
}
