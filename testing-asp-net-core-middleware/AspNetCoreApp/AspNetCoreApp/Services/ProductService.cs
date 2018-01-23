namespace AspNetCoreApp.Services
{
    public class ProductService : IProductService
    {
        public bool DoesLoggedUserHaveAccessForProduct(int productId)
        {
            return productId > 2;
        }
    }
}
