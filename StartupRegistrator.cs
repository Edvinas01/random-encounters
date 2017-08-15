using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace RandomEncounters
{
    public static class StartupRegistrator
    {
        public static void Setup(bool remove = false)
        {
            var regKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (regKey == null)
            {
                return;
            }

            var dirPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" +
                          Constants.AppName;

            var dirExists = Directory.Exists(dirPath);

            if (remove && dirExists)
            {
                regKey.DeleteValue(Constants.AppName);
                Directory.Delete(dirPath, true);
            }
            else if (!remove)
            {
                var execPath = dirPath + @"\" + Constants.AppName + ".exe";
                if (Application.ExecutablePath == execPath)
                {
                    return;
                }

                if (!dirExists)
                {
                    Directory.CreateDirectory(dirPath);
                }

                File.Copy(Application.ExecutablePath, execPath, true);
                regKey.SetValue(Constants.AppName, execPath);
            }
        }
    }
}