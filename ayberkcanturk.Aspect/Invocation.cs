using System;
using System.Runtime.Remoting.Messaging;
using System.Diagnostics;
using ayberkcanturk.Aspect.Core;

namespace ayberkcanturk.Aspect
{
    [DebuggerStepThrough]
    public class Invocation<TI> : IInvocation
    {
        private IMethodCallMessage MethodCallMessage { get; set; }
        public string MethodName { get; set; }
        public object[] Arguments { get; set; }
        public Type ReturnType { get; set; }
        public object Response { get; set; }
        private TI Target { get; set; }
        public bool IsProcceeded { get; set; }

        public Invocation(TI instance, IMethodCallMessage methodCallMessage, Type returnType)
        {
            MethodCallMessage = methodCallMessage;
            MethodName = methodCallMessage.MethodName;
            Arguments = methodCallMessage.InArgs;
            ReturnType = returnType;
            Target = instance;
        }

        public object Procceed()
        {
            if (IsProcceeded)
                return null;

            object result = MethodCallMessage.MethodBase.Invoke(Target, MethodCallMessage.InArgs);
            IsProcceeded = true;

            return result;
        }
    }
}
