using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.UI.MobileControls;
using System.Xml;
using System.Xml.Linq;

namespace myFirstProject
{
    public class MyModel : InterfaceModel
    {
        Dictionary<string, int> myMap = new Dictionary<string, int>();
        List<string> myfeatures = new List<string>();
        private animaly_detection anomaly = new animaly_detection();
        List<List<float>> data_train;
        List<List<float>> data_test;
        private int cols;
        //private int rows;
        private LinkedList<string> mylist_train = new LinkedList<string>();
        private LinkedList<string> mylist_test = new LinkedList<string>();
        private InterfaceClient client;
        private int speedsend = 50;
        private bool stop;
        private bool pausePressed;
        private int indexRow = 0;
        private int numRows = 1;
        private float aileron=125;
        private float elevator=125;
        private float rudder;
        private float throttle;
        private float altimeter;
        private float airspeed;
        private float roll;
        private float direction;
        private float pitch;
        private float yaw;
        private string time = "00:00:00";
        private dynamic dynamic_load=null;

        public dynamic Dynamic_load
        {
            get
            {
                return dynamic_load;
            }
            set
            {
                dynamic_load = value;
                NotifyPropertyChanged("Dynamic_load");
            }
        }

        //public List<List<float>> getmydata()
        //{
        //    return data_train;
        //}
        public List<List<float>> Data_train
        {
            set
            {
                data_train = value;
                NotifyPropertyChanged("Data_train");

            }
            get
            {
                return data_train;
            }
        }

        public List<List<float>> Data_test
        {
            set
            {
                data_test = value;
                NotifyPropertyChanged("Data_test");

            }
            get
            {
                return data_test;
            }
        }
        public float Airspeed
        {
            set
            {
                airspeed = value;
                NotifyPropertyChanged("Airspeed");
            }
            get
            {
                return airspeed;
            }
        }
        public float Roll
        {
            set
            {
                roll = value;
                NotifyPropertyChanged("Roll");
            }
            get
            {
                return roll;
            }
        }
        public float Direction
        {
            set
            {
                direction = value;
                NotifyPropertyChanged("Direction");
            }
            get
            {
                return direction;
            }
        }
        public float Pitch
        {
            set
            {
                pitch = value;
                NotifyPropertyChanged("Pitch");
            }
            get
            {
                return pitch;

            }
        }
        public float Yaw
        {
            set
            {
                yaw = value;
                NotifyPropertyChanged("Yaw");
            }

            get
            {
                return yaw;
            }
        }
        public float Altimeter
        {
            get { return altimeter; }
            set
            {
                altimeter = value;
                NotifyPropertyChanged("Altimeter");
            }
        }

        public int NumRows
        {
            get
            {
                return numRows;
            }
            set
            {
                numRows = value;
                NotifyPropertyChanged("NumRows");
            }

        }
        public int Speedsend
        {
            set
            { speedsend = value; }
            get
            { return speedsend; }
        }

