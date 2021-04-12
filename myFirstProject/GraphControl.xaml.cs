using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
           // vm.VM_setMainGraphList("aileron");
        }

        private void Button_elevator(object sender, RoutedEventArgs e)
        {
           // vm.VM_setMainGraphList("elevator");
        }



        private void Button_setMainGraphList(object sender, RoutedEventArgs e)
        {
            vm.VM_MainGraphName = ((Button)sender).Content.ToString();
            if (vm.Isopen)
            {
                vm.show(dynamic_load);

            }
            
        }

        private void showGraphs(object sender, RoutedEventArgs e)
        {
            //Path= "C:/Users/yosef/Source/Repos/WpfControlLibraryTest/WpfControlLibraryTest/bin/x64/Debug/WpfControlLibraryTest.dll";
            if (vm.VM_IsAfterLoad)
            {

                myStack.Children.Clear();

                List<string> list_col = vm.getColumnList();


                foreach (string i in list_col)
                {
                    Button newB = new Button();
                    BrushConverter converter = new BrushConverter();
                    //Brush brush = converter.ConvertFromString("#66FFE7CB") as Brush;
                    newB.Background = converter.ConvertFromString("#66FFE7CB") as Brush;
                    newB.Content = i;
                    newB.FontWeight= FontWeights.Bold;
                    myStack.Children.Add(newB);
                    newB.Click += new RoutedEventHandler(Button_setMainGraphList);
                }
            }
           
        }

        private dynamic dynamic_load;

        public dynamic Dynamic_load
        {
            get
            {
                return dynamic_load;

            }
            set
            {
                dynamic_load = value;
            }
        }
        private string path;
        public string Path
        {
            set
            {
                path = value;
                init();
            }
            get
            {
                return path;

            }
        }
        public void init()
        {
            try
            {
               
                Assembly dll = Assembly.LoadFile(path);
                Type[] t = dll.GetExportedTypes();
                foreach (Type i in t)
                {
                    if (i.Name == "dll")
                    {
                        dynamic_load = Activator.CreateInstance(i);
                    }
                }
                mydock.Children.Add(dynamic_load.create());
                vm.Isopen = true;
              //  dynamic_load.
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            vm.VM_Dynamic_load = dynamic_load;

        }
    }
}
