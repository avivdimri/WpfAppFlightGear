using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFirstProject
{
    //The class holds 2 points.
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
                return b;
            }
            set
            {
                b = value;
            }
        }
        //constructor
        public Line()
        {
            a = 0;
            b = 0;
        }
        //constructor
        public Line(float a, float b)
        {
            this.a = a;
            this.b = b;
        }

        //The method accepts X and returns the corresponding
        //Y according to the equation of the line
        public float f(float x)
        {
            return a * x + b;
        }

        //The method accepts y and returns the corresponding
        //X according to the equation of the line
        public float X_line(float y)
        {
            return (y - b) / a;
        }
    }
    //The point class holds 2 float
    public class Point
    {
        float x, y;


        public float X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }
        public float Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        //constructor
        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

    }
}
