using Microsoft.CodeAnalysis;

namespace H.Generators;

public static class IncrementalGeneratorInitializationContextExtensions
{
    public static IncrementalValueProvider<Framework> TryDetectFramework(this IncrementalGeneratorInitializationContext context)
    {
        return context.AnalyzerConfigOptionsProvider
            .Select((options, _) => options.TryRecognizeFramework());
    }
}