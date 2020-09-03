using System;
using System.Windows;
using System.Windows.Controls;

namespace AntCrypter.ApplicationWorkers
{
    class InstallerWorker
    {
        public static string InstallDirectory;
        public static string AppFolder;
        public static string AppName;
        public static bool FoundAppFolder;
        public static bool FoundAppName;
        public static bool VerifiedAppName;
        public static bool InstallerOk;
        public static string RawDirectory;
        public static void InstallApplication(TextBox CurrentControl)
        {
            InstallDirectory = CurrentControl.Text;
            AppFolder = GetElement(InstallDirectory, "%!", "!%");
            AppName = GetElement(InstallDirectory, "%?", "?%");
            if(AppFolder != "")
            {
                FoundAppFolder = true;
            }
            else
            {
                FoundAppFolder = false;
            }
            if(AppName != "")
            {
                FoundAppName = true;
            }
            else
            {
                FoundAppName = false;
            }
            if (AppName.Contains(".exe") || AppName.Contains(".EXE"))
            {
                VerifiedAppName = true;
            }
            else
            {
                VerifiedAppName = false;
            }
            if(FoundAppFolder == true && FoundAppName == true && VerifiedAppName == true)
            {
                InstallDirectory = InstallDirectory.Replace("%!", "");
                InstallDirectory = InstallDirectory.Replace("!%", "");
                InstallDirectory = InstallDirectory.Replace("%%", "");
                InstallDirectory = InstallDirectory.Replace("%?", "");
                InstallDirectory = InstallDirectory.Replace("?%", "");
                InstallerOk = true;
                RawDirectory = InstallDirectory.Replace(AppName, "");
                MessageBox.Show("Path: " + InstallDirectory + " is valid!" + Environment.NewLine + "Directory: "+ RawDirectory + Environment.NewLine + "File name: " + AppName, "Information!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Path: " + InstallDirectory + " is invalid!, have you entered the correct string format with the right keywords?", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                InstallerOk = false;
            }
        }

        public static string GetElement(string SourceString, string Start, string End)
        {
            if (SourceString.Contains(Start) && SourceString.Contains(End))
            {
                int StartPos, EndPos;
                StartPos = SourceString.IndexOf(Start, 0) + Start.Length;
                EndPos = SourceString.IndexOf(End, StartPos);
                return SourceString.Substring(StartPos, EndPos - StartPos);
            }
            return "";
        }
    }
}
