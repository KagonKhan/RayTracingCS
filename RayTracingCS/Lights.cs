using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracingCS
{

    public abstract class Light
    {
        public Point pos;
        public Color intensity;

        public abstract Color Lighting(in Material m, in Point p, in Vector eye, in Vector norm);
    }


    public class PointLight : Light
    {

        public PointLight(Point p, Color i)
        {
            pos = p;
            intensity = i;
        }
        public override Color Lighting(in Material m, in Point p, in Vector eye, in Vector norm)
        {
            var eff_color = m.color * intensity;
            var ambient = eff_color * m.ambient;


            var lightV = MatMaths.Norm(pos - p);

            Color dif;
            Color spec;

            var light_dot_normal = MatMaths.Dot(lightV, norm);
            if (light_dot_normal < 0)
            {
                dif = Color.Black;
                spec = Color.Black;
            }
            else
            {
                dif = eff_color * m.diffuse * light_dot_normal;

                var reflectv = Vector.Reflect(-lightV, norm);
                var reflect_dot_eye = MatMaths.Dot(reflectv, eye);

                if (reflect_dot_eye <= 0)
                    spec = Color.Black;
                else
                {
                    var factor = Math.Pow(reflect_dot_eye, m.shininess);
                    spec = intensity * m.specular * factor;

                }

            }
            Color c = ambient + dif + spec;
            Clamp(ref (c));

            return c;
        }

        public static void Clamp(ref Color c)
        {
            c.r = Math.Clamp(c.r, 0, 255);
            c.g = Math.Clamp(c.g, 0, 255);
            c.b = Math.Clamp(c.b, 0, 255);
        }
    }



}
