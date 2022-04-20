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

        public abstract Color Lighting(in Material m, in Point p, in Vector eye, in Vector norm, bool shadowed = false);
    }


    public class PointLight : Light
    {

        public PointLight(Point p, Color i)
        {
            pos = p;
            intensity = i;
        }
        public override Color Lighting(in Material m, in Point p, in Vector eye, in Vector norm, bool shadowed = false)
        {
            var eff_color = m.color * intensity;
            var ambient = eff_color * m.ambient;


            var lightV = MatMaths.Norm(pos - p);

            Color dif;
            Color spec;

            var light_dot_normal = MatMaths.Dot(lightV, norm);
            if (light_dot_normal < 0 || shadowed)
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
            return c;
        }


    }



}
