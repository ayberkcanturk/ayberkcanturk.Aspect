using ayberkcanturk.Aspect.Core;
using System;

namespace ayberkcanturk.Aspect.Default
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class Interceptor : Attribute, IInterceptor
    {
        void IInterceptor.Intercept(ref IInvocation invocation)
        {
            Intercept(ref invocation);
        }

        public virtual void Intercept(ref IInvocation invocation)
        {
            throw new NotImplementedException();
        }
    }
}
