using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMinCircleLib
{
    public interface IntrfaceDll
    {
        UserControl1 create();
        void update(List<float> p1_train, List<float> p2_train, List<float> p1_test, List<float> p2_test);
        void time(int index);


    }
}
