using System;

namespace ayberkcanturk.Aspect.Common
{
    using Core;
    public class ExceptionHandlingInterceptor : Interceptor
    {
        public override void Intercept(ref IInvocation invocation)
        {
            try
            {
                invocation.Response = invocation.Procceed();
            }
            catch (Exception e)
            {
                //Log here.
            }
        }
    }
}
