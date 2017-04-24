using System;

namespace ayberkcanturk.Aspect.Common
{
    using Core;
    using System.Text;

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
                StringBuilder log = new StringBuilder();

                foreach (var property in invocation.Properties)
                {
                    log.Append(property);
                }

                Console.WriteLine(log.ToString());
                
                //Log here.
            }
        }
    }
}
