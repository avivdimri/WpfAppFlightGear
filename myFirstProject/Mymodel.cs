using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace myFirstProject
{
    public class MyModel : InterfaceModel
    {
        Dictionary<string, int> myMap = new Dictionary<string, int>();
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
                aileron = value;
                NotifyPropertyChanged("Aileron");
            }
        }
        public float Elevator
        {
            get { return elevator; }
            set
            {
                elevator = value;
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
                myMap[a] = i;
                j++;
            }
            cols = j;

        }

        public float findElement(string feature)
        {
            int index = myMap[feature];
            List<float> list = myData.ElementAt(index);
            return list.ElementAt(IndexRow);
        }
        public void check(string feature)
        {
            int index = myMap[feature];
            List<float> list = myData.ElementAt(index);

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

            setMainGraphList(mainGraphName);
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



        public Dictionary<string, int> ColumnMap
        {
            get
            {
                return myMap;
            }
        }


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



        public void setMainGraphList(string column)
        {
            List<float> cornentColumn = myData[myMap[column]];
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

        }
        public Dictionary<string, string> pearson()
        {
            animaly_detection anomaly = new animaly_detection();
            Dictionary<string, string> my_dictionary = new Dictionary<string, string>();
            int i;
            for (i = 0; i < myMap.Count; i++)
            {

                List<float> feathre_one = myData.ElementAt(i);
                float max = 0;
                int index = 0;
                for (int j = 0; j < myMap.Count; j++)
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
                my_dictionary.Add(myMap.ElementAt(i).Key, myMap.ElementAt(index).Key);
            }
            return my_dictionary;
        }

    }
}
