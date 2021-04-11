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
        public List<string> getColumnList()
        {
            return model.ColumnList;
        }

        public bool VM_IsAfterLoad
        {
            get
            {
                return model.IsAfterLoad;
            }
        }


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


        public void VM_setMainGraphList(string column)
        {
            model.setMainGraphList(column);
        }



        public List<DataPoint> VM_MainGraphList
        {

            get
            {
                return model.MainGraphList;
            }
        }

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

        public List<DataPoint> VM_SecondGraphList
        {

            get
            {
                return model.SecondGraphList;
            }
        }


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


        public string VM_SecondGraphName
        {

            get
            {
                return model.SecondGraphName;
            }
        }



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

        public void show(dynamic dll)
        {
            if (isopen) { 
            List<float> p1_train = model.get_col_train(model.MainGraphName);
            List<float> p2_train = model.get_col_train(model.SecondGraphName);
            List<float> p1_test = model.get_col_test(model.MainGraphName);
            List<float> p2_test = model.get_col_test(model.SecondGraphName);
                //List<Point> my_list = create_point_list(p1,p2
            
  
            dll.update(p1_train,p2_train,p1_test,p2_test);
            }
        }
        public List<Point> create_point_list(List<float>p1, List<float> p2)
        {
            List<Point> listPoint = new List<Point>();
            for (int i = 0; i < p1.Count; i++)
            {
                Point p = new Point(p1.ElementAt(i), p2.ElementAt(i));
                listPoint.Add(p);
            }
            return listPoint;

        }
    }


}
