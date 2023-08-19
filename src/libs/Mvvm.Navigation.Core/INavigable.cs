namespace Mvvm.Navigation;

/// <summary>
/// INavigable represents any object that is hosting its own navigator -
/// usually this object is your AppViewModel or MainWindow object.
/// </summary>
public interface INavigable<T>
{
    /// <summary>
    /// Gets the Navigator associated with this INavigable.
    /// </summary>
    Navigator<T> Navigator { get; }
}