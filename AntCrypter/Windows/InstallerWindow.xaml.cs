using System;
using System.Windows;
using System.Windows.Input;

namespace AntCrypter.Windows
{
    /// <summary>
    /// Interaction logic for InstallerWindow.xaml
    /// </summary>
    public partial class InstallerWindow : Window
    {
        public InstallerWindow()
        {
            InitializeComponent();
        }
        public static bool WindowOpened;
        public static bool RunOnStartupSelected;

        private void Install_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                ApplicationWorkers.InstallerWorker.InstallApplication(InstallDirectoryTextBox);
            }
        }

        private void InstallerHelpIcon_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.MouseHoverAnimations.MouseEnterAnimationHop(InstallerHelpIcon);
        }

        private void InstallerHelpIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("'%%USERNAME%%' is a placeholder for the machine's default user, don't change this unless you plan on installing the payload to a different user on the same machine. The username must be included in between the '%%' and '%%' characters." + Environment.NewLine + "%!APPFOLDER!% represents the folder the application will be placed into. If the folder does not exist, it will be created, the folder's name must be placed between '%!' and '!%'." + Environment.NewLine + "'%?APPNAME.EXE?%' represents the installed application's name. The name must be entered between the '%?' and '?%' characters respectively.", "Information!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void InstallOnStartupButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.MouseHoverAnimations.MouseEnterAnimationHop(InstallOnStartupButton);
        }

        private void InstallerWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ApplicationWorkers.InstallerWorker.InstallerOk == false)
            {
                if (MessageBox.Show("You did not validate the directory!, Press enter whlie the textbox is active to check the validity of the desired directory, if you click no the process will be canceled", "Error!", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.No)
                {
                    e.Cancel = true;
                    this.Hide();
                    Animations.ImageChangeAnimations.DeselectedItemAnimation(InstallOnStartupButton);
                    Animations.ImageChangeAnimations.DeselectedItemAnimation(WindowControllers.MainWindowController.CrypterMainWindow.InstallerModeButton);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void InstallerWindow1_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (WindowOpened == false)
            {
                Animations.StartupAnimations.InstallerOpening();
                WindowOpened = true;
            }
            else
            {
                Animations.StartupAnimations.InstallerClosing();
                WindowOpened = false;
            }
        }

        private void InstallOnStartupButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (RunOnStartupSelected == false)
            {
                Animations.ImageChangeAnimations.SelectedItemAnimation(InstallOnStartupButton);
                RunOnStartupSelected = true;
            }
            else
            {
                Animations.ImageChangeAnimations.DeselectedItemAnimation(InstallOnStartupButton);
                RunOnStartupSelected = false;
            }
        }
    }
}
