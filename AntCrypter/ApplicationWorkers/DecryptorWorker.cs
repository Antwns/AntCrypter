using Microsoft.Win32;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace AntCrypter.ApplicationWorkers
{
    class DecryptorWorker
    {
        public static void DoDecryption()
        {
            OpenFileDialog DecryptFile = new OpenFileDialog()
            {
                Filter = "AntCryped files(*.AntCrypted)|*.AntCrypted"
            };
            if (DecryptFile.ShowDialog() == true)
            {
                DecryptFile.FilterIndex = 1;
                DecryptFile.DefaultExt = "AntCrypted";
                if (DecryptFile.FileName.EndsWith(".AntCrypted"))
                {
                    try
                    {
                        string password = @DataAndResources.KeysAndData.EKey; // Your Key Here

                        UnicodeEncoding UE = new UnicodeEncoding();
                        byte[] key = UE.GetBytes(password);

                        FileStream fsCrypt = new FileStream(DecryptFile.FileName, FileMode.Open);

                        RijndaelManaged RMCrypto = new RijndaelManaged();

                        CryptoStream cs = new CryptoStream(fsCrypt, RMCrypto.CreateDecryptor(key, key), CryptoStreamMode.Read);

                        FileStream fsOut = new FileStream(DecryptFile.FileName.Replace(".AntCrypted", ""), FileMode.Create);

                        int data;
                        while ((data = cs.ReadByte()) != -1)
                        {
                            fsOut.WriteByte((byte)data);
                        }

                        fsOut.Close();
                        cs.Close();
                        fsCrypt.Close();

                        File.Delete(DecryptFile.FileName);

                        MessageBox.Show("The File has been successfully decrypted!", "Attention!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Encryption failed, have you used a valid RijndaelManaged decryption key?Erro info: " + exc, "Warning!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("The chosen file is not encrypted by AntTools therefore it cannot be decrypted by this program!", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
