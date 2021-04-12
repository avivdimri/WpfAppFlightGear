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
using Microsoft.Win32;
using System.IO;

namespace myFirstProject
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		string path;
		ViewModel vm;
		MyModel m;
		VMslider vmslider;
		VmDeshboard vmDeshboard;
		VmControler vmcont;

        VmGraph vm_graph;
		

        public MainWindow()
		{
			InitializeComponent();
			MyModel m = new MyModel(new myClient());
			vm = new ViewModel(m);
			vmslider = new VMslider(m);
			sliderCont.VM_slider = vmslider;
			vmDeshboard = new VmDeshboard(m);
			deshboardCont.VM_deshboard = vmDeshboard;
			vmcont = new VmControler(m);
			joystickCont.Vm_Controler = vmcont;
            vm_graph = new VmGraph(m);
            graphCont.VM_Graph = vm_graph;

            this.DataContext = vm;

		}


		private void Open_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFile = new OpenFileDialog();
			openFile.Filter = "CSV files (*.csv)|*.csv";
			if (openFile.ShowDialog() == true)
			{
				path = openFile.FileName;
				vm.Loadtrainfile(path);
			}
			
		}

		private void Start_Click(object sender, RoutedEventArgs e)
		{
			vm.Start();
		}

		private void Pause_Click(object sender, RoutedEventArgs e)
		{
			vm.Stop();

		}
		private void upload_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFile = new OpenFileDialog();
			openFile.Filter = "xml files (*.xml)|*.xml";
			if (openFile.ShowDialog() == true)
			{
				path = openFile.FileName;
				vm.LoadfileXml(path);
			}


		}
		private void deshCont_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void controlerCont_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void graphCont_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
			OpenFileDialog openFile = new OpenFileDialog();
			openFile.Filter = "CSV files (*.csv)|*.csv";
			if (openFile.ShowDialog() == true)
			{
				path = openFile.FileName;
				vm.Loadtestfile(path);
			}

		}

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
			OpenFileDialog openFile = new OpenFileDialog();
			openFile.Filter = "dll files (*.dll)|*.dll";
			if (openFile.ShowDialog() == true)
			{
				path = openFile.FileName;
				
			}
			graphCont.Path = path;
        }
    }
}
