using CatRegistry.Domain;
using CatRegistry.Dto;

namespace CatRegistry.ServiceInterface
{
    public interface IFileServices
    {
        void UploadFileToDatabase(KittyDto dto, Kitty kitty);
        Task<FileToDatabase> RemoveImageFromDatabase(FileToDatabase dto);
    }
}
