using System;

namespace ayberkcanturk.Aspect.Core
{
    public interface IInvocation
    {
        string MethodName { get; set; }
        object[] Arguments { get; set; }
        Type ReturnType { get; set; }
        object Response { get; set; }
        object Procceed();
        bool IsProcceeded { get; set; }
    }
}
