using System;

namespace ayberkcanturk.Aspect.Model
{
    using Core;
    public class OnExecutionErrorEventArgs
    {
        public IInvocation Invocation { get; set; }
        public Exception Exception { get; set; }
    }
}