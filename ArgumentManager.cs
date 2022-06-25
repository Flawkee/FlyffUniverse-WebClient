using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;


namespace FlyffUniverse_WebClient
{
    public sealed class ArgumentManager
    {
        private static ArgumentManager _instance;
        public static string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string profilePath = appData + @"\FlyffUniverse\DefaultProfile";
        private string[] userArgs;
        FlyffWCForm mainForm = FlyffWCForm.Instance;

        // conditions
        bool profileCheck = false;
        bool resCheck = false;
        bool fsCheck = false;
        bool pixelCheck = false;
        bool disCheck = false;

        private ArgumentManager()
        {
           
        }
        public static ArgumentManager Instance { get { if (_instance == null) { _instance = new ArgumentManager(); } return _instance; } } 

        public void InitializeArguments()
        {
            userArgs = Environment.GetCommandLineArgs();
            foreach (string arg in userArgs)
            {
                if (!profileCheck) { ProfileArg(arg); }
                if (!resCheck) { ResolutionArg(arg); }
                if (!fsCheck) { FullscreenArg(arg); }
                if (!pixelCheck) { PixelLocationArg(arg); }
                if (!disCheck) { DisplaySelectionArg(arg); }
            }
        }
        private void ProfileArg(string arg)
        {
            string rg = @"/ProfileName=(.+)";
            GroupCollection gc = RegexCheck.Test(arg, rg);
            if (gc != null) { profilePath = appData + @"\FlyffUniverse\" + gc[1].Value; profileCheck = true; }
        }
        private void ResolutionArg(string arg)
        {
            string rg = @"/Resolution=([0-9]{1,4})x([0-9]{1,4})";
            GroupCollection gc = RegexCheck.Test(arg, rg);
            if (gc != null) {
                int width, height;
                Int32.TryParse(gc[1].Value, out width);
                Int32.TryParse(gc[2].Value, out height);
                mainForm.Size = new Size(width, height);
                resCheck = true;
            }
        }
        private void FullscreenArg(string arg)
        {
            string rg = @"(/Fullscreen)";
            GroupCollection gc = RegexCheck.Test(arg, rg);
            if (gc != null)
            {
                mainForm.WindowState = FormWindowState.Maximized; 
                mainForm.FormBorderStyle = FormBorderStyle.None;
                fsCheck = true;
            }
        }
        private void PixelLocationArg(string arg)
        {
            string rg = @"/PixelLocation=(.+)\,(.+)";
            GroupCollection gc = RegexCheck.Test(arg, rg);
            if (gc != null)
            {
                int x = 0, y = 0;
                Int32.TryParse(gc[1].Value, out x);
                Int32.TryParse(gc[2].Value, out y);
                mainForm.Location = new Point(x, y);
                pixelCheck = true;
            }
        }
        private void DisplaySelectionArg(string arg)
        {
            string rg = @"/DisplayID=([0-9])";
            GroupCollection gc = RegexCheck.Test(arg, rg);
            if (gc != null)
            {
                int id;
                Int32.TryParse(gc[1].Value, out id);
                if (Screen.AllScreens.Length >= id) { mainForm.Location = Screen.AllScreens[id - 1].WorkingArea.Location; }
                disCheck = true;
            }
        }
    }
}
