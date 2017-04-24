using System;

namespace ayberkcanturk.Aspect
{
    using Core;
    using Model;

    [AttributeUsage(AttributeTargets.Method)]
    public abstract class Interceptor : Attribute, IInterceptor
    {
        public delegate void OnExecutionError(object sender, OnExecutionErrorEventArgs args);
        public delegate void PreExecution(object sender, PreExecutionEventArgs args);
        public delegate void PostExecution(object sender, PostExecutionEventArgs args);
        public event PreExecution PreExecutionHandler;
        public event PostExecution PostExecutionHandler;
        public event OnExecutionError ExecutionErrorHandler;

        public virtual void Intercept(ref IInvocation invocation)
        {
            try
            {
                PreExecutionHandler?.Invoke(this, new PreExecutionEventArgs(invocation));
                invocation.Procceed();
                PostExecutionHandler?.Invoke(this, new PostExecutionEventArgs(invocation));
            }
            catch (Exception e)
            {
                ExecutionErrorHandler?.Invoke(this, new OnExecutionErrorEventArgs(invocation, e));
            }
        }
    }
}
