namespace ayberkcanturk.Aspect
{
    public class ProxyFactory
    {

        public static TI GetTransparentProxy<TI, T>()  where T :TI, new()
        {
            TransparentProxy<TI, T> transparentProxy = new TransparentProxy<TI, T>();

            return (TI)transparentProxy.GetTransparentProxy();
        }

        public static TI GetTransparentProxy<TI, T>(TI instance) where T : TI, new()
        {
            TransparentProxy<TI, T> transparentProxy = new TransparentProxy<TI, T>(instance);

            return (TI)transparentProxy.GetTransparentProxy();
        }
    }
}
