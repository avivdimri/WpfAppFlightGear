using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace myFirstProject
{
    /// <summary>
    /// Interaction logic for JoystickControler.xaml
    /// </summary>
    public partial class JoystickControler : UserControl
    {
        VmControler vm;
        public JoystickControler()
        {
            InitializeComponent();
        }
        public VmControler Vm_Controler

        {
            get
            {
                return vm;
            }
            set
            {
                vm = value;
                this.DataContext = value;
                //slider.Maximum = vm.VM_NumRows;
            }
        }
    }
}
