using System.Threading.Tasks;

namespace AmazingShop.Product.Application.Repository.Abstraction
{
    public interface IStorageService
    {
        Task<string> UploadAsync(string fileName, byte[] content, string contentType);
    }
}