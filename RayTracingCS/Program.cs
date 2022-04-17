using System;
using System.Threading;


namespace RayTracingCS
{
    class Program
    {
        static void Main(string[] args)
        {
            

            Canvas canvas = new(500, 500);


            var m = new Mat4(-5, 2, 6, -8,
                              1, -5, 1, 8,
                              7, 7, -6, -7,
                              1, -3, 7, 4);

            var b = m.Inverse();

            var res = new Mat4(0.21805, 0.45113, 0.24060, -0.04511,
                               -0.80827, -1.45677, -0.44361, 0.52068,
                               -0.07895, -0.22368, -0.05263, 0.19737,
                               -0.52256, -0.81391, -0.30075, 0.30639);



            Console.WriteLine(b.ToString());
            Console.WriteLine(res.ToString());
            //canvas.ToPPM();
        }
    }
}
