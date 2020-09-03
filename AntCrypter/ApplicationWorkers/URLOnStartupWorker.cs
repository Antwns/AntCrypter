using AntCrypter.Animations;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AntCrypter.ApplicationWorkers
{
    class URLOnStartupWorker
    {
        public static string StartupURI;
        public static void LoadCustomURL(Image CurrentControl)
        {
            OpenFileDialog CustomUrlLoader = new OpenFileDialog()
            {
                Filter = "url files(*.url)|*.url"
            };
            if (CustomUrlLoader.ShowDialog() == true)
            {
                if (CustomUrlLoader.FileName != "" )
                {
                    Uri StartupURL = new Uri(CustomUrlLoader.FileName);
                    if (StartupURL.IsFile)
                    {
                        string URILine = File.ReadLines(StartupURL.LocalPath).Skip(4).Take(1).First();
                        StartupURI = URILine.Replace("URL=", "");
                        StartupURI = StartupURI.Replace("\"", "");
                        StartupURI = StartupURI.Replace("BASE", "");
                        ImageChangeAnimations.SelectedItemAnimation(CurrentControl);
                        CurrentControl.ToolTip = "Enabled: Open URL on application startup.";
                        MessageBox.Show("Valid URL selected!" + Environment.NewLine + StartupURI, "Information!", MessageBoxButton.OK, MessageBoxImage.Information);
                        MainWindow.OpenURLOnStartupMode = true;
                    }
                    else
                    {
                        ImageChangeAnimations.DeselectedItemAnimation(CurrentControl);
                        CurrentControl.ToolTip = "Disabled: Open URL on application startup.";
                        MessageBox.Show("You did not select a valid URL! The process will be canceled.", "Warning!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            else
            {
                if (CustomUrlLoader.FileName == "")
                {
                    ImageChangeAnimations.DeselectedItemAnimation(CurrentControl);
                    CurrentControl.ToolTip = "Disabled: Open URL on application startup.";
                    MessageBox.Show("You did not select a valid URL! The process will be canceled.", "Warning!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
