using ayberkcanturk.Aspect.Core;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace ayberkcanturk.Aspect.Default
{
    [DebuggerStepThrough]
    public class TransparentProxy<T, TI> : RealProxy where T : TI, new()
    {
        T instance = default(T);

        private TransparentProxy()
            : base(typeof(TI))
        {
            instance = new T();
        }

        public static TI GenerateProxy()
        {
            TransparentProxy<T,TI> instance = new TransparentProxy<T,TI>();
            return (TI)instance.GetTransparentProxy();
        }

        public override IMessage Invoke(IMessage message)
        {
            IMethodCallMessage methodCallMessage = message as IMethodCallMessage;
            Type realType = typeof(T);
            MethodInfo methodInfo = realType.GetMethod(methodCallMessage.MethodName);
            ReturnMessage returnMessage = null;

            try
            {

                object[] aspects = methodInfo.GetCustomAttributes(typeof(IAspect), true);
                object[] interceptors = methodInfo.GetCustomAttributes(typeof(IInterceptor), true);
                IInvocation invocation = null;
                object response = null;

                OnBeforeAspect(ref response, aspects);

                if (response != null)
                {
                    returnMessage = new ReturnMessage(response, null, 0, methodCallMessage.LogicalCallContext, methodCallMessage);
                }

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

                if (invocation != null)
                {
                    if (invocation.IsProcceeded.Equals(false))
                    {
                        response = methodCallMessage.MethodBase.Invoke(instance, methodCallMessage.InArgs);
                        returnMessage = new ReturnMessage(response, null, 0, methodCallMessage.LogicalCallContext, methodCallMessage);
                    }
                }

                OnAfterAspect(ref response, aspects);

                return returnMessage;
            }
            catch (Exception e)
            {
                return new ReturnMessage(e, methodCallMessage);
            }
        }

        private void OnBeforeAspect(ref object response, object[] aspects)
        {
            foreach (IAspect loopAttribute in aspects)
            {
                if (loopAttribute is IOnBeforeVoidAspect)
                {
                    ((IOnBeforeVoidAspect)loopAttribute).OnBefore();
                }
                else if (loopAttribute is IOnBeforeInterruptingAspect)
                {
                    response = ((IOnBeforeInterruptingAspect)loopAttribute).OnBefore();
                }
            }
        }

        //private ReturnMessage RunInterceptors(ref object response, object[] interceptors, IInvocation invocation, IMethodCallMessage methodCallMessage, MethodInfo methodInfo)
        //{
        //    if (interceptors.Length > 0)
        //    {
        //        invocation = new Invocation<T>(methodCallMessage, methodInfo.ReturnType);
        //        foreach (IInterceptor interceptor in interceptors)
        //        {
        //            interceptor.Intercept(ref invocation);
        //            if (invocation.Response != null)
        //            {
        //                response = invocation.Response;
        //            }
        //        }
        //    }

        //    if (invocation != null)
        //    {
        //        if (invocation.IsProcceeded.Equals(false))
        //        {
        //            response = methodCallMessage.MethodBase.Invoke(new T(), methodCallMessage.InArgs);
        //            return new ReturnMessage(response, null, 0, methodCallMessage.LogicalCallContext, methodCallMessage);
        //        }
        //    }
        //}


        private void OnAfterAspect(ref object result, object[] aspects)
        {
            foreach (IAspect loopAttribute in aspects)
            {
                if (loopAttribute is IOnAfterVoidAspect)
                {
                    ((IOnAfterVoidAspect)loopAttribute).OnAfter(result);
                }
            }
        }
    }
}
