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

namespace rtWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        WriteableBitmap writeableBitmap;
        byte[] pixels;
        int width, height;

        public MainWindow()
        {
            InitializeComponent();
            width = 7600 / 16;
            height = 4320 / 16;
            RayTracingCS.Program.InitRealTime(width, height);



            CreateImage();
            Render();
            SetImage();
        }
        void CreateImage()
        {
            writeableBitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Rgb24, null);
            pixels = new byte[(int)(width * height * 3)];
        }
        void Render()
        {
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int index = (row * width + col) * 3;
                    RayTracingCS.Color color = RayTracingCS.Program.RenderRealTime();

                    pixels[index + 0] = (byte)color.r;
                    pixels[index + 1] = (byte)color.g;
                    pixels[index + 2] = (byte)color.b;


                }
                SetImage();
            }
        }

        public void SetImage()
        {
            writeableBitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, width * 3, 0, 0);
            image.Source = writeableBitmap;
        }
    }
}
