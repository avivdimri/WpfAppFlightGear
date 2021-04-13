using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfMinCircleLib
{
  
  public  class dll
    {
        private UserControl1 s;
        private VM_grafh vm;
        private ModelGrafh model;


        public UserControl1 create()
        {
            model = new ModelGrafh();
            vm = new VM_grafh(model);
            s = new UserControl1(vm);
            return s;

        }
        public void update(List<float> p1_train, List<float> p2_train, List<float> p1_test, List<float> p2_test)
        {
            //Console.WriteLine("in update");
            
                model.update_lists(p1_train, p2_train, p1_test, p2_test);
                //circle = algo_circle.findMinCircle(my_point_train, my_point_train.Count());
           
            
          
        }
        public void time(int index)
        {
           //model.update_time(index);
        }
    }
}
