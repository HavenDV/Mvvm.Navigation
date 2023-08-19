using Avalonia.Controls;
using Avalonia.Controls.Templates;
using ReactiveUI;

namespace Mvvm.Apps;

public class ViewLocator : IDataTemplate
{
    public IControl Build(object? data)
    {
        data = data ?? throw new ArgumentNullException(nameof(data));
        
        var name = data.GetType().FullName!.Replace("ViewModel", "View");
        var type = Type.GetType(name);

        if (type != null)
        {
            return (Control)Activator.CreateInstance(type)!;
        }
        else
        {
            return new TextBlock { Text = "Not Found: " + name };
        }
    }

    public bool Match(object? data)
    {
        return data is ReactiveObject;
    }
}
