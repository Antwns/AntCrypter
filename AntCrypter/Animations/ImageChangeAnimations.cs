using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace AntCrypter.Animations
{
    class ImageChangeAnimations
    {
        public static void SelectedItemAnimation(Image CurrentControl)
        {
            string OldTooltip = CurrentControl.ToolTip.ToString();
            string NewToolTip = OldTooltip.Replace("Disabled", "Enabled");
            CurrentControl.ToolTip = NewToolTip;
            string NewResource = CurrentControl.Source.ToString().Replace(".png","Ready.png");
            CurrentControl.Source = new BitmapImage(new Uri(NewResource));
        }

        public static void DeselectedItemAnimation(Image CurrentControl)
        {
            string OldToolTip = CurrentControl.ToolTip.ToString();
            string NewToolTip = OldToolTip.Replace("Enabled", "Disabled");
            CurrentControl.ToolTip = NewToolTip;
            string OldResource = CurrentControl.Source.ToString().Replace("Ready.png", ".png");
            CurrentControl.Source = new BitmapImage(new Uri(OldResource));
        }
    }
}
