using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracingCS
{
    public class Canvas
    {
        public readonly int width, height;
        Color[,] canvas;

        public Color this[int row, int col]
        {
            get => canvas[row, col]; 
        }

        public Canvas(int rows, int cols)
        {
            this.height = rows;
            this.width = cols;

            canvas = new Color[rows, cols];

        }

        public void Flush(in Color color)
        {
            for (int row = 0; row < height; row++)
                for (int col = 0; col < width; col++)
                    canvas[row, col] = color;
        }

        public void WritePixel(int row, int col, in Color c)
        {
#if DEBUG
            Console.WriteLine($"Writing to ({x},{y})");
#endif

            if (col >= width || col < 0 || row >= height || row < 0) 
                return;

            Color scaled = c * 255;
            PointLight.Clamp(ref (scaled));
            canvas[row, col] = scaled;
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
