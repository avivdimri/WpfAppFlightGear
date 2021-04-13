using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMinCircleLib
{
   public class Circle
    {
        private Point center;
        private double radius;

        public Circle(Point p, double r)
        {
            center = p;
            radius = r;
        }
        public Point Center
        {
            set
            {
                center = value;
            }
            get
            {
                return center;
            }
        }
        public double Radius
        {
            set
            {
                radius = value;
            }
            get
            {
                return radius;
            }
        }
    }

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
        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }


    }
}
