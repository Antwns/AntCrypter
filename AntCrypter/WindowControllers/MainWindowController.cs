
namespace AntCrypter.WindowControllers
{
    class MainWindowController
    {
        public static Windows.InstallerWindow InstallerMainWindow = new Windows.InstallerWindow();
        public static MainWindow CrypterMainWindow = new MainWindow();
        static void Start(string[] args)
        {
            CrypterMainWindow.Show();
        }
    }
}
