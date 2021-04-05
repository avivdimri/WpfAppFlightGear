using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFirstProject
{
    interface InterfaceModel : INotifyPropertyChanged
    {

        void Connect(string ip, int port);
        void disConnect();
        void Start();

        int IndexRow { set; get; }


    }
}
