namespace ayberkcanturk.Aspect.UnitTest
{
    public interface IProductService
    {
        Product GetProduct(int productId);
        Product GetProductWithCache(int productId);
    }
}
