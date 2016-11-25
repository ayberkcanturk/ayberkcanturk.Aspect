namespace ayberkcanturk.Aspect.Common
{
    public interface IProductService
    {
        Product GetProduct(int productId);
        Product GetProductWithCache(int productId);
    }
}
