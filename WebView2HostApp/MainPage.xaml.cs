using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WebView2HostApp;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
    }

    private void WebView2_CoreWebView2Initialized(WebView2 sender, CoreWebView2InitializedEventArgs args)
    {
        if (args.Exception != null)
        {
            // We hit an issue with WebView2 load
            // TODO: Handle this scenario
            Debugger.Break();
        }
    }

    private void WebView2_NavigationCompleted(WebView2 sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs args)
    {
        // TODO: Use XAML Binding
        // Note: This ensures we don't display an empty WebView until our page content has loaded.
        WebView2.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
    }
}
