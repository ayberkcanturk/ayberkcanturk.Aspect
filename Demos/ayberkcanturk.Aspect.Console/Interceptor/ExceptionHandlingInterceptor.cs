using ayberkcanturk.Aspect.Core;
using System;

namespace ayberkcanturk.Aspect.Console
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ExceptionHandlingInterceptor : Attribute, IInterceptor
    {
        public void Intercept(ref IInvocation invocation)
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
