namespace ProductsAPI.Core.Utils.Config;

public static class WebHostEnvironmentEx
{
    public static bool IsLocal(this IWebHostEnvironment env) => env.IsEnvironment("Local");
}