using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MarkSetBot_ToolKit.AppShell), typeof(MarkSetBot_ToolKit.iOS.Renderers.CustomShellRenderer))]
namespace MarkSetBot_ToolKit.iOS.Renderers
{
    public class CustomShellRenderer : ShellRenderer
    {
        protected override IShellSectionRenderer CreateShellSectionRenderer(ShellSection shellSection)
        {
            var renderer = base.CreateShellSectionRenderer(shellSection);

            if (renderer != null)
            {
                if (renderer is ShellSectionRenderer shellRenderer)
                {
                    var appearance = new UINavigationBarAppearance();
                    appearance.ConfigureWithOpaqueBackground();
                    appearance.BackgroundColor = UIColor.Clear.FromHex(0x82C91E);
                    
                    appearance.TitleTextAttributes = new UIStringAttributes() { ForegroundColor = UIColor.White };

                    shellRenderer.NavigationBar.Translucent = false;
                    shellRenderer.NavigationBar.StandardAppearance = appearance;
                    shellRenderer.NavigationBar.ScrollEdgeAppearance = shellRenderer.NavigationBar.StandardAppearance;
                }
            }

            return renderer;
        }

        protected override IShellItemRenderer CreateShellItemRenderer(ShellItem item)
        {
            var renderer = base.CreateShellItemRenderer(item);

            if (renderer != null)
            {
                if (renderer is ShellItemRenderer shellItemRenderer)
                {
                    var appearance = new UITabBarAppearance();

                    appearance.ConfigureWithOpaqueBackground();
                    appearance.BackgroundColor = UIColor.Clear.FromHex(0x82C91E);
                    shellItemRenderer.TabBar.Translucent = false;
                    shellItemRenderer.TabBar.StandardAppearance = appearance;
                }
            }
            return renderer;
        }

    }

    public static class UIColorExtensions
    {
        public static UIColor FromHex(this UIColor color, int hexValue)
        {
            return UIColor.FromRGB(
                (((float)((hexValue & 0xFF0000) >> 16)) / 255.0f),
                (((float)((hexValue & 0xFF00) >> 8)) / 255.0f),
                (((float)(hexValue & 0xFF)) / 255.0f)
            );
        }
    }
}
