using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFirstProject
{
    public class VMslider : INotifyPropertyChanged
    {
        private MyModel model;
        private bool IsPause = false;
        public VMslider(MyModel m)
        {
            model = m;
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);

            };
        }

        public string VM_Time { get { return model.Time; } }
      

        private void NotifyPropertyChanged(string propName)
        {

            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Stop()
        {
            if (!IsPause)
                model.changeMood();
                IsPause = true;
        }
        public void play()
        {
            if (IsPause)
                model.changeMood();
                IsPause = false;
        }
        public double VM_IndexRow
        {
            get
            {
                return model.IndexRow;
            }
            set
            {
                model.IndexRow = (int)value;
            }
        }

        public int VM_NumRows
        {
            get
            {
                return model.NumRows;
            }

        }

        public double VM_speedsend
        {
            get { return model.Speedsend; }
            set
            {
                model.changeSpeed(value);
            }
        }
    }
}
