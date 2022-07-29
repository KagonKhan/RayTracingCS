using System;
using System.Drawing;
using System.Threading;


namespace RayTracingCS
{
    public class Camera
    {
        public int width;
        public int height;
        public double fov;
        public double pxsize;
        public double halfWidth;
        public double halfHeight;


        private Ray r = new();
        private Mat4 camInverse = new(Mat4.I);
        private Mat4 transform = new(Mat4.I);


        public Mat4 Transformation
        {
            get { return transform; }
            set
            {
                transform = value;
                camInverse = value.Inversed();
            }
        }

        public Camera(int w, int h, double fov)
        {
            width = w;
            height = h;
            this.fov = fov;

            double half_view = Math.Tan(fov / 2);
            double aspect = (double)width / height;

            if (aspect >= 1)
            {
                halfWidth = half_view;
                halfHeight = half_view / aspect;
            }
            else
            {
                halfWidth = half_view * aspect;
                halfHeight = half_view;
            }

            pxsize = 2 * halfWidth / width;
        }

        public Ray Ray(int row, int col)
        {
            double xoffset = (col + 0.5) * pxsize;
            double yoffset = (row + 0.5) * pxsize;

            double world_x = halfWidth - xoffset;
            double world_y = halfHeight - yoffset;


            Point pixel = camInverse * new Point(world_x, world_y, -1);
            Point origin = camInverse * new Point(0, 0, 0);
            Vector dir = (pixel - origin).Normalized();

            return new Ray(origin, dir);
        }

        public void Ray(ref Ray r, int row, int col)
        {
            double xoffset = (col + 0.5) * pxsize;
            double yoffset = (row + 0.5) * pxsize;

            double world_x = halfWidth - xoffset;
            double world_y = halfHeight - yoffset;


            Point pixel = camInverse * new Point(world_x, world_y, -1);

            r.origin = camInverse * new Point(0, 0, 0);
            r.direction = (pixel - r.origin).Normalized();
        }

        public Canvas Render(in World w)
        {
            int threads = 2;
            var copy = w;
            var retVal = new Canvas(height, width);

            Thread[] qs = new Thread[threads*threads];

            for (int i = 0; i < threads; i++)
            {
                for (int j = 0; j < threads; j++)
                {
                    var row_beg = (i + 0) * width / threads;
                    var row_end = (i + 1) * width / threads - 1;

                    var col_beg = (j + 0) * height / threads;
                    var col_end = (j + 1) * height / threads - 1;

                    qs[i * threads + j] =
                        new Thread(() => Render(ref retVal, ref copy, row_beg, row_end, col_beg, col_end));
                }
            }

            /*  i, j |  width, height = 1000
             *------------------------
             *  0, 0 |  (0->249, 0->249)
             *  0, 1 |  (0->245, 250->499)
             *  0, 2 |  (0->245, 500->749)
             *  0, 3 |  (0->245, 750->999)
             *------------------------
             *  1, 0 |  (250->499, 0->249)
             *  1, 1 |  (250->499, 250->499)
             *  1, 2 |  (250->499, 500->749)
             *  1, 3 |  (250->499, 750->999)
             *------------------------ 
             *  2, 0 |
             *  2, 1 |
             *  2, 2 |
             *  2, 3 |
             *------------------------ 
             *  3, 0 |
             *  3, 1 |
             *  3, 2 |
             *  3, 3 |
             *
             * 
             */


            for (int i = 0; i < threads * threads; i++)
            {
                qs[i].Start();
            }

            for (int i = 0; i < threads * threads; i++)
            {
                qs[i].Join();
            }

            return retVal;
        }

        public void Render(ref Canvas canvas, ref World w, int row_beg, int row_end, int col_beg, int col_end)
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: Width: {col_beg} -> {col_end}\nHeight: {row_beg} -> {row_end}\n\n");

            Color c;
            Ray r = new();

            for (int t_row = row_beg; t_row < row_end; t_row++)
            for (int t_col = col_beg; t_col < col_end; t_col++)
            {
                Ray(ref r, t_row, t_col);
                c = w.Coloring(r);

                canvas.WritePixel(t_row, t_col, c);

                if ((t_row * (col_end - col_beg) + t_col) % 10000 == 0)
                    Console.WriteLine(
                        $"Thread {Thread.CurrentThread.ManagedThreadId}: {(((t_row - row_beg) * (col_end - col_beg) + (t_col - col_beg)) / 1000)} out of {(row_end - row_beg) * (col_end - col_beg) / 1000}");
            }
        }

        static int row = 0, col = 0;

        public Color RenderRealTime(in World w)
        {
            Ray(ref r, row, col);

            col++;
            if (col >= width)
            {
                col = 0;
                row++;
            }

            return w.Coloring(r);
        }

        public Color RenderRealTime(int y, int x, in World w)
        {
            Ray(ref r, y, x);
            return w.Coloring(r);
        }
    }
}