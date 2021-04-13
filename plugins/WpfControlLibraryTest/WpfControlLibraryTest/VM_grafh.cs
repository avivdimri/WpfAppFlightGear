using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfControlLibraryTest
{



   public class VM_grafh: INotifyPropertyChanged
    {
        private ModelRegGraph model;

        public event PropertyChangedEventHandler PropertyChanged;

        // the dev points
        public List<DataPoint> VM_Dev_point
        {
           
            get
            {
              
                return model.Dev_point;

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


        //all the points
        public List<DataPoint> VM_Points
        {
            
            get
            {
                return model.Points;
               
            }
        }
        //all the current points
        public List<DataPoint> VM_Corrent_points
        {
            get
            {
                return model.Corrent_points;
            }
   
        }
        //the line reg
        public List<DataPoint> VM_Reg_line
        {

            get
            {
              
                return model.Reg_line;
               
            }
        }

        private void NotifyPropertyChanged(string propName)
        {

            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public VM_grafh(ModelRegGraph m) {
            this.model = m;
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
                Console.WriteLine("in  NotifyPropertyChanged");
            };
        }
    }
}
