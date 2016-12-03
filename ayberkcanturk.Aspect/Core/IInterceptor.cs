using System.ComponentModel;

namespace ayberkcanturk.Aspect.Core
{
    public interface IInterceptor
    {

        [Description("You can get more information about executing method via invocation.")]
        void Intercept(ref IInvocation invocation);
    }
}