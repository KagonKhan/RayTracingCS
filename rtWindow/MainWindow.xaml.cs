using System;
using System.Collections.Generic;
using System.Diagnostics;
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



        private Stopwatch _stopwatch;
        bool StopTimer = false;

        //public DispatcherTimer timer4x1 = new DispatcherTimer();
        //public DispatcherTimer timer4x2 = new DispatcherTimer();
        //public DispatcherTimer timer4x3 = new DispatcherTimer();
        //public DispatcherTimer timer4x4 = new DispatcherTimer();
        public DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            Initialize();
            InitializeTimings();

        }

        void Initialize()
        {
            width = 7680 / 4;
            height = 4320 / 4;

            RayTracingCS.Program.InitRealTime(width, height);

            image = new WriteableBitmap(width, height, 96, 96, PixelFormats.Rgb24, null);
            pixels = new byte[width * height * 3];
            wImage.Source = image;



            myCol4x1 = 0;
            myRow4x1 = 0;

            myCol4x2 = width/2;
            myRow4x2 = 0;

            myCol4x3 = 0;
            myRow4x3 = height/2;

            myCol4x4 = width/2;
            myRow4x4 = height/2;

        }
        void InitializeTimings()
        {
            //for (int i = 0; i < 1024/4; i++)
            //    timer4x1.Tick += Render4x1;

            //for (int i = 0; i < 1024/4; i++)
            //    timer4x2.Tick += Render4x2;

            //for (int i = 0; i < 1024/4; i++)
            //    timer4x3.Tick += Render4x3;

            //for (int i = 0; i < 1024/4; i++)
            //    timer4x4.Tick += Render4x4;


            for (int i = 0; i < 1024 ; i++)
                timer.Tick += Render;
            timer.Tick += Write;




            _stopwatch = Stopwatch.StartNew();
            //timer4x1.Start();
            //timer4x2.Start();
            //timer4x3.Start();
            //timer4x4.Start();
            timer.Start();
        }


        void Render(object sender, EventArgs e)
        {

            if (col == width - 1 && row == height - 1)
            {
                StopTimer = true;
                return;
            }

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


            int elapsedMs = (int)_stopwatch.ElapsedMilliseconds;

            if(StopTimer == false)
                timetext.Content = elapsedMs.ToString() + "[ms]";
        }




        #region Regional Rendering
        int myCol4x1 = 0;
        int myRow4x1 = 0;
        void Render4x1(object sender, EventArgs e)
        {

            if (myCol4x1 == width / 2 - 1 && myRow4x1 == height / 2 - 1)
                return;
            


            if (++myCol4x1 == width/2)
            {
                myRow4x1++;
                myCol4x1 = 0;
            }


            int index = (myRow4x1 * width + myCol4x1) * 3;
            RayTracingCS.Color color = RayTracingCS.Program.RenderRealTime(myRow4x1, myCol4x1);
            pixels[index + 0] = (byte)color.r;
            pixels[index + 1] = (byte)color.g;
            pixels[index + 2] = (byte)color.b;

        }



        int myCol4x2 = 0;
        int myRow4x2 = 0;
        void Render4x2(object sender, EventArgs e)
        {

            if (myCol4x2 == width - 1 && myRow4x2 == height / 2 - 1)
                return;

            if (++myCol4x2 == width)
            {
                myRow4x2++;
                myCol4x2 = width/2;
            }


            int index = (myRow4x2 * width + myCol4x2) * 3;
            RayTracingCS.Color color = RayTracingCS.Program.RenderRealTime(myRow4x2, myCol4x2);
            pixels[index + 0] = (byte)color.r;
            pixels[index + 1] = (byte)color.g;
            pixels[index + 2] = (byte)color.b;

        }


        int myCol4x3 = 0;
        int myRow4x3 = 0;
        void Render4x3(object sender, EventArgs e)
        {

            if (myCol4x3 == width / 2 - 1 && myRow4x3 == height - 1)
            {
                return;
            }

            if (++myCol4x3 == width / 2)
            {
                myRow4x3++;
                myCol4x3 = 0;
            }


            int index = (myRow4x3 * width + myCol4x3) * 3;
            RayTracingCS.Color color = RayTracingCS.Program.RenderRealTime(myRow4x3, myCol4x3);
            pixels[index + 0] = (byte)color.r;
            pixels[index + 1] = (byte)color.g;
            pixels[index + 2] = (byte)color.b;

        }




        int myCol4x4 = 0;
        int myRow4x4 = 0;
        void Render4x4(object sender, EventArgs e)
        {

            if (myCol4x4 == width - 1 && myRow4x4 == height - 1)
            {
                return;
            }

            if (++myCol4x4 == width)
            {
                myRow4x4++;
                myCol4x4 = width/2;
            }


            int index = (myRow4x4 * width + myCol4x4) * 3;
            RayTracingCS.Color color = RayTracingCS.Program.RenderRealTime(myRow4x4, myCol4x4);
            pixels[index + 0] = (byte)color.r;
            pixels[index + 1] = (byte)color.g;
            pixels[index + 2] = (byte)color.b;

        }
        #endregion
    }
}



/*                                               Benchmarks 
 *                                               
 *                                               
 *                                               
 *                                               
 * width = 7680 / 4, height = 4320 / 4      |       1024:1 calc:write ops       |            20297 [ms]
 * width = 7680 / 4, height = 4320 / 4      |       16:1 calc:write ops         |            199336 [ms]
 * 
 * width = 7680 / 16, height = 4320 / 16    |       1024:1 calc:write ops       |            2144   [ms] Seems to be the upper limit, higher values provided no speed up
 * width = 7680 / 16, height = 4320 / 16    |       128:1 calc:write ops        |            2852   [ms]
 * width = 7680 / 16, height = 4320 / 16    |       32:1 calc:write ops         |            4487   [ms]
 * width = 7680 / 16, height = 4320 / 16    |       16:1 calc:write ops         |            5394   [ms]
 * width = 7680 / 16, height = 4320 / 16    |       2:1 calc:write ops          |            16059  [ms]
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */