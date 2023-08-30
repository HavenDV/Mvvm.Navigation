using UIKit;
using Mvvm.Apps.Maui;

#if DEBUG
if (System.Diagnostics.Debugger.IsAttached)
{
    Thread.Sleep(TimeSpan.FromSeconds(5));
}
#endif

UIApplication.Main(args, null, typeof(AppDelegate));