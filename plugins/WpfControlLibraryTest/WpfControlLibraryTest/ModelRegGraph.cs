using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfControlLibraryTest
{
    public class ModelRegGraph: INotifyPropertyChanged
    {

       
        private anomaly_detection_util anomaly=new anomaly_detection_util();
        private Line line;

        public event PropertyChangedEventHandler PropertyChanged;


        private List<DataPoint> points =null;

        
        //All the points 
        public List<DataPoint> Points
        {
            set 
            {
                points = value;
                NotifyPropertyChanged("Points");

            }
            get
            {
                return points;
            }
        }

        private List<DataPoint> corrent_points;

        //the current points
        public List<DataPoint> Corrent_points
        {
            get {
                return corrent_points;
            }
            set {
                corrent_points = value;
                NotifyPropertyChanged("Corrent_points");

            }
        }
        //the dev points

        private List<DataPoint> dev_point;
        public List<DataPoint> Dev_point
        {
            set
            {
                dev_point = value;
                NotifyPropertyChanged("Dev_point");
             

            }
            get
            {
                return dev_point;
            }
        }

        //the line reg of the features
        private List<DataPoint> reg_line;
        public List<DataPoint> Reg_line
        {
            set
            {
                reg_line = value;
                NotifyPropertyChanged("Reg_line");
                
            }
            get
            {
                return reg_line;
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

        public ModelRegGraph()
        { }

        //update the time 
        public void update_time(int index)
        {
            if (points != null)
            {
                List<DataPoint> timePoints = new List<DataPoint>();

                for (int i = index; i < index + 50; i++)
                {
                    if (i < points.Count())
                    {
                        timePoints.Add(points[i]);
                    }
                }

                Corrent_points = timePoints;
            }
        }
        //update the grafh of points of the features.

        public void update_lists(List<float> p1_train, List<float> p2_train, List<float> p1_test, List<float> p2_test)
        {
            List<DataPoint> tepm_dev_point = new List<DataPoint>();
            float max = get_threshold(p1_train, p2_train);

            List<Point> listPoint_test = create_point_list(p1_test, p2_test);

            int[] dev_arr = new int[listPoint_test.Count()];
            int i = 0;

            foreach (Point p in listPoint_test)
            {
                float dev = anomaly.dev(p, line);
                if (dev > max)
                {
                    tepm_dev_point.Add(new DataPoint(p.X, p.Y));
                    dev_arr[i] = 1;
                }
                else
                {
                    dev_arr[i] = 0;
                }
                i++;
            }
            this.Dev_point = tepm_dev_point;
            this.Points = create_data_point_list(p1_test, p2_test);

            setLineReg(p1_train, p2_train);
            SliderPoints = createSliderPoints(420, listPoint_test.Count(), dev_arr);

        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        //create a list of points
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
        //create a list of datapoints

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


        //finding the max normal dev of the line reg in the train files.
        public float get_threshold(List<float> p1,List<float> p2)
        {
            List<Point> points_train = create_point_list(p1,p2);
            Point[] points = points_train.ToArray();
            line = anomaly.linear_reg(points, points_train.Count);
            float max_dev = 0;
            foreach (Point p in points_train)
            {
                float temp = anomaly.dev(p, line);
                if (temp > max_dev)
                {
                    max_dev = temp;
                }
            }
            return max_dev;

        }
        //create a line reg
        public void setLineReg(List<float> p1, List<float> p2)
        {

            float[] one_X = p1.ToArray();
            float[] tow_Y = p2.ToArray();

            Point[] ps = new Point[one_X.Length];
            List<DataPoint> temp_points = new List<DataPoint>();


            for (int i = 0; i < p1.Count; ++i)
            {
                ps[i] = new Point(one_X[i], tow_Y[i]);
            }
           
            Line l = anomaly.linear_reg(ps, ps.Length);

            List<DataPoint> cor_point = new List<DataPoint>();

            for (int i = 0; i < p1.Count; ++i)
            {
                cor_point.Add(new DataPoint(l.X_line(p2[i]), p2[i]));
                cor_point.Add(new DataPoint(p1[i], l.f(p1[i])));
            }
            Reg_line = cor_point;
        }
        //create a slider of date points.
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
