using System;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace AntCrypter.Animations
{
    class StartupAnimations
    {
        public static Stream StartupPlayerStream = Properties.Resources.Intro;
        public static SoundPlayer StartupPlayer = new SoundPlayer(StartupPlayerStream);
        public static bool FirstCLick = true;
        public static Stream InstallerSoundStream = Properties.Resources.WarioFalling;
        public static SoundPlayer InstallerSoundPlayer = new SoundPlayer(InstallerSoundStream);
        public static Stream InstallerSoundClosingStream = Properties.Resources.WarioOhBoy;
        public static SoundPlayer InstallerSoundClosingPlayer = new SoundPlayer(InstallerSoundClosingStream);

        internal static void RectangleAnimator(System.Windows.Shapes.Rectangle CurrentControl)
        {
            if (FirstCLick == true)
            {
                StartupPlayer.Play();
                DoubleAnimation StartupAnimation = new DoubleAnimation(1.0, 0.0, TimeSpan.FromSeconds(9.5));
                StartupAnimation.Completed += new EventHandler((sender, e) => StartupAnimation_Completed(sender, e, CurrentControl));
                CurrentControl.BeginAnimation(System.Windows.Shapes.Rectangle.OpacityProperty, StartupAnimation);
                FirstCLick= false;
            }
        }
        private static void StartupAnimation_Completed(object sender, EventArgs e, System.Windows.Shapes.Rectangle CurrentControl)
        {
            CurrentControl.Visibility = Visibility.Hidden;
        }

        internal static void TextBoxAnimator(TextBox CurrentControl)
        {
            CurrentControl.BorderBrush = new SolidColorBrush(Colors.Black);
            ColorAnimation StartupAnimation = new ColorAnimation(Color.FromRgb(200, 0, 50), Color.FromRgb(50, 0, 200), TimeSpan.FromSeconds(1));
            StartupAnimation.AutoReverse = true;
            StartupAnimation.RepeatBehavior = RepeatBehavior.Forever;
            CurrentControl.BorderBrush.BeginAnimation(SolidColorBrush.ColorProperty, StartupAnimation);
        }
        public static void InstallerOpening()
        {
            Thread InstallerMusicThread = new Thread(InstallerMusicThreadWork);
            InstallerMusicThread.Start();
        }

        public static void InstallerClosing()
        {
            Thread InstallerMusicClosingThread = new Thread(InstallerMusicClosingThreadWork);
            InstallerMusicClosingThread.Start();

        }
        private static void InstallerMusicThreadWork()
        {
            InstallerSoundPlayer.Play();
        }

        private static void InstallerMusicClosingThreadWork()
        {
            InstallerSoundClosingPlayer.Play();
        }
    }
}
