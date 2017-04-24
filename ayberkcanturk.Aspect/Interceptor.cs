using System;

namespace ayberkcanturk.Aspect
{
    using Core;
    using Model;

    [AttributeUsage(AttributeTargets.Method)]
    public abstract class Interceptor : Attribute, IInterceptor
    {
        public delegate void OnExecutionError(IInvocation invocation, Exception exception);
        public delegate void PreExecution(object sender, PreExecutionEventArgs args);
        public delegate void PostExecution(object sender, PostExecutionEventArgs args);
        public event PreExecution PreExecutionHandler;
        public event PostExecution PostExecutionHandler;
        public event OnExecutionError ExecutionErrorHandler;

        public virtual void Intercept(ref IInvocation invocation)
        {
            try
            {
                PreExecutionHandler?.Invoke(this, new PreExecutionEventArgs() { Invocation = invocation });
                invocation.Procceed();
                PostExecutionHandler?.Invoke(this, new PostExecutionEventArgs() { Invocation = invocation });
            }
            catch (Exception e)
            {
                ExecutionErrorHandler?.Invoke(invocation, e);
            }
        }
    }
}
