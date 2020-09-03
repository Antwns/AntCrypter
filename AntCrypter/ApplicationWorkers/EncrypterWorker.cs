using AntCrypter.Windows;
using Microsoft.Win32;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace AntCrypter.ApplicationWorkers
{
    class EncrypterWorker
    {
        public static byte[] EncryptedContent;
        public static void DoEncryption(bool PayloadMode, bool InstallerMode, bool OpenURLOnStartupMode, bool CustomIconMode)
        {
            OpenFileDialog CryptFile = new OpenFileDialog();
            if (CryptFile.ShowDialog() == true)
            {
                if (MessageBox.Show("Are you sure you want to ecrypt " + CryptFile.FileName + "? You can decrypt it later if you need to" + Environment.NewLine + "Make sure that you remember your encryption key or your files will be GONE!", "Attention!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (PayloadMode == false && InstallerMode == false && OpenURLOnStartupMode == false && CustomIconMode == false)
                        {
                            string password = @DataAndResources.KeysAndData.EKey;
                            UnicodeEncoding UE = new UnicodeEncoding();
                            byte[] key = UE.GetBytes(password);

                            string CryptFileDir = CryptFile.FileName;
                            FileStream CryptFileStream = new FileStream(CryptFileDir + ".AntCrypted", FileMode.Create);
                            RijndaelManaged RMCrypto = new RijndaelManaged();

                            CryptoStream CryptoStream = new CryptoStream(CryptFileStream, RMCrypto.CreateEncryptor(key, key), CryptoStreamMode.Write);

                            FileStream FileOpenStream = new FileStream(CryptFileDir, FileMode.Open);

                            int data;
                            while ((data = FileOpenStream.ReadByte()) != -1)
                            {
                                CryptoStream.WriteByte((byte)data);
                            }

                            FileOpenStream.Close();
                            CryptoStream.Close();
                            CryptFileStream.Close();

                            File.Delete(CryptFile.FileName);

                            MessageBox.Show("File was ecrypted successfully!", "Information!", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            string password = @DataAndResources.KeysAndData.EKey;
                            UnicodeEncoding UE = new UnicodeEncoding();
                            byte[] key = UE.GetBytes(password);

                            string CryptFileDir = CryptFile.FileName;
                            FileStream CryptFileStream = new FileStream(CryptFileDir + ".AntCrypted", FileMode.Create);
                            RijndaelManaged RMCrypto = new RijndaelManaged();

                            CryptoStream CryptoStream = new CryptoStream(CryptFileStream, RMCrypto.CreateEncryptor(key, key), CryptoStreamMode.Write);

                            FileStream FileOpenStream = new FileStream(CryptFileDir, FileMode.Open);

                            int data;
                            while ((data = FileOpenStream.ReadByte()) != -1)
                            {
                                CryptoStream.WriteByte((byte)data);
                            }

                            FileOpenStream.Close();
                            CryptoStream.Close();
                            CryptFileStream.Close();

                            EncryptedContent = File.ReadAllBytes(CryptFileDir + ".AntCrypted");

                            File.Delete(CryptFileDir + ".AntCrypted");

                            MessageBox.Show("File was ecrypted successfully! Payload mode was enabled please wait for the payload confirmation message!", "Information!", MessageBoxButton.OK, MessageBoxImage.Information);

                            PayloadWorker.Payload(EncryptedContent, InstallerMode, InstallerWorker.RawDirectory, OpenURLOnStartupMode, URLOnStartupWorker.StartupURI, CustomIconMode, CustomIconWorker.CustomIconDirectory ,DataAndResources.KeysAndData.EKey, InstallerWorker.AppName, InstallerWindow.RunOnStartupSelected);
                        }
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Encryption failed, have you used a valid RijndaelManaged encryption key?Erro info: " + exc, "Warning!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
