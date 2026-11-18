namespace HIMS.API.Extensions
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class SharedAuthenticatedAttribute : AuthenticatedAttribute
    {
    }
}
