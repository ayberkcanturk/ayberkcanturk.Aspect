namespace ayberkcanturk.Aspect.Console
{
    public interface IProductService
    {
        Product GetProduct(int productId);
        Product GetProductWithCache(int productId);
    }
}
