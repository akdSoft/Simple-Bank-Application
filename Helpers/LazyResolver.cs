namespace Simple_Bank_Application.Helpers;

public class LazyResolver<T> : Lazy<T>
{
    public LazyResolver(IServiceProvider provider) : base(() => provider.GetRequiredService<T>()) { }
}
