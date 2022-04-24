using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace rtWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        WriteableBitmap image;
        byte[] pixels;
        int width, height;

        int row, col;

        public DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            width = 7680 / 4;
            height = 4320 / 4;

            RayTracingCS.Program.InitRealTime(width, height);




            for(int i = 0; i < 16; i++)
                timer.Tick += Render;
            timer.Tick += Write;

            image = new WriteableBitmap(width, height, 96, 96, PixelFormats.Rgb24, null);
            pixels = new byte[width * height * 3];
            wImage.Source = image;


            timer.Start();
        }

        void Render(object sender, EventArgs e)
        {

            if (col == width-1 && row == height-1)
                return;

            if(++col == width)
            {
                row++;
                col = 0;
            }



            int index = (row * width + col) * 3;
            RayTracingCS.Color color = RayTracingCS.Program.RenderRealTime();
            pixels[index + 0] = (byte)color.r;
            pixels[index + 1] = (byte)color.g;
            pixels[index + 2] = (byte)color.b;

        }

        void Write(object sender, EventArgs e)
        {
            image.WritePixels(new Int32Rect(0, 0, width, height), pixels, width * 3, 0, 0);
        }
    }
}
