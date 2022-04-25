using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RayTracingCS
{
    public class Canvas
    {
        public readonly int width, height;
        public Color[,] canvas;

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
//#if DEBUG
//            Console.WriteLine($"Writing to ({row},{col})");
//#endif

            if (col >= width || col < 0 || row >= height || row < 0) 
                return;


            canvas[row, col] = ClampR(c * 255);
        }

        // Artifacts: no space after color values in file

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


        private void Clamp(ref Color c)
        {
            c.r = Math.Clamp(c.r, 0, 255);
            c.g = Math.Clamp(c.g, 0, 255);
            c.b = Math.Clamp(c.b, 0, 255);
        }
        public static Color ClampR(in Color c)
        {
            return new Color(Math.Clamp(c.r, 0, 255), Math.Clamp(c.g, 0, 255), Math.Clamp(c.b, 0, 255));
            
        }
    }
}
