using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace AntCrypter.Animations
{
    class VisibilityAnimations
    {
        public static void ShowItemAnimation(Image CurrentControl)
        {
            DoubleAnimation ShowAnimation = new DoubleAnimation(0, 50, TimeSpan.FromMilliseconds(400));
            CurrentControl.Visibility = Visibility.Visible;
            CurrentControl.BeginAnimation(Image.WidthProperty, ShowAnimation);
            CurrentControl.BeginAnimation(Image.HeightProperty, ShowAnimation);
        }

        internal static void HideItemsAnimation(Image CurrentControl)
        {
            DoubleAnimation ShowAnimation = new DoubleAnimation(50, 0, TimeSpan.FromMilliseconds(400));
            CurrentControl.Visibility = Visibility.Visible;
            CurrentControl.BeginAnimation(Image.WidthProperty, ShowAnimation);
            CurrentControl.BeginAnimation(Image.HeightProperty, ShowAnimation);
        }
    }
}
