using AntCrypter.Animations;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows;

namespace AntCrypter.ApplicationWorkers
{
    class CustomIconWorker
    {
        public static string CustomIconDirectory;
        public static void LoadCustomIcon(System.Windows.Controls.Image CurrentControl)
        {
            OpenFileDialog CustomIconLoader = new OpenFileDialog()
            {
                Filter = "Icon files(*.ico)|*.ico"
            };
            if (CustomIconLoader.ShowDialog() == true)
            {
                if(CustomIconLoader.FileName != "")
                {
                    try
                    {
                        CustomIconDirectory = CustomIconLoader.FileName;
                        ImageChangeAnimations.SelectedItemAnimation(CurrentControl);
                        MessageBox.Show("Valid custom icon selected!", "Information!", MessageBoxButton.OK, MessageBoxImage.Information);
                        MainWindow.CustomIconMode = true;
                    }
                    catch
                    {
                        MainWindow.CustomIconMode = false;
                        ImageChangeAnimations.DeselectedItemAnimation(CurrentControl);
                        MessageBox.Show("You did not select a valid icon! The process will be canceled.", "Warning!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            else
            {
                if (CustomIconLoader.FileName == "")
                {
                    MainWindow.CustomIconMode = false;
                    ImageChangeAnimations.DeselectedItemAnimation(CurrentControl);
                    MessageBox.Show("You did not select a valid icon! The process will be canceled.", "Warning!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
