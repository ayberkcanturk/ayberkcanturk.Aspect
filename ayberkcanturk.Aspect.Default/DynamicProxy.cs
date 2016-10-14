using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace ayberkcanturk.Aspect.Default
{
    [DebuggerStepThrough]
    public class DynamicProxy<T, TI> : RealProxy where T : TI, new()
    {
        T instance = default(T);
        private Predicate<MethodInfo> _filter;
        public event EventHandler<IMethodCallMessage> BeforeExecute;
        public event EventHandler<IMethodCallMessage> AfterExecute;
        public event EventHandler<IMethodCallMessage> ErrorExecuting;

        private DynamicProxy()
            : base(typeof(TI))
        {
            instance = new T();
        }

        public static TI GenerateProxy()
        {
            DynamicProxy<T, TI> instance = new DynamicProxy<T, TI>();
            return (TI)instance.GetTransparentProxy();
        }

        private void OnBeforeExecute(IMethodCallMessage methodCall)
        {
            if (BeforeExecute != null)
            {
                var methodInfo = methodCall.MethodBase as MethodInfo;
                if (_filter(methodInfo))
                    BeforeExecute(this, methodCall);
            }
        }
        private void OnAfterExecute(IMethodCallMessage methodCall)
        {
            if (AfterExecute != null)
            {
                var methodInfo = methodCall.MethodBase as MethodInfo;
                if (_filter(methodInfo))
                    AfterExecute(this, methodCall);
            }
        }
        private void OnErrorExecuting(IMethodCallMessage methodCall)
        {
            if (ErrorExecuting != null)
            {
                var methodInfo = methodCall.MethodBase as MethodInfo;
                if (_filter(methodInfo))
                    ErrorExecuting(this, methodCall);
            }
        }

        public Predicate<MethodInfo> Filter
        {
            get { return _filter; }
            set
            {
                if (value == null)
                    _filter = m => true;
                else
                    _filter = value;
            }
        }

        public override IMessage Invoke(IMessage message)
        {
            var methodCall = message as IMethodCallMessage;
            var methodInfo = methodCall.MethodBase as MethodInfo;
            OnBeforeExecute(methodCall);
            try
            {
                var result = methodInfo.Invoke(instance, methodCall.InArgs);
                OnAfterExecute(methodCall);
                return new ReturnMessage(
                  result, null, 0, methodCall.LogicalCallContext, methodCall);
            }
            catch (Exception e)
            {
                OnErrorExecuting(methodCall);
                return new ReturnMessage(e, methodCall);
            }
        }
    }
}
