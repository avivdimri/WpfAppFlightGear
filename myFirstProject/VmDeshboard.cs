using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFirstProject
{
    public class VmDeshboard : INotifyPropertyChanged
    {

        private MyModel model;
      

        public VmDeshboard(MyModel m)
        {
            this.model = m;
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);

            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public double VM_Altimeter
        {
            get
            {
                int max = 750;
                int min = -50;
                return (model.Altimeter - min) / (max - min) * 100;
            }

        }
        public float VM_Airspeed
        {
            get
            {
                
                return model.Airspeed;
            }

        }
        public float VM_Roll
        {
            get
            {
                return (model.Roll);
            }

        }
        public float VM_Direction
        {
            get
            {
                return (model.Direction);
            }

        }
        public float VM_Pitch
        {
            get
            {
                return (model.Pitch);
            }

        }
        public float VM_Yaw
        {
            get
            {
                return (model.Yaw);
            }

        }
    }
}
