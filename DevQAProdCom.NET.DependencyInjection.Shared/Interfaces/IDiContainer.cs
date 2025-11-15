namespace DevQAProdCom.NET.DependencyInjection.Shared.Interfaces
{
    public interface IDiContainer
    {
        T GetRequiredService<T>() where T : notnull;
        T? TryGetService<T>();
    }
}

