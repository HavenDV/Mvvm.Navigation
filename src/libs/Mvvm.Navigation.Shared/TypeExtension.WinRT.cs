namespace Mvvm.Navigation;

[MarkupExtensionReturnType(ReturnType = typeof(Type))]
public sealed class TypeExtension : MarkupExtension
{
    public Type? Type { get; set; }

    /// <inheritdoc/>
    protected override object? ProvideValue()
    {
        return Type;
    }
}