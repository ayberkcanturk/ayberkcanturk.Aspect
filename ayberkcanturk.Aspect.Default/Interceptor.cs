using ayberkcanturk.Aspect.Core;
using System;

namespace ayberkcanturk.Aspect.Default
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class Interceptor : Attribute, IInterceptor
    {
        public abstract void Intercept(ref IInvocation invocation);

    }
}
