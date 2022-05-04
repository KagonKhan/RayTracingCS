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

        public override bool Equals(object obj)
        {
            if (obj is Material m)
            {
                return ambient == m.ambient
                    && diffuse == m.diffuse
                    && specular == m.specular
                    && shininess == m.shininess
                    && color == m.color
                    && pattern == m.pattern
                    && reflective == m.reflective
                    && transparency == m.transparency
                    && refraction == m.refraction;         
            }

            return false;
        }

    }

    public abstract class Pattern
    {
        public Color a, b;
        
        public Mat4 Transformation = new Mat4(MatMaths.I);

        #region Solid Color constants
        static public readonly Color Black      = new(0, 0, 0);
        static public readonly Color White      = new(1, 1, 1);
        static public readonly Color Red        = new(1, 0, 0);
        static public readonly Color Lime       = new(0, 1, 0);
        static public readonly Color Blue       = new(0, 0, 1);
        static public readonly Color Yellow     = new(1, 1, 0);
        static public readonly Color Cyan       = new(0, 1, 1);
        static public readonly Color Magenta    = new(1, 0, 1);
        static public readonly Color Silver     = new(0.75, 0.75, 0.75);
        static public readonly Color Gray       = new(0.5, 0.5, 0.5);
        static public readonly Color Maroon     = new(0.5, 0, 0);
        static public readonly Color Olive      = new(0.5, 0.5, 0);
        static public readonly Color Green      = new(0, 0.5, 0);
        static public readonly Color Purple     = new(0.5, 0, 0.5);
        static public readonly Color Teal       = new(0, 0.5, 0.5);
        static public readonly Color Navy       = new(0, 0, 0.5);
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
        public StripePattern(in Color a, in Color b)
        {
            this.a = a;
            this.b = b;
        }
        public override Color Get(in Point p, params double[] param)
        {
            Point tp = Transformation.Inversed() * p;
            return Math.Floor(tp.X) % 2 == 0 ? a : b;
        }
    }
    public class GradientPattern : Pattern
    {
        public GradientPattern(in Color a, in Color b)
        {
            this.a = a;
            this.b = b;
        }
        public override Color Get(in Point p, params double[] param)
        {
            return a + (b - a) * (p.X - Math.Floor(p.X));
        }
    }
    public class MirroredGradient : Pattern
    {
        public MirroredGradient(in Color a, in Color b)
        {
            this.a = a;
            this.b = b; 
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
        public RingPattern(in Color a, in Color b)
        {
            this.a = a;
            this.b = b;
        }
        public override Color Get(in Point p, params double[] param)
        {
            return Math.Floor(Math.Sqrt(p.X * p.X + p.Z * p.Z)) % 2 == 0 ? a : b;
        }
    }


    public class CheckeredPattern : Pattern
    {
        public CheckeredPattern(in Color a, in Color b)
        {
            this.a = a;
            this.b = b;
        }
        public override Color Get(in Point p, params double[] param)
        {
            Point blend = Transformation.Inversed() * p;
            return (Math.Floor(blend.X) + Math.Floor(blend.Y) + Math.Floor(blend.Z)) % 2 == 0 ? a : b;
        }
    }
    public class Checkered2DPattern : Pattern
    {
        public Checkered2DPattern(in Color a, in Color b)
        {
            this.a = a;
            this.b = b;
        }
        public override Color Get(in Point p, params double[] param)
        {
            Point blend = Transformation.Inversed() * p;
            return (Math.Floor(blend.X) + Math.Floor(blend.Z)) % 2 == 0 ? a : b;
        }
    }

    public class RadialGradientPattern : Pattern
    {

        public RadialGradientPattern(in Color a, in Color b)
        {
            this.a = a;
            this.b = b;
        }
        public override Color Get(in Point p, params double[] param)
        {
            double dist = Math.Sqrt(p.X * p.X + p.Z * p.Z);
            return a + (b - a) * (dist - Math.Floor(dist));
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
