using System;
using System.Collections.Generic;
using System.Linq;


namespace RayTracingCS
{

    public struct Ray
    {
        public Point origin;
        public Vector direction;

        public Ray(in Point o, in Vector d)
        {
            origin = o;
            direction = d;
        }

        public Point position(double t)
        {
            return direction * t + origin;
        }

        public Ray Transformed(in Mat4 transformation)
        {
            return new Ray(transformation * origin, transformation * direction);
        }
    }

    public struct Computations
    {
        public double t;
        public HitObject obj;
        public Point point;
        public Point over_point;
        public Point under_point;
        public Vector eye;
        public Vector normal;
        public Vector reflect;
        public bool inside;

        public double n1, n2;
        public Computations(double t, in HitObject obj, in Point p, in Vector eye, in Vector norm, bool inside)
        {
            this.t = t;
            this.obj = obj;
            this.point = p;
            this.eye = eye;
            this.normal = norm;
            this.inside = inside;
            this.over_point = p;

            over_point  = p + normal * (MatMaths.eps);
            under_point = p - normal * (MatMaths.eps);
            reflect = Vector.Reflect(-eye, normal);

            n1 = n2 = 0;
        }


        public double Schlick()
        {
            double cos = eye.Dot(normal);

            if(n1>n2)
            {
                double n = n1 / n2;
                double sin_2t = n * n * (1 - cos * cos);

                if (sin_2t > 1.0)
                    return 1.0;

                double cos_t = Math.Sqrt(1.0 - sin_2t);
                cos = cos_t;
            }

            double r0 = Math.Pow((n1 - n2) / (n1 + n2), 2);
            return r0 + (1 - r0) * Math.Pow(1-cos, 5);
        }
    }



    public class Intersection
    {
        public HitObject? obj;
        public double t;
        public Intersection(in HitObject obj, double t)
        {
            this.obj = obj;
            this.t = t;
        }

        public override bool Equals(object obj)
        {
            if (obj is Intersection p)
            {
                return this.obj == p.obj
                    && t == p.t;
            }

            return false;
        }

        public static Intersection Hit(in List<Intersection> xs)
        {
            if (xs == null) return null;


            xs.Sort((Intersection a, Intersection b) => MatMaths.SpaceshipOp(a.t, b.t));

            for (int i = 0; i < xs.Count; i++)
                if (xs[i].t > 0d)
                    return xs[i];

            return null;
        }

        public Computations Compute(in Ray r, in List<Intersection> xs = default(List<Intersection>))
        {

            Point pos   = r.position(t);
            Vector norm = obj.NormalAt(pos);
            bool inside;
                
            if (norm.Dot(-r.direction) < 0)
            {
                inside = true;
                norm = -norm;
            }
            else
                inside = false;

            Computations retVal =  new Computations(t, obj, pos, -r.direction.Normalized(), norm.Normalized(), inside);


            
            if (xs == null)
                return retVal;



            List<HitObject> containers = new List<HitObject>();

            for (int i = 0; i < xs.Count; i++)
            {
                if (this == xs[i])
                {
                    if(containers.Count == 0)
                        retVal.n1 = 1.0;
                    else
                        retVal.n1 = containers.Last().material.refraction;
                }

                if(containers.Contains(xs[i].obj))
                    containers.Remove(xs[i].obj);
                else
                    containers.Add(xs[i].obj);

                if (this == xs[i])
                {
                    if (containers.Count == 0)
                        retVal.n2 = 1.0;
                    else
                        retVal.n2 = containers.Last().material.refraction;
                    break;
                }
            }

            return retVal;
        }
    }


    public class World
    {
        const int depth = 5;

        // Possibly a dictionary with IDs
        public LinkedList<HitObject> objects    = new LinkedList<HitObject>();
        public List<Light> lights               = new List<Light>();

        public World(params HitObject[] objs)
        {
            for (int i = 0; i < objs.Length; i++)
                objects.AddLast(objs[i]);
        }
        public World()
        {
            PointLight light = new PointLight(new Point(-10, 10, -10), new Color(1, 1, 1));
            Sphere s1 = new Sphere();
            Sphere s2 = new Sphere();

            s1.material.color = new Color(0.8f, 1.0f, 0.6f);
            s1.material.diffuse = 0.7f;
            s1.material.specular = 0.2f;

            s2.Transformation = MatMaths.Scaling(0.5, 0.5, 0.5);


            lights.Add(light);
            objects.AddLast(s1);
            objects.AddLast(s2);
        }



        public List<Intersection> Intersect(in Ray r)
        {
            List<Intersection> retVal = new List<Intersection>();
            foreach (HitObject item in objects)
            {
                List<Intersection> xs = item.IntersectionsWith(r);
                if (xs.Count != 0)
                    retVal.AddRange(xs);
            }

            retVal.Sort((Intersection a, Intersection b) => MatMaths.SpaceshipOp(a.t, b.t));
            return retVal;
        }

        public Color Shading(in Computations comp, int remaining)
        {
            Color retVal = Color.Black;

            int lightIndex = 0;
            foreach (Light light in lights)
            {
                bool shaded = IsShadowed(comp.over_point, lightIndex);
                retVal += light.Lighting(comp.obj.material, comp.obj, comp.point, comp.eye, comp.normal, shaded);

                lightIndex++;
            }

            Color reflected = ReflectiveShading(comp, remaining);
            Color refracted = RefractiveShading(comp, remaining);

            Material mat = comp.obj.material;

            if(mat.reflective > 0 && mat.transparency > 0)
            {
                double reflectance = comp.Schlick();
                return retVal + (reflected * reflectance) + (refracted * (1 - reflectance));
            }

            return retVal + reflected + refracted;
        }
        public Color ReflectiveShading(in Computations comp, int remaining)
        {
            if (comp.obj.material.reflective == 0 || remaining <=0)
                return new Color(0, 0, 0);


            Ray reflect_ray = new Ray(comp.over_point, comp.reflect);
            Color color = Coloring(reflect_ray, remaining - 1);

            return color * comp.obj.material.reflective;
        }
        public Color RefractiveShading(in Computations comp, int remaining)
        {

            if (comp.obj.material.transparency == 0 || remaining <= 0)
                return new Color(0, 0, 0);


            double ratio = comp.n1 / comp.n2;
            double cos_i = MatMaths.Dot(comp.eye, comp.normal);
            double sin_2t = ratio * ratio * (1.0 - cos_i * cos_i);

            if (sin_2t > 1)
                return new Color(0, 0, 0);

            double cos_t = Math.Sqrt(1.0 - sin_2t);
            Vector dir = comp.normal * (ratio * cos_i - cos_t) - comp.eye * ratio;

            Ray ref_ray = new Ray(comp.under_point, dir);

            return Coloring(ref_ray, remaining - 1) * comp.obj.material.transparency;
        }
    
        public Color Coloring(in Ray r, int remaining = depth)
        {
            List<Intersection> xs = Intersect(r);
            Intersection hit = Intersection.Hit(xs);


            if (hit == null)
            {
                return Color.Black;
            }
            else {
                Computations comp = hit.Compute(r, xs);
                return Shading(comp, remaining);
            }
        }

        
        public bool IsShadowed(in Point p, int lightIndex)
        {
            Vector v = lights[lightIndex].pos - p;

            double distance = v.Magnitude();
            Vector direction = v.Normalized();

            Ray r = new Ray(p, direction);

            List<Intersection> xs = Intersect(r);
            Intersection h = Intersection.Hit(xs);

            if (h != null && h.t < distance)
                return true;
            else
                return false;
        }
    }


}
