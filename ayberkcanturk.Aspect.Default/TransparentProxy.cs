using ayberkcanturk.Aspect.Core;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace ayberkcanturk.Aspect.Default
{
    //[DebuggerStepThrough]
    public class TransparentProxy<T, TI> : RealProxy where T : TI, new()
    {
        TI instance = default(T);

        private TransparentProxy()
            : base(typeof(TI))
        {
            instance = new T();
        }

        internal TransparentProxy(TI instance) : base(typeof(TI))
        {
            this.instance = instance;
        }

        public override IMessage Invoke(IMessage message)
        {
            IMethodCallMessage methodCallMessage = message as IMethodCallMessage;
            Type realType = typeof(T);
            MethodInfo methodInfo = realType.GetMethod(methodCallMessage.MethodName);
            ReturnMessage returnMessage = null;

            try
            {
                object[] interceptors = methodInfo.GetCustomAttributes(typeof(IInterceptor), true);
                IInvocation invocation = null;
                object response = null;

                if (interceptors.Length > 0)
                {
                    invocation = new Invocation<T>(methodCallMessage, methodInfo.ReturnType);
                    foreach (IInterceptor interceptor in interceptors)
                    {
                        interceptor.Intercept(ref invocation);
                        if (invocation.Response != null)
                        {
                            response = invocation.Response;
                        }
                    }
                }

                if (invocation != null && invocation.IsProcceeded.Equals(false))
                {
                    response = methodCallMessage.MethodBase.Invoke(instance, methodCallMessage.InArgs);
                }

                returnMessage = new ReturnMessage(response, null, 0, methodCallMessage.LogicalCallContext, methodCallMessage);

                return returnMessage;
            }
            catch (Exception e)
            {
                return new ReturnMessage(e, methodCallMessage);
            }
        }
    }
}
