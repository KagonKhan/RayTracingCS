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
using System.Windows.Shapes;

namespace RayTracingCS
{
    /// <summary>
    /// Interaction logic for rtWindow.xaml
    /// </summary>
    /// 


    public partial class rtWindow : Window
    {

        WriteableBitmap writeableBitmap;
        byte[] pixels;
        int width, height;

        Window w = new Window();

        public rtWindow(int w, int h)
        {
            width = w;
            height = h;

            InitializeComponent();

            CreateImage();
        }

        void CreateImage()
        {
            writeableBitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Rgb24, null);
            pixels = new byte[(int)(width * height * 3)];
        }

        public void WriteAt(int index, byte value)
        {
            pixels[index] = value;
        }

        public void SetImage()
        {
            writeableBitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, width * 3, 0, 0);
            image.Source = writeableBitmap;
        }

        void Render()
        {
            for (int row = 0; row < height; row++)
                for (int col = 0; col < width; col++)
                {
                    int index = (row * width + col) * 3;
                    pixels[index + 0] = 255;
                    pixels[index + 1] = (byte)(col / 2);
                    pixels[index + 2] = (byte)(row / 2);
                }
        }
    }
}
