using System;
using System.Runtime.Remoting.Messaging;
using System.Diagnostics;
using ayberkcanturk.Aspect.Core;

namespace ayberkcanturk.Aspect.Default
{
    [DebuggerStepThrough]
    public class Invocation<T> : IInvocation
    {
        private IMethodCallMessage MethodCallMessage { get; set; }
        public string MethodName { get; set; }
        public object[] Arguments { get; set; }
        public Type ReturnType { get; set; }
        public object Response { get; set; }
        private T Target { get; set; }
        public bool IsProcceeded { get; set; }

        public Invocation(IMethodCallMessage methodCallMessage, Type returnType)
        {
            MethodCallMessage = methodCallMessage;
            MethodName = methodCallMessage.MethodName;
            Arguments = methodCallMessage.InArgs;
            ReturnType = returnType;
            Target = Activator.CreateInstance<T>();
        }

        public object Procceed()
        {
            object result = MethodCallMessage.MethodBase.Invoke(Target, MethodCallMessage.InArgs);
            IsProcceeded = true;

            return result;
        }
    }
}
