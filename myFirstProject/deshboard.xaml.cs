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
    /// Interaction logic for deshboard.xaml
    /// </summary>
    partial class deshboard : UserControl
    {
    
        VmDeshboard vm;
        public deshboard()
        {
            InitializeComponent();
        }
        public VmDeshboard VM_deshboard

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

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
