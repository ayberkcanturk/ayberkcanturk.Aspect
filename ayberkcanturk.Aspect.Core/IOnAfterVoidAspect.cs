namespace ayberkcanturk.Aspect.Core
{
    public interface IOnAfterVoidAspect : IAspect
    {
        void OnAfter(object value);
    }
}
