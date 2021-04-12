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
       
        private animaly_detection anomaly = new animaly_detection();

        private LinkedList<string> mylist_train = new LinkedList<string>();
        private LinkedList<string> mylist_test = new LinkedList<string>();
        private InterfaceClient client;

        private bool stop = false;
        private bool pausePressed = false;

        //propert of the dynamic of the dll
        private dynamic dynamic_load = null;
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

        //propert of the Data train file
        List<List<float>> data_train;
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

        //propert of the Data test file
        List<List<float>> data_test;
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




        // The constructor gets client which responsible on the connection with the FG.
        public MyModel(InterfaceClient the_client)
        {
            client = the_client;
        }

        // The function go over of all her observers and send them the notificaton.
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        // The function gets path to csv file of train flight,  parse and save the data of it.
        public void readCsvTrainFile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string currentLine;
                currentLine = sr.ReadLine();
                // currentLine will be null when the StreamReader reaches the end of file.
                while ((currentLine = sr.ReadLine()) != null)
                {
                    mylist_train.AddLast(currentLine);
                }
            }
            ParseXml();
            Data_train = createMap(mylist_train);

        }

        // The function gets path to csv file of test flight, parser and save the data of it.
        public void readCsvTestFile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string currentLine;
                currentLine = sr.ReadLine();
                // currentLine will be null when the StreamReader reaches the end of file
                while ((currentLine = sr.ReadLine()) != null)
                {
                    mylist_test.AddLast(currentLine);
                }
                NumRows = mylist_test.Count - 2;
            }
            Data_test = createMap(mylist_test);
        }

        // The function gets list which contain data of the rows. The function divides
        // the rows to cols by the features and enter to  data structure(list of list) which she build.
        public List<List<float>> createMap(LinkedList<string> data_by_rows)
        {
            List<List<float>> Data = new List<List<float>>();
            int size = myfeatures.Count;
            // build the data structure which wil contain the the data int the list the function gets 
            for (int k = 0; k < size; k++)
            {
                List<float> l = new List<float>();
                Data.Add(l);
            }
            string line;
            string[] words;
            float num;
            int j;
            size = data_by_rows.Count;
            // go over all the rows of the 
            for (int i = 0; i < size; i++)
            {
                line = data_by_rows.ElementAt(i); //the row number i
                words = line.Split(',');
                j = 0;
                // parse the row line and enter to the data structue 
                foreach (List<float> e in Data)
                {
                    string n = words[j++];
                    num = float.Parse(n);
                    e.Add(num);
                }

            }
            return Data;

        }

        //The function gets path of XML file and gets from it the list of the flight features.
        public void ParseXml()
        {
            int i;
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("C:/Program Files/FlightGear 2020.3.6/data/Protocol/playback_small.xml");
            XmlNodeList name = xdoc.GetElementsByTagName("name");
            int size = name.Count / 2; // read only the first part(the input) in the XML file
            for (i = 0; i < size; i++)
            {
                string a = name[i].InnerText;
                myfeatures.Add(a);
            }
        }

        // The function gets feature and return the col of the feature in the train file
        public List<float> get_col_train(string feature)
        {
            int index = myfeatures.FindIndex(a => a.Contains(feature)); // the index number of the feature 
            List<float> list = data_train.ElementAt(index);
            return list;
        }

        // The function gets feature and return the col of the feature in the test file
        public List<float> get_col_test(string feature)
        {
            int index = myfeatures.FindIndex(a => a.Contains(feature)); // the index number of the feature 
            List<float> list = data_test.ElementAt(index);
            return list;
        }
     
        // The function gets feature and return the value of this feature in the current row which is reading now 
        public float findElement(string feature)
        {
            int index = myfeatures.FindIndex(a => a.Contains(feature)); // the index number of the feature 
            List<float> list = data_train.ElementAt(index);
            return list.ElementAt(IndexRow);
        }

       

        // The function gets ip and port and creates onnection with the FG
        public void Connect(string ip, int port)
        {
            client.connect(ip, port);
        }

        // The function diconnect
        public void disConnect()
        {
            client.disconnect();
        }

         // The main function which responsible about the running of the FG on another thread.
        public void Start()
        {
            isAfterLoad = true;
            pearson(); // make all the correlative featurses couples
            new Thread(delegate ()
            {
                while (!stop)
                {
                    if ((!pausePressed) && (IndexRow < numRows))
                    {
                        //advance to the next row only if the flight is playing and isnt in the end
                        IndexRow++;
                    }
                    string line = mylist_test.ElementAt(IndexRow);
                    client.write(line); //send to the FG
                    updateData(); // update all the changes
                    Thread.Sleep(speedsend); 
                }

            }).Start();
        }

     
        // The function updates all the important data for the views
        public void updateData()
        {
            //update the data of the Deshboard
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

            // update  the data of the Graphs
            MainGraphList = setGraphList(MainGraphName);
            SecondGraphList = setGraphList(SecondGraphName);
            setLineReg();

            // update the time
            setTime();
           
            // update the dll
            if (dynamic_load != null)
            {
                dynamic_load.time(indexRow);
            }

        }



        /// deshboard /////


        //property of the Airspeed
        private float airspeed;
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

        //propert of the Roll
        private float roll;
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

        //property of the Direction
        private float direction;
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

        //property of the Pitch
        private float pitch;
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

        //property of the Yaw
        private float yaw;
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

        //property of the Altimeter
        private float altimeter;
        public float Altimeter
        {
            get { return altimeter; }
            set
            {
                altimeter = value;
                NotifyPropertyChanged("Altimeter");
            }
        }



        ////// slider ///////
        ///
        /// 
        /// The function changes the speed of the screening to the new speed she gets.
        public void changeSpeed(double speed)
        {
            speedsend = (int)(50 / speed);
        }

        // The function turns on/off the pause bollean.
        public void changeMood()
        {
            pausePressed = !pausePressed;
        }


        //property of the number of rows
        private int numRows = 1;
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

        //property of the speed of the screening
        private int speedsend = 50;
        public int Speedsend
        {
            set
            { speedsend = value; }
            get
            { return speedsend; }
        }

        // property of the number of the row which now on the screen
        private int indexRow = 0;
        public int IndexRow
        {
            get { return indexRow; }
            set
            {
                indexRow = value;
                NotifyPropertyChanged("IndexRow");
            }
        }

        //property of the Time on the slider
        private string time = "00:00:00";
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

        // The function sets the current time according to the index row which is screening in FG
        private void setTime()
        {
            int miliSeconds = IndexRow % 10;
            int secends = (IndexRow / 10) % 60;
            int minutes = IndexRow / 600;
            Time = (minutes.ToString("D2")) + ":" + (secends.ToString("D2")) + ":" + ((miliSeconds * 10).ToString("D2"));

        }





        ////// Joystick //////
        


        //property of the Aileron 
        private float aileron = 125;
        public float Aileron
        {
            get { return aileron; }
            set
            {
                aileron = 125 + 100 * value;
                NotifyPropertyChanged("Aileron");
            }
        }

        //property of the Elevator
        private float elevator = 125;
        public float Elevator
        {
            get { return elevator; }
            set
            {
                elevator = 125 + 100 * value;
                NotifyPropertyChanged("Elevator");
            }

        }

        //property of the Rudder
        private float rudder;
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

        //property of the throttle
        private float throttle;
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



        /// ///////    Graph   //////  //

        // property represent if we load
        private bool isAfterLoad = false;
        public bool IsAfterLoad
        {
            get
            {
                return isAfterLoad;
            }
        }

        // property of the list features
        List<string> myfeatures = new List<string>();
        public List<string> ColumnList
        {
            get
            {
                return myfeatures;
            }
        }


        // map of the corelations of all features
        private List<KeyValuePair<string, string>> corelationMap;

        // property of the left Graph's list of points
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

        //property of the name of the left Graph
        private string mainGraphName = "aileron";
        public string MainGraphName
        {
            set
            {
                mainGraphName = value;
                // update the name of the second Graph to be the most corrleative to the first
                secondGraphName = get_element(mainGraphName);
                NotifyPropertyChanged("MainGraphName");
            }
            get
            {
                return mainGraphName;
            }
        }

        // property of right Graph's list of points
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

        //property of the name of the right Graph
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

        // The function gets a feature and return a list of DataPoint with the data col of the feature.
        public List<DataPoint> setGraphList(string feature)
        {
            List<float> featureColumn = get_col_test(feature);
            List<DataPoint> newDataPoints = new List<DataPoint>();
            int i = 0;
            foreach (float num in featureColumn)
            {
                newDataPoints.Add(new DataPoint(i++, num));
                // enter the points according to the index row of the screening
                if (i > IndexRow)
                {
                    break;
                }
            }
            return newDataPoints;
        }

        //The function sets the current regression line according to the 2 features
        public void setLineReg()
        {
            List<float> x_train = get_col_train(mainGraphName);
            List<float> x_test = get_col_test(mainGraphName);
            float[] x_array = x_train.ToArray();
            
            List<float> Y_train = get_col_train(secondGraphName);
            List<float> y_test = get_col_test(secondGraphName);
            float[] y_array = Y_train.ToArray();

            Point[] ps = new Point[x_array.Length];
            List<DataPoint> temp_points = new List<DataPoint>();
            int size = x_train.Count;
            int i;
            for (i = 0; i < size ; ++i)
            {
                //defines every point from 2 features lists 
                ps[i] = new Point(x_array[i], y_array[i]);
                
                //defines the last 30 seconds Data points
                if (i > (indexRow - 300) && i <= indexRow)
                {
                    temp_points.Add(new DataPoint(x_test.ElementAt(i), y_test.ElementAt(i)));
                }
            }
            
            Line l = anomaly.linear_reg(ps, ps.Length);
            List<DataPoint> cor_point = new List<DataPoint>();
            size = x_test.Count;
            for (i = 0; i < size; ++i)
            {
                cor_point.Add(new DataPoint(l.X_line(y_test.ElementAt(i)), y_test.ElementAt(i)));
                cor_point.Add(new DataPoint(x_test.ElementAt(i), l.f(x_test.ElementAt(i))));
            }
            LineReg = cor_point;
            Points_reg = temp_points;
        }

        // property of points regression's list
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


        // property of points line regression's list
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
         
        // The function get feature and return the feature which most corrleative to
        public string get_element(string col)
        {
            foreach (var i in corelationMap)
            {
                if (i.Key.Equals(col))
                {
                    return i.Value;
                }

            }
            return null;
        }

        //The function defines a map of couples of features which most correlaive each other
        public void pearson()
        {

            corelationMap = new List<KeyValuePair<string, string>>();
            int i,j,index,size = myfeatures.Count;
            float max,num;
            for (i = 0; i < size; i++)
            {

                List<float> feathre_one = data_train.ElementAt(i);
                max = 0;
                index = 0;
                // find the most correlative feature to the first feaure
                for (j = 0; j < size; j++)
                {
                    if (i != j)
                    {
                        num = 0;
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
                corelationMap.Add(new KeyValuePair<string, string>(myfeatures.ElementAt(i), myfeatures.ElementAt(index)));

            }
        }



    }

}
