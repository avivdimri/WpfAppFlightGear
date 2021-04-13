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

namespace WpfMinCircleLib
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private VM_grafh vm;

        public VM_grafh Vm
        {
            set
            {
                vm = value;
                DataContext = value;
            }
        }


        public UserControl1(VM_grafh vm)
        {
            InitializeComponent();
            this.vm = vm;
            DataContext = vm;

        }
    }
}
