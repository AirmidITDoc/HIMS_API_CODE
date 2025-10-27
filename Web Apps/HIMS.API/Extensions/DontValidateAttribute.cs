namespace HIMS.API.Extensions
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DontValidateAttribute : Attribute
    {
    }
}