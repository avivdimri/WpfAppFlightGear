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


        public Dictionary<string, int> getColumnMap()
        {
            return model.ColumnMap;
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
        }

        public List<DataPoint> VM_SecondGraphList
        {

            get
            {
                return model.SecondGraphList;
            }
        }

        public string VM_SecondGraphName
        {

            get
            {
                return model.SecondGraphName;
            }
        }
    }


}
