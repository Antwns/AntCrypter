using System;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace AntCrypter.Animations
{
    class MouseClickAnimations
    {
        public static bool Enabled;

        public static void MouseCLickAnimation(Label CurrentControl)
        {
            DoubleAnimation FirstAnimation = new DoubleAnimation(CurrentControl.Width, CurrentControl.Width + 20, TimeSpan.FromMilliseconds(200));
            if (Enabled == false)
            {
                FirstAnimation.AutoReverse = true;
                FirstAnimation.RepeatBehavior = RepeatBehavior.Forever;
                CurrentControl.BeginAnimation(Label.WidthProperty, FirstAnimation);
                Enabled = true;
            }
            else
            {
                CurrentControl.BeginAnimation(Label.WidthProperty, null);
                Enabled = false;
            }
            
        }
    }
}
