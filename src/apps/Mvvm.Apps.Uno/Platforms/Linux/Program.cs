using Windows.UI.Core;
#if __UNO5__
using Uno.UI.Runtime.Skia.Linux.FrameBuffer;
#else
using Uno.UI.Runtime.Skia;
#endif

namespace Mvvm.Apps.Skia.Linux.Framebuffer;

public class Program
{
	public static void Main(string[] args)
	{
		try
		{
			Console.CursorVisible = false;

			var host = new FrameBufferHost(() =>
			{
				if (CoreWindow.GetForCurrentThread() is { } window)
				{
					// Framebuffer applications don't have a WindowManager to rely
					// on. To close the application, we can hook onto CoreWindow events
					// which dispatch keyboard input, and close the application as a result.
					// This block can be moved to App.xaml.cs if it does not interfere with other
					// platforms that may use the same keys.
					window.KeyDown += (s, e) =>
					{
						if (e.VirtualKey == Windows.System.VirtualKey.F12)
						{
							Application.Current.Exit();
						}
					};
				}

				return new App();
			});
			host.Run();
		}
		finally
		{
			Console.CursorVisible = true;
		}
	}
}