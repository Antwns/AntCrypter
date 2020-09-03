using System;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace AntCrypter.Animations
{
    class MouseHoverAnimations
    {
        public static System.IO.Stream EntryPlayerStream = Properties.Resources.Light;
        public static System.IO.Stream LeavePlayerStream = Properties.Resources.Heavy;
        public static SoundPlayer MouseEntryPlayer = new SoundPlayer(EntryPlayerStream);
        public static SoundPlayer MouseLeavePlayer = new SoundPlayer(LeavePlayerStream);

        public static bool ReadyForNewAnimation = true;

        #region Casual mouse entry
        public static void MouseEnterAnimation(Image CurrentImage, Label CurrentLabel)
        {
            MouseEntryPlayer.Play();
            if (CurrentImage != null && CurrentLabel == null)
            {
                DoubleAnimation EntryAnimation = new DoubleAnimation(80, 100, TimeSpan.FromMilliseconds(120));

                EntryAnimation.Completed += EntryAnimation_Completed;

                EntryAnimation.AutoReverse = true;

                CurrentImage.BeginAnimation(Image.WidthProperty, EntryAnimation);
                CurrentImage.BeginAnimation(Image.HeightProperty, EntryAnimation);
            }
            else
            {
                DoubleAnimation EntryAnimation = new DoubleAnimation(5, 0, TimeSpan.FromMilliseconds(120));

                EntryAnimation.Completed += EntryAnimation_Completed;

                EntryAnimation.AutoReverse = true;

                CurrentLabel.Effect.BeginAnimation(DropShadowEffect.BlurRadiusProperty, EntryAnimation);
                CurrentLabel.Effect.BeginAnimation(DropShadowEffect.ShadowDepthProperty, EntryAnimation);
            }

        }

        private static void EntryAnimation_Completed(object sender, EventArgs e)
        {
            MouseLeavePlayer.Play();
        }

        #endregion

        #region fancy mouse entry

        public static void MouseEnterAnimationHop(Image CurrentControl)
        {

            double LeftMargin = CurrentControl.Margin.Left;
            double RightMargin = CurrentControl.Margin.Right;
            double TopMargin = CurrentControl.Margin.Right;
            double BottomMargin = CurrentControl.Margin.Bottom;

            Thickness OldThickness = new Thickness(LeftMargin, TopMargin, RightMargin, BottomMargin);
            Thickness NewThickness = new Thickness(LeftMargin, TopMargin, RightMargin, BottomMargin + 5);

            ThicknessAnimation EntryAnimationHop = new ThicknessAnimation(OldThickness, NewThickness, TimeSpan.FromMilliseconds(100));
            if (ReadyForNewAnimation == true)
            {
                ReadyForNewAnimation = false;
                MouseEntryPlayer.Play();

                EntryAnimationHop.AutoReverse = true;
                EntryAnimationHop.Completed += EntryAnimationHop_Completed;

                CurrentControl.BeginAnimation(Image.MarginProperty, EntryAnimationHop);
            }
            else
            {
                CurrentControl.BeginAnimation(Image.MarginProperty, null);
            }
        }

        private static void EntryAnimationHop_Completed(object sender, EventArgs e)
        {
            ReadyForNewAnimation = true;
            MouseLeavePlayer.Play();
        }

        #endregion
    }
}
