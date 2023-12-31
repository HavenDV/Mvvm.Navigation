﻿//HintName: HostBuilderExtensions.g.cs
#nullable enable

namespace Mvvm.Navigation
{
    public static class HostBuilderExtensions
    {
        public static global::Microsoft.Extensions.Hosting.IHostBuilder AddMvvmNavigation(this global::Microsoft.Extensions.Hosting.IHostBuilder builder)
        {
            builder = builder ?? throw new global::System.ArgumentNullException(nameof(builder));

            return builder.ConfigureServices(static (_, services) => services.AddMvvmNavigation());
        }
    }

    public static class HostApplicationBuilderExtensions
    {
        public static global::Microsoft.Extensions.Hosting.HostApplicationBuilder AddMvvmNavigation(this global::Microsoft.Extensions.Hosting.HostApplicationBuilder builder)
        {
            builder = builder ?? throw new global::System.ArgumentNullException(nameof(builder));
            _ = builder.Services.AddMvvmNavigation();
            return builder;
        }
    }
}