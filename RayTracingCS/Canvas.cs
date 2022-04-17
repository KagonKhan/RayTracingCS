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

            for (int col = 0; col < width; col++)
                for (int row = 0; row < height; row++)
                    canvas[col, row] = new Color();

        }

        public void Flush(Color color)
        {
            for (int col = 0; col < width; col++)
                for (int row = 0; row < height; row++)
                    canvas[col, row] = color;
        }

        public void WritePixel(int x, int y, Color color)
        {
            canvas[x, y] = color;
        }

        public void ToPPM()
        {
            var sb = new System.Text.StringBuilder();
            sb.Append($"P3\n{width} {height}\n255\n");

            for (int col = 0; col < width; col++)
            {
                for (int row = 0; row < height; row++)
                {
                    sb.Append(canvas[col, row].ToString());
                }
                sb.Append('\n');
            }


            System.IO.File.WriteAllText("canvas.ppm", sb.ToString());
        }
    }
}
