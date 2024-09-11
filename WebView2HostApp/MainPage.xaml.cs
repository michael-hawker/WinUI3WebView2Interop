using AddHostObjectBridgeComponent;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WebView2HostApp;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainPage : Page
{
    private Bridge _bridge;

    public MainPage()
    {
        _bridge = new Bridge();
        _bridge.ItemsChangedEvent += UpdateItemsList;

        InitializeComponent();

        InitializeWebView2Async();
    }

    private async void InitializeWebView2Async()
    {
        await WebView2.EnsureCoreWebView2Async();

        WebView2.CoreWebView2.Settings.AreHostObjectsAllowed = true;
        WebView2.CoreWebView2.SetVirtualHostNameToFolderMapping("appassets.html.example", "html", CoreWebView2HostResourceAccessKind.Allow);
        WebView2.Source = new Uri("http://appassets.html.example/add_host_object.html");

        var dispatchAdapter = new WinRTAdapter.DispatchAdapter();
        WebView2.CoreWebView2.AddHostObjectToScript("BridgeInstance", dispatchAdapter.WrapObject(_bridge, dispatchAdapter));
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

    private void UpdateItemsList(object sender, IList<string> items)
    {
        ItemsListTextBox.Text = string.Join("\n", items);
    }

    private void ItemButton_Click(object sender, RoutedEventArgs e)
    {
        var text = (sender as Button).Content as string;
        _bridge.AppendToItems(text);
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        _bridge.ClearItems();
    }
}
