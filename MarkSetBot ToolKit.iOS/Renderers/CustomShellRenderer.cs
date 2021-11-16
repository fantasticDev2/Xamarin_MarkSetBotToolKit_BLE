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
                    //appearance.BackgroundColor = new UIColor(red: 0.86f, green: 0.24f, blue: 0.00f, alpha: 1.00f);
                    //appearance.TitleTextAttributes = new UIStringAttributes() { ForegroundColor = UIColor.White };

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

                    shellItemRenderer.TabBar.Translucent = false;
                    shellItemRenderer.TabBar.StandardAppearance = appearance;
                }
            }
            return renderer;
        }

    }
}