        public int IndexRow
        {
            get { return indexRow; }
            set
            {
                indexRow = value;
                NotifyPropertyChanged("IndexRow");
            }
        }
        public float Aileron
        {
            get { return aileron; }
            set
            {
                aileron = 125 + 100 * value;
                NotifyPropertyChanged("Aileron");
            }
        }
        public float Elevator
        {
            get { return elevator; }
            set
            {
                elevator = 125 + 100 * value;
                NotifyPropertyChanged("Elevator");
            }

        }
        public float Rudder
        {
            get
            {
                return rudder;
            }
            set
            {
                rudder = value;
                NotifyPropertyChanged("Rudder");
            }

        }
        public float Throttle
        {
            get
            {
                return throttle;
            }
            set
            {
                throttle = value;
                NotifyPropertyChanged("Throttle");
            }

        }
        public string Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
                NotifyPropertyChanged("Time");
            }

        }


        public MyModel(InterfaceClient the_client)
        {
            client = the_client;
            stop = false;
            pausePressed = false;
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }


        public void readcsvtrainfile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string currentLine;
                currentLine = sr.ReadLine();
                // currentLine will be null when the StreamReader reaches the end of file
                while ((currentLine = sr.ReadLine()) != null)
                {
                    mylist_train.AddLast(currentLine);
                    //currentLine = sr.ReadLine();

                }
                //NumRows = mylist_train.Count - 2;
                //rows = NumRows - 1;
            }
            parser();
            Data_train = createMap(mylist_train);
            
        
        // Data_train=
    }
        public void readcsvtestfile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string currentLine;
                currentLine= sr.ReadLine();
                // currentLine will be null when the StreamReader reaches the end of file
                while ((currentLine = sr.ReadLine()) != null)
                {
                    mylist_test.AddLast(currentLine);
                    //currentLine = sr.ReadLine();

                }
                NumRows = mylist_test.Count - 2;
                //rows = NumRows - 1;
            }
            Data_test = createMap(mylist_test);
        }
        public List<List<float>> createMap(LinkedList<string> temp_list)
        {
            List<List<float>> temp= new List<List<float>>();
           // myData = new List<List<float>>();
            for (int k = 0; k < cols; k++)
            {
                List<float> l = new List<float>();
                temp.Add(l);
               // myData.Add(l);
            }
            string line;
            string[] words;
            float num;
            int j;
            for (int i = 0; i < temp_list.Count; i++)
            {
                line = temp_list.ElementAt(i);
                words = line.Split(',');
                j = 0;
                foreach (List<float> e in temp)
                {
                    string n = words[j++];
                    num = float.Parse(n);
                    e.Add(num);
                }

            }
            return temp;

        }

        public void parser()
        {
            int i, j = 0;
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("C:/Program Files/FlightGear 2020.3.6/data/Protocol/playback_small.xml");
            XmlNodeList name = xdoc.GetElementsByTagName("name");
            for (i = 0; i < name.Count / 2; i++)
            {

                string a = name[i].InnerText;
                myfeatures.Add(a);
                myMap[a] = i;
                j++;
            }
            cols = j;
        }


        public List<float> get_col_train(string col)
        {
            int index =  myfeatures.FindIndex(a => a.Contains(col));
            List<float> list = data_train.ElementAt(index);
            return list;
        }
        public List<float> get_col_test(string col)
        {
            int index = myfeatures.FindIndex(a => a.Contains(col));
            List<float> list = data_test.ElementAt(index);
            return list;
        }

        public float findElement(string feature)
        {
            // int index = myMap[feature];
            int index = myfeatures.FindIndex(a => a.Contains(feature));
            List<float> list = data_train.ElementAt(index);
            return list.ElementAt(IndexRow);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Connect(string ip, int port)
        {
            client.connect(ip, port);

        }

        public void disConnect()
        {
            client.disconnect();
        }

        public void Start()
        {
            isAfterLoad = true;
            pearson();

            new Thread(delegate ()
            {
                while (!stop)
                {
                    if ((!pausePressed) && (IndexRow < numRows))
                    {
                        IndexRow++;
                    }
                    string line = mylist_test.ElementAt(IndexRow);
                    client.write(line);
                    updateData(line);
                    Thread.Sleep(speedsend);
                }

            }).Start();
        }
        private void setTime()
        {
            int miliSeconds = IndexRow % 10;
            int secends = (IndexRow / 10) % 60;
            int minutes = IndexRow / 600;
            Time = (minutes.ToString("D2")) + ":" + (secends.ToString("D2")) + ":" + ((miliSeconds * 10).ToString("D2"));

        }
        public void updateData(string line)
        {
            Aileron = findElement("aileron");
            Elevator = findElement("elevator");
            Rudder = findElement("rudder");
            Throttle = findElement("throttle");
            Altimeter = findElement("altimeter_indicated-altitude-ft");
            Airspeed = findElement("airspeed-kt");
            Roll = findElement("roll-deg");
            Pitch = findElement("pitch-deg");
            Yaw = findElement("side-slip-deg");
            Direction = findElement("heading-deg");
            setMainGraphList(MainGraphName);
            setLineReg();
            setTime();
            //////////////////
            if (dynamic_load != null)
            {

                dynamic_load.time(indexRow);

                //new Thread(delegate ()
                //{
                //    dynamic_load.time(indexRow);
                //});
            }
            ///////////////////////

        }


        
        public void changeSpeed(double speed)
        {
            speedsend = (int)(50 / speed);
        }
        public void changeMood()
        {
            pausePressed = !pausePressed;
        }
        public int get_numRows()
        {
            return numRows;
        }



        /// ///////    Graph handler:  //////  //


        private bool isAfterLoad = false;

        public bool IsAfterLoad
        {
            get
            {
                return isAfterLoad;
            }
        }



        public List<string> ColumnList
        {
            get
            {
                return myfeatures;
            }
        }


        // map of the corelations of all features
        private List<KeyValuePair<string, string>> corelationMap;
        


        private List<DataPoint> mainGraphList;


        public List<DataPoint> MainGraphList
        {
            set
            {
                mainGraphList = value;
                NotifyPropertyChanged("MainGraphList");
            }
            get
            {
                return mainGraphList;
            }
        }

        private string mainGraphName = "aileron";

        public string MainGraphName
        {
            set
            {
                mainGraphName = value;

                secondGraphName = get_element(mainGraphName);

                NotifyPropertyChanged("MainGraphName");

            }
            get
            {
                return mainGraphName;
            }
        }




        private List<DataPoint> secondGraphList;


        public List<DataPoint> SecondGraphList
        {
            set
            {
                secondGraphList = value;
                
                NotifyPropertyChanged("SecondGraphList");
            }
            get
            {
                return secondGraphList;
            }
        }


        private string secondGraphName = "rudder";

        public string SecondGraphName
        {
            set
            {
                secondGraphName = value;
                NotifyPropertyChanged("SecondGraphName");
            }
            get
            {
                return secondGraphName;
            }
        }


        public void setMainGraphList(string column)
        {
            /*** int index = myfeatures.FindIndex(a => a.Contains(column));
             List<float> cornentColumn = myData[index];
             List<DataPoint> newDataPoints = new List<DataPoint>();
             int i = 0;
             foreach (float num in cornentColumn)
             {

                 newDataPoints.Add(new DataPoint(i++, num));
                 if (i > IndexRow)
                 {
                     break;
                 }

             }


             MainGraphList = newDataPoints;
              ***/

            MainGraphList = setGraphList(MainGraphName);

            //SecondGraphName = get_element(column);

            SecondGraphList = setGraphList(SecondGraphName); 


            //setSecondGraphList(SecondGraphName);
           
        }


        public List<DataPoint> setGraphList(string column)
        {
            int index = myfeatures.FindIndex(a => a.Contains(column));
            List<float> cornentColumn = data_test[index];
            List<DataPoint> newDataPoints = new List<DataPoint>();
            int i = 0;
            foreach (float num in cornentColumn)
            {

                newDataPoints.Add(new DataPoint(i++, num));
                if (i > IndexRow)
                {
                    break;
                }

            }
            return newDataPoints;
        }
     

        public void setLineReg()
        {
            
            int index = myfeatures.FindIndex(a => a.Contains(MainGraphName));
            List<float> x_train = data_train[index];
            List<float> x_test = data_test[index];
            float[] one_X =x_train.ToArray();
        ;
            index = myfeatures.FindIndex(a => a.Contains(SecondGraphName));
            List<float> Y_train= data_train[index];
            List<float> y_test = data_test[index];
            float[] tow_Y =Y_train.ToArray();
        

            Point[] ps = new Point[one_X.Length];
            List<DataPoint>temp_points= new List<DataPoint>();


            for (int i = 0; i < x_train.Count; ++i)
            {
                ps[i] = new Point(one_X[i], tow_Y[i]);
                if (i > (indexRow - 300) && i <= indexRow) 
                {
                    temp_points.Add(new DataPoint(x_test.ElementAt(i), y_test.ElementAt(i)));
                }
            }
           // Points = ps;
            Line l =  anomaly.linear_reg(ps, ps.Length);

            List<DataPoint> cor_point = new List<DataPoint>();

            for (int i = 0; i < x_test.Count; ++i)
            {
                cor_point.Add(new DataPoint(l.X_line(y_test.ElementAt(i)), y_test.ElementAt(i)));
                cor_point.Add(new DataPoint(x_test.ElementAt(i), l.f(x_test.ElementAt(i))));
            }


            //   cor_point.Add(new DataPoint(-100, l.f(2)));
            //  cor_point.Add(new DataPoint(300,l.f(4)));
            LineReg = cor_point;
            Points_reg = temp_points;
        }
        public double X_line(double a, double b, double y)
        {
            return (y - b) / a;
        }

        private List<DataPoint> points_reg;

        public List<DataPoint> Points_reg
        {
            set
            {
                points_reg = value;
                NotifyPropertyChanged("Points");
            }
            get
            {
                return points_reg;
            }
        }


        private List<DataPoint> lineReg;
        public List<DataPoint> LineReg
        {
            set
            {
                lineReg = value;

                NotifyPropertyChanged("LineReg");
            }
            get
            {
                return lineReg;
            }
        }

        public string get_element(string col)
        {
            foreach(var i in corelationMap)
            {
                if (i.Key.Equals(col))
                {
                    return i.Value;
                }
                
            }
            return null;
        }


        /***
        public void setSecondGraphList(string column)
        {
            int index = myfeatures.FindIndex(a => a.Contains(column));
            List<float> cornentColumn = myData[index];
            List<DataPoint> newDataPoints = new List<DataPoint>();
            int i = 0;
            foreach (float num in cornentColumn)
            {

                newDataPoints.Add(new DataPoint(i++, num));
                if (i > IndexRow)
                {
                    break;
                }

            }

            SecondGraphName = column;
            SecondGraphList = newDataPoints;

        }
        ***/

       

        public void pearson()
        {
          
            corelationMap = new List<KeyValuePair<string, string>>();
            int i;
            for (i = 0; i < myfeatures.Count; i++)
            {

                List<float> feathre_one = data_train.ElementAt(i);
                float max = 0;
                int index = 0;
                for (int j = 0; j < myfeatures.Count; j++)
                {
                    if (i != j)
                    {
                        float num = 0;
                        List<float> feathre_tow = data_train.ElementAt(j);
                        float[] arr_one = feathre_one.ToArray();
                        float[] arr_tow = feathre_tow.ToArray();
                       
                        num = Math.Abs(anomaly.pearson(arr_one, arr_tow, arr_one.Length));
                        if (num > max)
                        {
                            max = num;
                            index = j;
                        }
                    }
                }
                corelationMap.Add(new KeyValuePair <string,string>( myfeatures.ElementAt(i), myfeatures.ElementAt(index)));
              
            }
        }

      

    }

}
