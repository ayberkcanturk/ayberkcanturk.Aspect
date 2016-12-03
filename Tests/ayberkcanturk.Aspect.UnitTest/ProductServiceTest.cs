using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ayberkcanturk.Aspect.UnitTest
{
    using Common;

    [TestClass]
    public class ProductServiceTest
    {
        private IProductService productProxyService;
        private IProductService productService;

        [TestInitialize]
        public void Initialize()
        {
            productProxyService = ProxyFactory.GetTransparentProxy<IProductService, ProductService>();
            productService = new ProductService();
        }

        [TestMethod]
        public void Is_Proxy_Instance_Of_Type()
        {
            Assert.IsInstanceOfType(productProxyService, typeof(IProductService));
        }

        [TestMethod]
        public void Is_Intercepted_Method_And_Method_Results_Equal()
        {
            Product actual = productService.GetProductWithCache(1);
            Product expected = productProxyService.GetProduct(1);
            Assert.AreEqual(expected, actual);
        }
    }
}
