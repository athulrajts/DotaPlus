using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace DotaPlus.Controls
{
    public sealed partial class WebVideoPlayer : UserControl
    {
        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(string), typeof(WebVideoPlayer), new PropertyMetadata(0));

        public WebVideoPlayer()
        {
            this.InitializeComponent();
        }

        public Uri GetUri(string source) => new(source);

        private async void WebView2_NavigationCompleted(WebView2 sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs args)
        {
            await sender.EnsureCoreWebView2Async();
            await sender.CoreWebView2.ExecuteScriptAsync(@"
                video = document.getElementsByName(""media"")[0];
                video.controls = null;
                video.loop = true;
                video.play()
            ");
        }
    }
}
