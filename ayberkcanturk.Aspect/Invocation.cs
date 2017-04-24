using System;
using System.Runtime.Remoting.Messaging;
using System.Diagnostics;
using System.Collections;

namespace ayberkcanturk.Aspect
{
    using Core;

    [DebuggerStepThrough]
    public class Invocation<TI> : IInvocation
    {
        private IMethodCallMessage MethodCallMessage { get; set; }
        public string MethodName { get; set; }
        public object[] Arguments { get; set; }
        public IDictionary Properties { get; set; }
        public Type ReturnType { get; set; }
        public object Response { get; set; }
        private TI Target { get; set; }
        public bool IsProcceeded { get; set; }

        public Invocation(TI instance, IMethodCallMessage methodCallMessage, Type returnType)
        {
            MethodCallMessage = methodCallMessage;
            MethodName = methodCallMessage.MethodName;
            Properties = methodCallMessage.Properties;
            Arguments = methodCallMessage.InArgs;
            ReturnType = returnType;
            Target = instance;
        }

        public object Procceed()
        {
            //Prevent secondary execution
            if (IsProcceeded)
                return null;

            object result = MethodCallMessage.MethodBase.Invoke(Target, MethodCallMessage.InArgs);
            IsProcceeded = true;

            return result;
        }
    }
}
