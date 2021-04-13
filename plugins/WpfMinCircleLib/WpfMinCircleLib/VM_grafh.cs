using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMinCircleLib
{
   public class VM_grafh: INotifyPropertyChanged
    {
        private ModelGrafh model;

        public event PropertyChangedEventHandler PropertyChanged;

        //the circle 
        public List<DataPoint> VM_Center
        {
           
            get
            {
                return model.Center;
            }
        }
      
        //3
        public List<DataPoint> VM_Dev_list_points
        {
           
            get
            {
                return model.Dev_list_points;
            }
        }
        //4
        public List<DataPoint> VM_My_list_point
        {
           
            get
            {
              
                return model.My_list_point;
            }
        }
        //5
        public List<DataPoint> VM_Corrent_points
        {
            get
            {
                return model.Corrent_points;
            }
        }

        // the slider points in a time step. 
        public List<DataPoint> VM_SliderPoints
        {
            get
            {
                return model.SliderPoints;

            }
        }

        public List<DataPoint> VM_SliderBorder
        {
            get
            {
                return model.SliderBorder;

            }
        }

        private void NotifyPropertyChanged(string propName)
        {

            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public VM_grafh(ModelGrafh m)
        {
            this.model = m;
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
                
            };
        }
    }
}
