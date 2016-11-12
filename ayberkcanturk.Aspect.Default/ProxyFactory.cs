namespace ayberkcanturk.Aspect.Default
{
    public class ProxyFactory
    {
        public static TI GetTransparentProxyByNewInstance<TI, T>()  where T :TI , new()
        {
            T instance = new T();

            TransparentProxy<T, TI> transparentProxy = new TransparentProxy<T, TI>(instance);
            return (TI)transparentProxy.GetTransparentProxy();
        }

        public static TI GetTransparentProxyByExistingInstance<TI, T>(T instance) where T : TI, new()
        {
            TransparentProxy<T, TI> transparentProxy = new TransparentProxy<T, TI>(instance);
            return (TI)transparentProxy.GetTransparentProxy();
        }
    }
}
