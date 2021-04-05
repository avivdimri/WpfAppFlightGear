using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFirstProject
{
    public class Line
    {

       private float a, b;

        public float A
        {
            get;
            set;
        }
        public float B
        {
            get;
            set;
        }

       public Line()
        {
            a = 0;
            b = 0;
        }
        public Line(float a, float b)
        {
            this.a = a;
            this.b = b;
        }
        public float f(float x)
        {
            return a * x + b;
        }
    }

    public class Point
    {
        float x, y;


        public float X
        {
            get;
            set;
        }
        public float Y
        {
            get;
            set;
        }
        public Point(float x, float y) {
           this.x = x;
            this.y = y;
        } 

    }
}
