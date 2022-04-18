using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracingCS
{
    class Canvas
    {
        public readonly int width, height;
        Color[,] canvas;
        public Canvas(int width, int height)
        {
            this.width = width;
            this.height = height;

            canvas = new Color[width, height];

            for (int row = 0; row < height; row++)
                for (int col = 0; col < width; col++)
                    canvas[row, col] = new Color();

        }

        public void Flush(in Color color)
        {
            for (int row = 0; row < height; row++)
                for (int col = 0; col < width; col++)
                    canvas[row, col] = color;
        }

        public void WritePixel(int x, int y, in Color color)
        {
#if DEBUG
            Console.WriteLine($"Writing to ({x},{y})");
#endif

            if (x >= width || x < 0 || y >= height || y < 0) 
                return;

            canvas[y, x] = color;
        }

        public void ToPPM()
        {
            var sb = new System.Text.StringBuilder();
            sb.Append($"P3\n{width} {height}\n255\n");

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    sb.Append(canvas[row, col].ToString() + " ");
                }
                sb.Append('\n');
            }


            System.IO.File.WriteAllText("canvas.ppm", sb.ToString());
        }
    }
}
