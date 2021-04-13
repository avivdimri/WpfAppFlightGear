﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFirstProject
{
    class ViewModel : INotifyPropertyChanged
    {
        private MyModel model;
        private bool IsPause = false;
       
        
        public ViewModel(MyModel m)
        {
            this.model = m;
            model.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
               NotifyPropertyChanged("VM_" + e.PropertyName);
                
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public void LoadfileXml(string path)
        {
            model.ParseXml(path);
           // model.readcsvfile(path);
           // model.parser(path);
           // model.createMap();
        }
        public void Loadtrainfile(string path)
        {
            model.readCsvTrainFile(path);
           // model.parser();
           // model.Data_train = model.createMap();
        }

        public void Loadtestfile(string path)
        {
            model.readCsvTestFile(path);
           // model.parser();
           // model.Data_test= model.createMap();
        }
        public void Start()
        {
            model.Connect("localhost", 5400);
            model.Start();

        } 

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

        public void exit()
        {
            model.Stop = true;
        }

    }
}
