using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfMinCircleLib
{
    
    public class ModelGrafh : INotifyPropertyChanged
    {
        private Circle circle;
        private List<DataPoint> center;
        private double radius;
        List<DataPoint> dev_list_points;
        List<DataPoint> my_list_point;
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public ModelGrafh()
        {

        }
        private List<DataPoint> corrent_points;

        public List<DataPoint> Corrent_points
        {
            get
            {
                return corrent_points;
            }
            set
            {
                corrent_points = value;
                NotifyPropertyChanged("Corrent_points");

            }
        }


        public List<DataPoint> Center
        {
            set
            {
                center = value;
                NotifyPropertyChanged("Center");

            }
            get
            {
                return center;
            }
        }
       
        public List<DataPoint> Dev_list_points
        {
            set
            {
                dev_list_points = value;
                NotifyPropertyChanged("Dev_list_points");
            }
            get
            {
                return dev_list_points;
            }
        }
        public List<DataPoint> My_list_point
        {
            set
            {
                my_list_point = value;
                NotifyPropertyChanged("My_list_point");
                Console.WriteLine("set:My_list_point ");
            }
            get
            {
                
                return my_list_point;
            }
        }

        //the slider points that repesent the dev in the slider.
        private List<DataPoint> sliderPoints;
        public List<DataPoint> SliderPoints
        {
            set
            {
                sliderPoints = value;
                NotifyPropertyChanged("SliderPoints");
            }
            get
            {
                return sliderPoints;
            }
        }

        private List<DataPoint> sliderBorder;
        public List<DataPoint> SliderBorder
        {
            set
            {
                sliderBorder = value;
                NotifyPropertyChanged("SliderBorder");
            }
            get
            {
                return sliderBorder;
            }
        }

        public void update_lists(List<float> p1_train, List<float> p2_train, List<float> p1_test, List<float> p2_test)
        {
            if ((p1_train.Count() == 0) || (p2_train.Count() == 0))
            {
               
                List<DataPoint> empty = new List<DataPoint>();
                Center = empty;
                Dev_list_points = empty;
                My_list_point = empty;
                corrent_points = empty;


            }
            else
            {
               
                List<Point> my_point_train = create_point_list(p1_train, p2_train);
                float min_X = my_point_train.ElementAt(0).X;
                float min_Y = my_point_train.ElementAt(0).Y;
                float max_X = my_point_train.ElementAt(0).X;
                float max_Y = my_point_train.ElementAt(0).Y;
                foreach (Point p in my_point_train)
                {
                    if (p.X < min_X)
                    {
                        min_X = p.X;
                    }
                    if (p.Y < min_Y)
                    {
                        min_Y = p.Y;
                    }
                    else if (p.X > max_X)
                    {
                        max_X = p.X;
                    }
                    else if (p.Y > max_Y)
                    {
                        max_Y = p.Y;
                    }
                }
                Point min_p = new Point(min_X, min_Y);
                Point max_p = new Point(max_X, max_Y);
                double radius = (dis(min_p, max_p)) / 2;
                float center_X = midPoint(min_p.X, max_p.X);
                float center_Y = midPoint(min_p.Y, max_p.Y);
                circle = new Circle(new Point(center_X, center_Y), radius);

                List<DataPoint> temp_center = createCircle(circle.Center.X, circle.Center.Y, circle.Radius);

                Center = temp_center;
                My_list_point = create_data_point_list(p1_test, p2_test);
                List<Point> my_point_test = create_point_list(p1_test, p2_test);
                Dev_list_points = findingdev(my_point_test);
            }
        }
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

        public List<Point> create_point_list(List<float> p1, List<float> p2)
        {
            List<Point> listPoint = new List<Point>();
            for (int i = 0; i < p1.Count; i++)
            {
                Point p = new Point(p1.ElementAt(i), p2.ElementAt(i));
                listPoint.Add(p);
            }
            return listPoint;

        }
     
        public List<DataPoint> create_data_point_list(List<float> p1, List<float> p2)
        {
            List<DataPoint> listDataPoint = new List<DataPoint>();
            for (int i = 0; i < p1.Count; i++)
            {
                DataPoint p = new DataPoint(p1.ElementAt(i), p2.ElementAt(i));
                listDataPoint.Add(p);
            }
            return listDataPoint;

        }
        public List<DataPoint> findingdev(List<Point> my_p)
        {
            List<DataPoint> temp_data = new List<DataPoint>();

            int[] dev_arr = new int[my_p.Count()];
            int i = 0;

            foreach (Point p in my_p)
            {
                if (!(findInCircle(p, circle))){
                    temp_data.Add(new DataPoint(p.X, p.Y));
                    dev_arr[i] = 1;
                }
                else
                {
                    dev_arr[i] = 0;
                }
                i++;
            }
            SliderPoints = createSliderPoints(420, my_p.Count(), dev_arr);
            return temp_data;
        }
        public List<DataPoint> createCircle(double a, double b, double r)
        {

            List<DataPoint> list = new List<DataPoint>();

            for (double i = 0; i < Math.PI * 2; i += 0.001)
            {
                double x = a + (r * Math.Cos(i));
                double y = b + (r * Math.Sin(i));
                list.Add(new DataPoint(x, y));
            }

            return list;
        }
        public bool findInCircle(Point p, Circle circle)
        {
            double my_dis = dis(p, circle.Center);
            return my_dis <= circle.Radius*1.1;
        }
        public void update_time(int index)
        {
            if (my_list_point != null)
            {
                List<DataPoint> timePoints = new List<DataPoint>();

                for (int i = index; i < index + 50; i++)
                {
                    if (i < my_list_point.Count())
                    {
                        timePoints.Add(my_list_point[i]);
                    }
                }

                Corrent_points = timePoints;
            }
        }
        private List<DataPoint> createSliderPoints(int width, int rows, int[] dev_arr)
        {
            List<DataPoint> list = new List<DataPoint>();

            for (int i = 0; i < rows; i++)
            {
                if (dev_arr[i] == 1)
                {
                    list.Add(new DataPoint(i, 0));
                }
                

            }
           
            List<DataPoint> border = new List<DataPoint>();
            border.Add(new DataPoint(0, 0));
            border.Add(new DataPoint(rows, 0));
            SliderBorder = border;
            return list;
        }
    }
}
