using System;
using System.Windows.Forms;

namespace RandomEncounters
{
    public class Launcher
    {
        public static void Main(string[] args)
        {
            StartupRegistrator.Setup();

            var random = new Random((int) DateTimeOffset.Now.ToUnixTimeMilliseconds());
            var form = new MainForm(new ImageProvider(random), random);

            Application.EnableVisualStyles();
            Application.Run(form);
        }
    }
}