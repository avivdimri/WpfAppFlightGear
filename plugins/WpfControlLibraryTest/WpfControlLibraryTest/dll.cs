using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfControlLibraryTest
{

   public class dll
    {
        private UserControl1 s;
        private VM_grafh vm;
        private ModelRegGraph model;

        //create a usercontrol and return it.
        public UserControl1 create()
        {
            model = new ModelRegGraph(); 
            vm = new VM_grafh(model);
            s = new UserControl1(vm);
            return s;

        }
        public void update(List<float>p1_train, List<float> p2_train, List<float> p1_test, List<float> p2_test)
        {
            model.update_lists(p1_train, p2_train, p1_test, p2_test);
        }
        public void time(int index)
        {
            model.update_time(index);
        }
    }
}
