using System;
using System.Threading;


namespace RayTracingCS
{
    class Program
    {
        static void Main(string[] args)
        {


            Canvas canvas = new(500, 500);
            canvas.Flush(Color.Blue);

            var middle  = new Point(250, 250, 0);
            var top     = new Point(0, 0, 1);

            for (int i = 0; i < 12; i++) { 
                var point = Mat4.RotationY(i * Math.PI / 6d).Scale(3,3,3) * top;
                point.x *= 20;
                point.z *= 20;
                point.x += 250;
                point.z += 250;
                canvas.WritePixel((int)(point.x), (int)(point.z), Color.White);
            }




            canvas.ToPPM();
        }
    }
}
