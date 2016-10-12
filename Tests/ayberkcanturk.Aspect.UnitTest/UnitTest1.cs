using Microsoft.VisualStudio.TestTools.UnitTesting;
using ayberkcanturk.Aspect.Default;

namespace ayberkcanturk.Aspect.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private IProductService productService;

        [TestInitialize]
        public void Initialize()
        {
            productService = TransparentProxy<ProductService, IProductService>.GenerateProxy();
        }

        [TestMethod]
        public void Is_InterceptedMethod_And_Method_Results_Equal()
        {
            IProductService _productService = new ProductService();
            Product _product = _productService.GetProductWithCache(1);
            Product product = productService.GetProduct(1);

            Assert.AreEqual(_product, product);
        }
    }
}
