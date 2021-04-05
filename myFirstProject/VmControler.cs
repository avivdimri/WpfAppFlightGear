using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFirstProject
{
    public class VmControler : INotifyPropertyChanged
    {
        private MyModel model;


        public VmControler(MyModel m)
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


        public float VM_Throttle
        {
            get
            {
                int max = 1;
                int min = 0;
                return (model.Throttle - min) / (max - min) * 100;

            }

        }
        public float VM_Rudder
        {
            get
            {
                int max = 1;
                int min = -1;
                return (model.Rudder - min) / (max - min) * 100;
            }

        }
        public float VM_Aileron
        {
            get
            {
                float max = (float) 0.214314;
                float min = (float)-0.343772;
                return (model.Aileron - min) / (max - min) * 100;

            }

        }
        public float VM_Elevator
        {
            get
            {
                float max = (float) 0.16409;
                float min = (float) -0.904754;
                return (model.Elevator - min) / (max - min) * 100;

            }

        }




    }
}
