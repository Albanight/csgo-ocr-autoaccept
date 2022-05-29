using System.Drawing;
using IronOcr;
using System.Threading;
using System.Runtime.InteropServices;


while (true)
{
    void Main()
    {
        var Ocr = new IronTesseract();
        Bitmap memoryImage;
        memoryImage = new Bitmap(220, 90);
        Size s = new Size(memoryImage.Width, memoryImage.Height);

        Graphics memoryGraphics = Graphics.FromImage(memoryImage);

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        const int MOUSEEVENT_LEFTDOWN = 0x02;
        const int MOUSEEVENT_LEFTUP = 0x04;

        memoryGraphics.CopyFromScreen(850, 403, 0, 0, s);

       
        var filePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory) + @"\image.png";
        string fileName = string.Format(filePath);
        memoryImage.Save(fileName);

        using (var Input = new OcrInput(fileName))
        {
            Input.DeNoise();
            var Result = Ocr.Read(Input);
            Console.WriteLine(Result.Text);

            if (Result.Text != "ACCEPT")
            {
                Console.WriteLine("No game ):");
                Thread.Sleep(3000);
                Main();
            }
            else if (Result.Text == "ACCEPT")
            {
                SetCursorPos(1050, 420);
                Console.WriteLine("Game found!");
                Thread.Sleep(1000);
                mouse_event(MOUSEEVENT_LEFTDOWN, 0, 0, 0, 0);
                Thread.Sleep(100);
                mouse_event(MOUSEEVENT_LEFTUP, 0, 0, 0, 0);

            }
        }



    }
    Main();
    break;
}
