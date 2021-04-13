using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFirstProject
{

    public class VmGraph : INotifyPropertyChanged
    {


        private MyModel model;

        //Checks if Path of the dll was received
        private bool isopen = false;
        public bool Isopen
        {
            set
            {
                isopen = value;
            }
            get
            {
                return isopen;
            }
        }


        //the title of the csv
        public List<string> getColumnList()
        {
            return model.ColumnList;
        }


        //Holds a message if there is no correlation
        public string VM_NonCorrelation
        {
            get
            {
                return model.NonCorrelation;
            }
        }

        //Did upload files
        public bool VM_IsAfterLoad
        {
            get
            {
                return model.IsAfterLoad;
            }
        }

        //Notifications
        public VmGraph(MyModel model)
        {
            this.model = model;
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);

            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propName)
        {

            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        //list of the point of the main grph
        public List<DataPoint> VM_MainGraphList
        {

            get
            {
                return model.MainGraphList;
            }
        }

        //the name of the main graph
        public string VM_MainGraphName
        {

            get
            {
                return model.MainGraphName;
            }
            set
            {
                model.MainGraphName = value;
            }
        }
        //list of the point of the secend grph
        public List<DataPoint> VM_SecondGraphList
        {

            get
            {
                return model.SecondGraphList;
            }
        }

        //the name of the secend graph
        public string VM_SecondGraphName
        {

            get
            {
                return model.SecondGraphName;
            }
        }

        //the dll referens
        public dynamic VM_Dynamic_load
        {

            get
            {
                return model.Dynamic_load;
            }
            set
            {
                model.Dynamic_load = value;
            }
        }



        //all the point of the test
        public List<DataPoint> VM_Points
        {
            set
            {
                model.Points_reg = value;
            }
            get
            {
                return model.Points_reg;
            }
        }


        //the line regression
        public List<DataPoint> VM_LineReg
        {
            set
            {
                model.LineReg = value;
            }
            get
            {
                return model.LineReg;
            }
        }


        //The method communicates with the dll and sends
        //data to it each time a graph changes
        public void show(dynamic dll)
        {
            //Did you load the dll
            if (isopen)
            {
                //Is there a correlation
                if (model.SecondGraphName != null)
                {
                    List<float> p1_train = model.get_col_train(model.MainGraphName);
                    List<float> p2_train = model.get_col_train(model.SecondGraphName);
                    List<float> p1_test = model.get_col_test(model.MainGraphName);
                    List<float> p2_test = model.get_col_test(model.SecondGraphName);
                    dll.update(p1_train, p2_train, p1_test, p2_test);
                }
                else
                {
                    List<float> empty = new List<float>();
                    dll.update(empty, empty, empty, empty);
                }
            }
        }
    }
}
