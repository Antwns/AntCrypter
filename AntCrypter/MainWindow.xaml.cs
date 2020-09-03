using System;
using System.Windows;
using System.Windows.Input;

namespace AntCrypter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        #region variable declarations

        public static bool PayloadMode;
        public static bool InstallerMode;
        public static bool MoreOptionsShown;
        public static bool OpenURLOnStartupMode;
        public static bool CustomIconMode;

        #endregion

        #region Mouse hover events and animations

        private void EncryptButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.MouseHoverAnimations.MouseEnterAnimation(EncryptButton, null);
        }
        private void DecryptButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.MouseHoverAnimations.MouseEnterAnimation(DecryptButton, null);
        }

        private void OpenURLOnStartupButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.MouseHoverAnimations.MouseEnterAnimationHop(OpenURLOnStartupButton);
        }

        private void SetCustomIconButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.MouseHoverAnimations.MouseEnterAnimationHop(SetCustomIconButton);
        }

        private void InstallerModeButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.MouseHoverAnimations.MouseEnterAnimationHop(InstallerModeButton);
        }

        private void PayloadModeButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.MouseHoverAnimations.MouseEnterAnimationHop(PayloadModeButton);
        }

        #endregion

        #region Mouse button pressings
        private void EncryptButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataAndResources.KeysAndData.EKey = EKeyTextBox.Text;
            ApplicationWorkers.EncrypterWorker.DoEncryption(PayloadMode,InstallerMode,OpenURLOnStartupMode,CustomIconMode);
        }

        private void DecryptButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataAndResources.KeysAndData.EKey = EKeyTextBox.Text;
            ApplicationWorkers.DecryptorWorker.DoDecryption();
        }

        private void MoreOptionsLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Animations.MouseHoverAnimations.MouseEnterAnimation(null, MoreOptionsLabel);
            if (MoreOptionsShown == false)
            {
                Animations.MouseClickAnimations.MouseCLickAnimation(MoreOptionsLabel);
                Animations.VisibilityAnimations.ShowItemAnimation(OpenURLOnStartupButton);
                Animations.VisibilityAnimations.ShowItemAnimation(PayloadModeButton);
                Animations.VisibilityAnimations.ShowItemAnimation(InstallerModeButton);
                Animations.VisibilityAnimations.ShowItemAnimation(SetCustomIconButton);
                MoreOptionsShown = true;
            }
            else
            {
                Animations.MouseClickAnimations.MouseCLickAnimation(MoreOptionsLabel);
                Animations.VisibilityAnimations.HideItemsAnimation(OpenURLOnStartupButton);
                Animations.VisibilityAnimations.HideItemsAnimation(PayloadModeButton);
                Animations.VisibilityAnimations.HideItemsAnimation(InstallerModeButton);
                Animations.VisibilityAnimations.HideItemsAnimation(SetCustomIconButton);
                MoreOptionsShown = false;
            }
        }

        private void PayloadModeButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (PayloadMode == false)
            {
                Animations.ImageChangeAnimations.SelectedItemAnimation(PayloadModeButton);
                PayloadMode = true;
            }
            else if(PayloadMode == true && InstallerMode == false && OpenURLOnStartupMode == false && CustomIconMode == false)
            {
                Animations.ImageChangeAnimations.DeselectedItemAnimation(PayloadModeButton);
                PayloadMode = false;
            }
            else
            {
                MessageBox.Show("Disable all other features before disabling payload mode", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InstallerModeButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (InstallerMode == false && PayloadMode == true)
            {
                WindowControllers.MainWindowController.InstallerMainWindow.Show();
                Animations.ImageChangeAnimations.SelectedItemAnimation(InstallerModeButton);
                InstallerMode = true;
            }
            else if (InstallerMode == true && PayloadMode == true)
            {
                Animations.ImageChangeAnimations.DeselectedItemAnimation(InstallerModeButton);
                InstallerMode = false;
                Windows.InstallerWindow.RunOnStartupSelected = false;
                Animations.ImageChangeAnimations.DeselectedItemAnimation(WindowControllers.MainWindowController.InstallerMainWindow.InstallOnStartupButton);
                MessageBox.Show("Startup info has been cleared successfully", "Information!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Payload mode needs to be enabled in order to initiate the installer...","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
        private void SetCustomIconButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (PayloadMode == false && CustomIconMode == false)
            {
                MessageBox.Show("Payload mode needs to be enabled in order to use a custom icon...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (PayloadMode == true && CustomIconMode == false)
            {
                ApplicationWorkers.CustomIconWorker.LoadCustomIcon(SetCustomIconButton);
            }
            else
            {
                CustomIconMode = false;
                ApplicationWorkers.CustomIconWorker.CustomIconDirectory = null;
                Animations.ImageChangeAnimations.DeselectedItemAnimation(SetCustomIconButton);
                MessageBox.Show("Custom icon has been cleared", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void OpenURLOnStartupButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (OpenURLOnStartupMode == false && PayloadMode ==true )
            {
                ApplicationWorkers.URLOnStartupWorker.LoadCustomURL(OpenURLOnStartupButton);
            }
            else if (OpenURLOnStartupMode == true && PayloadMode == true)
            {
                ApplicationWorkers.URLOnStartupWorker.StartupURI = null;
                MessageBox.Show("Startup Url cleared.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                Animations.ImageChangeAnimations.DeselectedItemAnimation(OpenURLOnStartupButton);
                OpenURLOnStartupMode = false;
            }
            else
            {
                MessageBox.Show("Payload mode needs to be enabled in order to use the startup URL feature...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                OpenURLOnStartupMode = false;
            }
        }

        private void BlockingImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Animations.StartupAnimations.RectangleAnimator(BlockingImage);
        }

        #endregion

        #region Loaded tags
        private void AntCrypterMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            BlockingImage.Visibility = Visibility.Visible;
            if (MessageBox.Show("                                   Terms of use:" + Environment.NewLine + "The creator of this program does not take any resonsiblity for damages caused to any system/computer/network/electronic device this software may cause, use this software at your own risk for personal use ONLY!!!", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.No)
            {
                this.Close();
                Application.Current.Shutdown();
            }
        }

        private void EKeyTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            Animations.StartupAnimations.TextBoxAnimator(EKeyTextBox);
        }

        #endregion

        private void AntCrypterMainWindow_Closed(object sender, EventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }
    }
}
