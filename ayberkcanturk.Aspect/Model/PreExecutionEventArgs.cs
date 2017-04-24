using System;

namespace ayberkcanturk.Aspect.Model
{
    using Core;
    public class PreExecutionEventArgs : EventArgs
    {
        public IInvocation Invocation { get; set; }

        public PreExecutionEventArgs(IInvocation invocation)
        {
            this.Invocation = invocation;
        }
    }
}