using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracingCS
{

    public class Material
    {
        public float ambient;
        public float diffuse;
        public float specular;
        public float shininess;
        public Color color;
        public Pattern pattern;

        public Material(float amb, float dif, float spe, float shi, in Color col)
        {
            ambient = amb;
            diffuse = dif;
            specular = spe;
            shininess = shi;
            color = col;
            pattern = null;
        }

        public Material()
        {
            color = new Color(1, 1, 1);
            ambient = 0.1f;
            diffuse = 0.9f;
            specular = 0.9f;
            shininess = 200f;
            pattern = null;
        }
    }

    public abstract class Pattern
    {
        public Color a, b;
        public Mat4 Transformation = new Mat4(MatMaths.I);

        protected Pattern(in Color a, in Color b)
        {
            this.a = a;
            this.b = b;
        }

        public abstract Color Get(in Point p, params double[] param);
        public Color GetOnObject(in Point p, in HitObject obj, params double[] param)  
        {
            Point obj_p = obj.Transformation.Inversed() * p;
            Point pat_p = Transformation.Inversed() * obj_p;

            return Get(pat_p, param);
        }
    }




    public class StripePattern : Pattern
    {
        public StripePattern(in Color a, in Color b) : base(a, b) { }
        public override Color Get(in Point p, params double[] param)
        {
            return Math.Floor(p.X) % 2 == 0 ? a : b;
        }
    }
    public class GradientPattern : Pattern
    {
        public GradientPattern(in Color a, in Color b) : base(a, b) { }
        public override Color Get(in Point p, params double[] param)
        {
            return a + (b - a) * (p.X - Math.Floor(p.X));
        }
    }
    public class MirroredGradient : Pattern
    {
        public MirroredGradient(in Color a, in Color b) : base(a, b)
        {
            Transformation = MatMaths.I.Scaled(2, 1, 1);
        }
        public override Color Get(in Point p, params double[] param)
        {
            if(Math.Abs(p.X - Math.Floor(p.X)) < 0.5)
                return a + (b - a) * (2 * p.X - Math.Floor(2 * p.X));
            else
                return b + (a - b) * (2 * p.X - Math.Floor(2 * p.X));
        }
    }
    public class RingPattern : Pattern
    {
        public RingPattern(in Color a, in Color b) : base(a, b) { }
        public override Color Get(in Point p, params double[] param)
        {
            return Math.Floor(Math.Sqrt(p.X * p.X + p.Z * p.Z)) % 2 == 0 ? a : b;
        }
    }


    public class CheckeredPattern : Pattern
    {
        public CheckeredPattern(in Color a, in Color b) : base(a, b) { }
        public override Color Get(in Point p, params double[] param)
        {
            return (Math.Floor(p.X) + Math.Floor(p.Y) + Math.Floor(p.Z)) % 2 == 0 ? a : b;
        }
    }
    public class Checkered2DPattern : Pattern
    {
        public Checkered2DPattern(in Color a, in Color b) : base(a, b) { }
        public override Color Get(in Point p, params double[] param)
        {
            return (Math.Floor(p.X) + Math.Floor(p.Z)) % 2 == 0 ? a : b;
        }
    }
    public class RadialGradientPattern : Pattern
    {
        public RadialGradientPattern(in Color a, in Color b) : base(a, b) { }
        public override Color Get(in Point p, params double[] param)
        {
            return (Math.Floor(p.X) + Math.Floor(p.Z)) % 2 == 0 ? a : b;
        }
    }
}
