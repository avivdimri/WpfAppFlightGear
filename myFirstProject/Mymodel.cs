﻿using OxyPlot;
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
        List<List<float>> myData;
        private int cols;
        private int rows;
        private LinkedList<string> mylist = new LinkedList<string>();
        private InterfaceClient client;
        private int speedsend = 50;
        private bool stop;
        private bool pausePressed;
        private int indexRow = 0;
        private int numRows = 1;
        private float aileron;
        private float elevator;
        private float rudder;
        private float throttle;
        private float altimeter;
        private float airspeed;
        private float roll;
        private float direction;
        private float pitch;
        private float yaw;
        private string time = "00:00:00";

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


        public void readcsvfile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string currentLine;
                // currentLine will be null when the StreamReader reaches the end of file
                while ((currentLine = sr.ReadLine()) != null)
                {
                    mylist.AddLast(currentLine);
                    currentLine = sr.ReadLine();

                }
                NumRows = mylist.Count - 2;
                rows = NumRows - 1;

            }
        }
        public void createMap()
        {
            myData = new List<List<float>>();
            for (int k = 0; k < cols; k++)
            {
                List<float> l = new List<float>();
                myData.Add(l);
            }
            string line;
            string[] words;
            float num;
            int j;
            for (int i = 0; i < mylist.Count; i++)
            {
                line = mylist.ElementAt(i);
                words = line.Split(',');
                j = 0;
                foreach (List<float> e in myData)
                {
                    string n = words[j++];
                    num = float.Parse(n);
                    e.Add(num);
                }

            }

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

        public float findElement(string feature)
        {
            // int index = myMap[feature];
            int index = myfeatures.FindIndex(a => a.Contains(feature));
            List<float> list = myData.ElementAt(index);
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
                    string line = mylist.ElementAt(IndexRow);
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
            setSecondGraphList(SecondGraphName);
            setTime();
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

            MainGraphName = column;
            MainGraphList = newDataPoints;
            SecondGraphName = get_element(column);
            setSecondGraphList(SecondGraphName);
            setLineReg();
            //setPoints_reg();
        }
        public void setLineReg()
        {
            
            int index = myfeatures.FindIndex(a => a.Contains(MainGraphName));
            List<float> X = myData[index];
            float[] one_X =X.ToArray();

             index = myfeatures.FindIndex(a => a.Contains(SecondGraphName));
            List<float> Y= myData[index];
            float[] tow_Y =Y.ToArray();
            Point[] ps = new Point[one_X.Length];
            List<DataPoint>temp_points= new List<DataPoint>();

            for (int i = 0; i < X.Count; ++i)
            {
                ps[i] = new Point(one_X[i], tow_Y[i]);
                if(i>(indexRow-300) && i <= indexRow)
                {
                    temp_points.Add(new DataPoint(one_X[i], tow_Y[i]));
                }
            }
           // Points = ps;
            Line l =  anomaly.linear_reg(ps, ps.Length);

            List<DataPoint> cor_point = new List<DataPoint>();

            for (int i = 0; i < X.Count; ++i)
            {
                cor_point.Add(new DataPoint(X[i], l.f(X[i])));
            }


         //   cor_point.Add(new DataPoint(-100, l.f(2)));
          //  cor_point.Add(new DataPoint(300,l.f(4)));
            
            LineReg = cor_point;
            Points_reg = temp_points;
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
        public void pearson()
        {
          
            corelationMap = new List<KeyValuePair<string, string>>();
            int i;
            for (i = 0; i < myfeatures.Count; i++)
            {

                List<float> feathre_one = myData.ElementAt(i);
                float max = 0;
                int index = 0;
                for (int j = 0; j < myfeatures.Count; j++)
                {
                    if (i != j)
                    {
                        float num = 0;
                        List<float> feathre_tow = myData.ElementAt(j);
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
            StreamWriter sw = new StreamWriter("C:/Users/yosef/Source/Repos/WpfDesktopAPP/Test.txt");
          
            foreach(KeyValuePair<string, string> k in corelationMap)
            {
                sw.Write(k.Key);
                sw.Write(" -- ");
                sw.WriteLine(k.Value);
            }
           
            sw.Close();
            //corelationMap.Add(myMap.ElementAt(i).Key, myMap.ElementAt(index).Key);




        }

    }

}
