using System;


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


        private Ray r           = new ();
        private Mat4 camInverse = new (Mat4.I);
        private Mat4 transform  = new (Mat4.I);


        public Mat4 Transformation
        {
            get { return transform; }
            set
            {
                transform  = value;
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
            Canvas retVal = new Canvas(height, width);

            Color c;

            for (int row = 0; row < height; row++)
                for (int col = 0; col < width; col++)
                {
                    Ray(ref r, row, col);
                    c = w.Coloring(r);

                    retVal.WritePixel(row, col, c);

                    if ((row * width + col) % 10000 == 0)
                        Console.WriteLine($"{((row * width + col)/1000)} out of {height * width / 1000}");

                }


            return retVal;
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
