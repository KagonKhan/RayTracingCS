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

        public double reflective;
        public double transparency;
        public double refraction;

        public Material(float amb, float dif, float spe, float shi, in Color col, double reflect = 0.0, double trans = 0.0, double refr = 1.0)
        {
            ambient = amb;
            diffuse = dif;
            specular = spe;
            shininess = shi;
            color = col;
            pattern = null;
            reflective = reflect;
            transparency = trans;
            refraction = refr;
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
        public Mat4 Transformation = new Mat4(MatMaths.I);

        #region Solid Color constants
        static public readonly SolidColorPattern red        = new(1, 0, 0);
        static public readonly SolidColorPattern Black      = new(0, 0, 0);
        static public readonly SolidColorPattern White      = new(1, 1, 1);
        static public readonly SolidColorPattern Red        = new(1, 0, 0);
        static public readonly SolidColorPattern Lime       = new(0, 1, 0);
        static public readonly SolidColorPattern Blue       = new(0, 0, 1);
        static public readonly SolidColorPattern Yellow     = new(1, 1, 0);
        static public readonly SolidColorPattern Cyan       = new(0, 1, 1);
        static public readonly SolidColorPattern Magenta    = new(1, 0, 1);
        static public readonly SolidColorPattern Silver     = new(0.75, 0.75, 0.75);
        static public readonly SolidColorPattern Gray       = new(0.5, 0.5, 0.5);
        static public readonly SolidColorPattern Maroon     = new(0.5, 0, 0);
        static public readonly SolidColorPattern Olive      = new(0.5, 0.5, 0);
        static public readonly SolidColorPattern Green      = new(0, 0.5, 0);
        static public readonly SolidColorPattern Purple     = new(0.5, 0, 0.5);
        static public readonly SolidColorPattern Teal       = new(0, 0.5, 0.5);
        static public readonly SolidColorPattern Navy       = new(0, 0, 0.5);
        #endregion






        protected Pattern() { }

        public abstract Color Get(in Point p, params double[] param);
        public Color GetOnObject(in Point p, in HitObject obj, params double[] param)  
        {
            Point obj_p = obj.Transformation.Inversed() * p;
            Point pat_p = Transformation.Inversed() * obj_p;

            return Get(pat_p, param);
        }
    }

    public class SolidColorPattern : Pattern
    {
        Color color;
        public SolidColorPattern(in Color c) 
        {
            color = c;
        }
        public SolidColorPattern(double r, double g, double b) 
        {
            color = new Color(r,g,b);
        }
        public override Color Get(in Point p, params double[] param)
        {
            return color;
        }
    }


    public class StripePattern : Pattern
    {
        Pattern a, b;
        public StripePattern(in Pattern a, in Pattern b)
        {
            this.a = a;
            this.b = b;
        }
        public override Color Get(in Point p, params double[] param)
        {
            Point tp = Transformation.Inversed() * p;
            return Math.Floor(tp.X) % 2 == 0 ? a.Get(tp, param) : b.Get(tp, param);
        }
    }
    public class GradientPattern : Pattern
    {
        Pattern a, b;
        public GradientPattern(in Pattern a, in Pattern b)
        {
            this.a = a;
            this.b = b;
        }
        public override Color Get(in Point p, params double[] param)
        {
            return a.Get(p, param) + (b.Get(p, param) - a.Get(p, param)) * (p.X - Math.Floor(p.X));
        }
    }
    public class MirroredGradient : Pattern
    {
        Pattern a, b;
        public MirroredGradient(in Pattern a, in Pattern b)
        {
            this.a = a;
            this.b = b; 
            Transformation = MatMaths.I.Scaled(2, 1, 1);
        }
        
 
        public override Color Get(in Point p, params double[] param)
        {
            if(Math.Abs(p.X - Math.Floor(p.X)) < 0.5)
                return a.Get(p, param) + (b.Get(p, param) - a.Get(p, param)) * (2 * p.X - Math.Floor(2 * p.X));
            else
                return b.Get(p, param) + (a.Get(p, param) - b.Get(p, param)) * (2 * p.X - Math.Floor(2 * p.X));
        }
    }
    public class RingPattern : Pattern
    {
        Pattern a, b;
        public RingPattern(in Pattern a, in Pattern b)
        {
            this.a = a;
            this.b = b;
        }
        public override Color Get(in Point p, params double[] param)
        {
            return Math.Floor(Math.Sqrt(p.X * p.X + p.Z * p.Z)) % 2 == 0 ? a.Get(p, param) : b.Get(p, param);
        }
    }


    public class CheckeredPattern : Pattern
    {
        Pattern a, b;
        public CheckeredPattern(in Pattern a, in Pattern b)
        {
            this.a = a;
            this.b = b;
        }
        public override Color Get(in Point p, params double[] param)
        {
            Point blend = Transformation.Inversed() * p;
            return (Math.Floor(blend.X) + Math.Floor(blend.Y) + Math.Floor(blend.Z)) % 2 == 0 ? a.Get(blend, param) : b.Get(blend, param);
        }
    }
    public class Checkered2DPattern : Pattern
    {
        Pattern a, b;
        public Checkered2DPattern(in Pattern a, in Pattern b)
        {
            this.a = a;
            this.b = b;
        }
        public override Color Get(in Point p, params double[] param)
        {
            Point blend = Transformation.Inversed() * p;
            return (Math.Floor(blend.X) + Math.Floor(blend.Z)) % 2 == 0 ? a.Get(blend, param) : b.Get(blend, param);
        }
    }

    public class RadialGradientPattern : Pattern
    {
        Pattern a, b;
        public RadialGradientPattern(in Pattern a, in Pattern b)
        {
            this.a = a;
            this.b = b;
        }
        public override Color Get(in Point p, params double[] param)
        {
            double dist = Math.Sqrt(p.X * p.X + p.Z * p.Z);
            return a.Get(p, param) + (b.Get(p, param) - a.Get(p, param)) * (dist - Math.Floor(dist));
        }
    }







    public class NestedPattern : Pattern
    {
        Pattern a, b;
        double t;
        public NestedPattern(in Pattern a, in Pattern b, double t = 0.5)
        {
            this.a = a;
            this.b = b;
            this.t = t;
        }
        public override Color Get(in Point p, params double[] param)
        {
            Point blend = Transformation.Inversed() * p;
            return a.Get(blend, param) * (1.0 - t) + b.Get(blend, param) * t;
        }
    }
}
