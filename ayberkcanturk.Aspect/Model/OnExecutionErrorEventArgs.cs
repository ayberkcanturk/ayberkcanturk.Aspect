using System;

namespace ayberkcanturk.Aspect.Model
{
    using Core;
    public class OnExecutionErrorEventArgs : EventArgs
    {
        public IInvocation Invocation { get; set; }
        public Exception Exception { get; set; }

        public OnExecutionErrorEventArgs(IInvocation invocation, Exception exception)
        {
            this.Invocation = invocation;
            this.Exception = exception;
        }
    }
}