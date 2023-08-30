namespace Mvvm.Navigation;

/// <summary>
/// Resolver returns views for view models.
/// </summary>
public interface IResolver
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    IViewFor Resolve(Type type);
}