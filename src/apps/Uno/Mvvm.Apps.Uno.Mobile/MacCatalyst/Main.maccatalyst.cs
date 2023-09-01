using Mvvm.Apps.Uno;
using UIKit;

#if DEBUG
if (System.Diagnostics.Debugger.IsAttached)
{
    Thread.Sleep(TimeSpan.FromSeconds(5));
}
#endif

UIApplication.Main(args, null, typeof(AppHead));
