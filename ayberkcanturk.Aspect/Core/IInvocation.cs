using System;
using System.Collections;

namespace ayberkcanturk.Aspect.Core
{
    public interface IInvocation
    {
        string MethodName { get; set; }
        object[] Arguments { get; set; }
        Type ReturnType { get; set; }
        object Response { get; set; }
        bool IsProcceeded { get; set; }
        object Procceed();
        IDictionary Properties { get; set; }
    }
}
