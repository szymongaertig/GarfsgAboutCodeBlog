
namespace AspNetCoreApp.Services
{
    public interface IProductService
    {
        bool DoesLoggedUserHaveAccessForProduct(int procuctId);
    }
}
