using System;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace ClipboardImageToFile
{
    class Program
    {


        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine(@"example: ClipboardImageToFile.exe --secondsToWait 5 --imagePath C:\screen.png");
            Clipboard.SetText(" ");
            string secondsToWait = args.Length>1? args[1]: "5";
            string imagePath = args.Length > 3 ? args[3] : @"C:\screen.png";

            int milisToSleep = 100;
            int isecondsToWait = int.Parse(secondsToWait)*(1000/milisToSleep);

            Thread thread = new Thread(() => SaveImage(isecondsToWait, milisToSleep, imagePath));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        static void SaveImage(int isecondsToWait, int milisToSleep, string imagePath)
        {
            for (int i = 0; i < isecondsToWait; i++)
            {
                Thread.Sleep(milisToSleep);
                var img = Clipboard.GetImage();
                if (img != null)
                {
                    img.Save(imagePath, ImageFormat.Png);
                    break;
                }
            }
        }
    }
}
