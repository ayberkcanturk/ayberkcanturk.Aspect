using System;

namespace ayberkcanturk.Aspect.Model
{
    using Core;
    public class PostExecutionEventArgs : EventArgs
    {
        public IInvocation Invocation { get; set; }

        public PostExecutionEventArgs(IInvocation invocation)
        {
            this.Invocation = invocation;
        }
    }
}
