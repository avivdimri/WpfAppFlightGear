using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMinCircleLib
{
   public class MinCircle
    {
        private static int my = 0;

        //
        // Created by yosi on 29.12.2020.
        //

        //distance function between 2 points.
        double dis(Point p1, Point p2)
        {
          
            double mult_Y = Math.Pow(p2.Y - p1.Y, 2);
            double mult_X = Math.Pow(p2.X - p1.X, 2);
            return Math.Sqrt(mult_X + mult_Y);
        }

        //return the mid of value of tow points.
        float midPoint(float x1, float x2)
        {
            return (x1 + x2) / 2;
        }

        //return the vertical slope of a slope between tow points.
        float verticalSlope(Point A, Point B)
        {
            return -1 * (1 / ((B.Y - A.Y) / (B.X - A.X)));
        }

        //return the value of the free member b in the equation.
        float getB(float slant, Point first)
        {
            return first.Y - (slant * first.X);
        }

        //same about y.
        float getY(float x, float b, float slant)
        {
            return ((slant * x) + b);
        }

        //return if the point is inside the circle-mining the dis is smaller than the radius.
        public bool findInCircle(Point p, Circle circle)
        {
            double my_dis = dis(p, circle.Center);
            return my_dis <= circle.Radius;
        }

        //the main function that we run - created a vector of point and return the min circle.
      public  Circle findMinCircle(List<Point> list, int size)
        {

         
            List<Point> my_points = new List<Point>();
            my = 0;
           // int s = size / 3;
            return minPoints(list, my_points, size);
        }
        public Circle findMinCircle_with_null(List<Point> list, int size)
        {


            List<Point> my_points = new List<Point>();
            my = 0;
            // int s = size / 3;
            return minPoints_with_null(list, my_points, size);
        }
        public Circle minPoints_with_null(List<Point> arr, List<Point> my_points, int size)
        {
            my++;

            if (my > 200000)
            {
                return minBorad(my_points);
            }

            //stop condition
            if ((size == 1) || (my_points.Count() == 3))
            {
                return minBorad(my_points);
            }
            //keep one of the points - that in the middle.
            // Console.WriteLine("the size of emlement is : " + size);
            //// Console.WriteLine(size);
            Point point = new Point(arr.ElementAt(size - 1).X, arr.ElementAt(size - 1).Y);
            //Point point = Point(points[size - 1]->x, points[size - 1]->y);
            //call the function recursive
            Circle min_circle = minPoints(arr, my_points, size - 1);
            //checking if the point in inside the circle
            if (findInCircle(point, min_circle))
            {
                return min_circle;
            }
            //add the point to the list and call the function again.
            my_points.Add(point);
            // border.push_back(point);
            return minPoints(arr, my_points, size - 1);
        }

        /*
         * this function is the recursive algorithm - checking if the size of the points is 0,
         * or the vector with the points is equal to 3,so we can stop - because we can know which center we need -
         * according 3 points on the border of the circle - by the sentence "all the middle verticals of a triangle
         * are meeting in the center of the circle - meaning our circle that we looking for.
         * so the algo will be - erase one of the points from the list and keep it,run the function again with one point less -
         * when we back from the calling check if the point is inside the circle - if it's ok - keep going back,
         * if not - add the point to the list of border - that mean the point is one that we are looking for.
         * and than run the function again with the point inside the vector of the border.
         */
        public  Circle minPoints(List<Point> arr, List<Point> my_points, int size)
        {
            my++;

            
    
            //stop condition
            if ((size == 1) || (my_points.Count() == 3))
            {
                return minBorad(my_points);
            }
            //keep one of the points - that in the middle.
            // Console.WriteLine("the size of emlement is : " + size);
           //// Console.WriteLine(size);
            Point point = new Point(arr.ElementAt(size-1).X, arr.ElementAt(size-1).Y);
            //Point point = Point(points[size - 1]->x, points[size - 1]->y);
            //call the function recursive
            Circle min_circle = minPoints(arr, my_points, size - 1);
            //checking if the point in inside the circle
            if (findInCircle(point, min_circle))
            {
                return min_circle;
            }
            //add the point to the list and call the function again.
            my_points.Add(point);
           // border.push_back(point);
            return minPoints(arr, my_points, size - 1);
        }
       

        //stop condition - check how much points we have in the border vector (the one we initialize in the begin.)
        //according to the size - return the min circle.
        public Circle minBorad(List<Point> border)
        {
            if (border.Count()==0)
            {
                return  new Circle(new Point(0, 0), 0);
            }
            if (border.Count() == 1)
            {
                return new Circle( border[0], 0 );
            }
            //return circle by 2 points.
            if (border.Count() == 2)
            {
                return towPointsCircle(border[0], border[1]);
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = i + 1; j < 3; j++)
                {

                    Circle c = towPointsCircle(border[i], border[j]);
                    if (is_valid_circle(c,border)) {
                        return c;
                    }
                }
            }
            //3 points
            return threePointsCircle(border[0], border[1], border[2]);
        }

        public bool is_valid_circle( Circle c, List<Point> P) {

            foreach (Point p in P)
                if (!findInCircle(p, c))
                    return false;

            return true;
        }

    // take tow point - find the middle that will be the center,and find the radius with dis between them.
    Circle towPointsCircle(Point P1, Point P2)
        {
            float x = midPoint(P1.X, P2.X);
            float y = midPoint(P1.Y, P2.Y);
            Point center=new Point(x, y);
            double radius = (dis(P1, P2)) / 2;
            return new Circle(center, radius) ;

        }

        /*
         * this function create a circle base on 3 points only -
         * find the tow lines that create a triangle
         * find the middle of the lines - and the vertical line of him
         * find the intersection point of the tow lines and that will be the center.
         */
        Circle threePointsCircle(Point A, Point B, Point C)
        {
            //mid point of A and B and the slope of them.
            Point mid_ab=new Point(midPoint(A.X, B.X), midPoint(A.Y, B.Y));
            float slope_ab = verticalSlope(A, B);
            //same about B and C
            Point mid_bc = new Point(midPoint(B.X, C.X), midPoint(B.Y, C.Y));
            float slope_bc = verticalSlope(B, C);
            //find the free members of the lines.
            float b = getB(slope_ab, mid_ab);
            float c = getB(slope_bc, mid_bc);
            //find the x and the y of the intersection point.
            float x = (c - b) / (slope_ab - slope_bc);
            float y = getY(x, b, slope_ab);
            Point center =new Point(x, y);
            double radius = dis(center, A);
            return  new Circle( center, radius) ;
        }

    }
}

