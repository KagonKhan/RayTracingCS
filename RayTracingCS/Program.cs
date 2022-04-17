using System;

namespace RayTracingCS
{





    class Program
    {

        static void Main(string[] args)
        {
            Canvas canvas = new(1920, 1080);

            canvas.Flush(Color.Magenta);

            canvas.ToPPM();
        }
    }
}
