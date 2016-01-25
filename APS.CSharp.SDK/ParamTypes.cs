
namespace APS.CSharp.SDK
{
    public enum ParamSource
    {
        Body,
        Query,
        Path
    }

    public static class ParamSourceExtension
    {
        public static string ToStringAPS(this ParamSource value)
        {
            return value.ToString().ToLower();
        }
    }
}
