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

        public abstract Color Lighting(in Material m, in HitObject obj, in Point p, in Vector eye, in Vector norm, bool shadowed = false);
    }


    public class PointLight : Light
    {

        public PointLight(Point p, Color i)
        {
            pos = p;
            intensity = i;
        }
        public override Color Lighting(in Material m, in HitObject obj, in Point p, in Vector eye, in Vector norm, bool shadowed = false)
        {
            Color eff_color = (m.pattern != null) ? m.pattern.GetOnObject(p, obj) : m.color;
            eff_color *= intensity;

            Color ambient = eff_color * m.ambient;


            Vector lightV = MatMaths.Norm(pos - p);

            Color dif;
            Color spec;

            double light_dot_normal = MatMaths.Dot(lightV, norm);
            if (light_dot_normal < 0 || shadowed)
            {
                dif = Color.Black;
                spec = Color.Black;
            }
            else
            {
                dif = eff_color * m.diffuse * light_dot_normal;

                Vector reflectv = Vector.Reflect(-lightV, norm);
                double reflect_dot_eye = MatMaths.Dot(reflectv, eye);

                if (reflect_dot_eye <= 0)
                    spec = Color.Black;
                else
                {
                    double factor = Math.Pow(reflect_dot_eye, m.shininess);
                    spec = intensity * m.specular * factor;

                }

            }
            Color c = ambient + dif + spec;
            return c;
        }


    }



}
