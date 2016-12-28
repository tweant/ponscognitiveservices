using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PonsCognitiveServices.Helpers
{
    public class WebBrowserUtility
    {
        // "HtmlString" attached property for a WebView
        public static readonly DependencyProperty HtmlStringProperty =
           DependencyProperty.RegisterAttached("HtmlString", typeof(string), typeof(WebBrowserUtility), new PropertyMetadata("", OnHtmlStringChanged));

        // Getter and Setter
        public static string GetHtmlString(DependencyObject obj) { return (string)obj.GetValue(HtmlStringProperty); }
        public static void SetHtmlString(DependencyObject obj, string value) { obj.SetValue(HtmlStringProperty, value); }

        // Handler for property changes in the DataContext : set the WebView
        private static void OnHtmlStringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WebView wv = d as WebView;
            if (wv != null)
            {
                wv.NavigateToString((string)e.NewValue);
            }
        }

//         <WebView local:MyProperties.HtmlString="{Binding CurrentHtmlString}"></WebView>
//Normally you already have the following line at the top of your XAML file :

//xmlns:local="using:MYNAMESPACE"
//You can find more explanation here from Rob Caplan : http://blogs.msdn.com/b/wsdevsol/archive/2013/09/26/binding-html-to-a-webview-with-attached-properties.aspx
    }
}
