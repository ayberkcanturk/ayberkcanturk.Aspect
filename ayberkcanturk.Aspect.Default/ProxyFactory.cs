namespace ayberkcanturk.Aspect.Default
{
    public class ProxyFactory
    {
        public static TI GetTransparentProxy<TI, T>()  where T :TI , new()
        {
            TI instance = new T();

            TransparentProxy<T, TI> transparentProxy = new TransparentProxy<T, TI>(instance);
            return (TI)transparentProxy.GetTransparentProxy();
        }

        public static TI GetTransparentProxy<TI, T>(TI instance) where T : TI, new()
        {
            TransparentProxy<T, TI> transparentProxy = new TransparentProxy<T, TI>(instance);
            return (TI)transparentProxy.GetTransparentProxy();
        }
    }
}
