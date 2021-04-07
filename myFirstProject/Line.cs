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
            get
            {
                return a;
            }
            set
            {
                a = value;
            }
        }
        public float B
        {
            get
            {
                return  b;
            }
            set
            {
                b = value;
            }
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

        public float X_line(float y)
        {
            return (y - b) / a;
        }
    }

    public class Point
    {
        float x, y;


        public float X
        {
            get {
                return x;
                }

            set {
                x = value;
            }
        }
        public float Y
        {
            get
            {
                return y;
            }
            set {
                y = value;
            }
        }
        public Point(float x, float y) {
           this.x = x;
            this.y = y;
        } 

    }
}
