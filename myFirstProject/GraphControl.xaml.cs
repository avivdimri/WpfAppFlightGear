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
    /// Interaction logic for GraphControl.xaml
    /// </summary>
    public partial class GraphControl : UserControl
    {
        private VmGraph vm;
        public GraphControl()
        {
            InitializeComponent();
        }

        public VmGraph VM_Graph
        {
            get
            {
                return vm;
            }
            set
            {
                vm = value;
                this.DataContext = value;

            }
        }

        private void Button_aileron(object sender, RoutedEventArgs e)
        {
            vm.VM_setMainGraphList("aileron");
        }

        private void Button_elevator(object sender, RoutedEventArgs e)
        {
            vm.VM_setMainGraphList("elevator");
        }



        private void Button_setMainGraphList(object sender, RoutedEventArgs e)
        {
            vm.VM_setMainGraphList(((Button)sender).Content.ToString());
        }

        private void showGraphs(object sender, RoutedEventArgs e)
        {
            if (vm.VM_IsAfterLoad)
            {
                myStack.Children.Clear();
                List<string> myList = new List<string> { "flaps", "slats", "speedbrake" };

                //Dictionary<string, int> columnMap = vm.getColumnMap();
                List<string> list_col = vm.getColumnList();


                foreach (string i in list_col)
                {



                    Button newB = new Button();
                    //newB.Name = pair.Key;
                    newB.Content = i;
                    myStack.Children.Add(newB);
                    newB.Click += new RoutedEventHandler(Button_setMainGraphList);
                }
            }

            //Button b1 = new Button();
            //b1.Name = "myB";
            //b1.Content = "myName";
            //myStack.Children.Add(b1);
        }
    }
}
