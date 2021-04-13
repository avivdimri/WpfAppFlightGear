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

		private bool path_csv_traing = false;
		private bool path_csv_test = false;
		private bool path_xml = false;



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
			///MessageBox.Show("Welcome to anomaly flight detect!\n" + "Please upload: " + "1.XML file\n" + "2.Train csv\n" + "3.Test csv file\n", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);


		}


		private void Open_Click(object sender, RoutedEventArgs e)
		{
			if (path_xml)
			{

				OpenFileDialog openFile = new OpenFileDialog();
				openFile.Filter = "CSV files (*.csv)|*.csv";
				if (openFile.ShowDialog() == true)
				{
					path = openFile.FileName;
					vm.Loadtrainfile(path);
					path_csv_traing = true;
				}
            }
            else
            {
				MessageBox.Show("please upload XML file!", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);

			}

		}

		private void Start_Click(object sender, RoutedEventArgs e)
		{
			if ((path_csv_traing) && (path_csv_test) && (path_xml))
			{
				vm.Start();
			}
			else
			{

				MessageBox.Show("Please upload:\n " + "1.XML file\n" + "2.Train csv\n" + "3.Test csv file\n", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
			}

		}

		private void upload_Click(object sender, RoutedEventArgs e)
		{
				OpenFileDialog openFile = new OpenFileDialog();
				openFile.Filter = "xml files (*.xml)|*.xml";
				if (openFile.ShowDialog() == true)
				{
					path = openFile.FileName;
					vm.LoadfileXml(path);
					path_xml = true;
				}
            
           
		}


		private void MenuItem_Click(object sender, RoutedEventArgs e)
		{
			if (path_xml)
			{
				OpenFileDialog openFile = new OpenFileDialog();
				openFile.Filter = "CSV files (*.csv)|*.csv";
				if (openFile.ShowDialog() == true)
				{
					path = openFile.FileName;
					vm.Loadtestfile(path);
					path_csv_test = true;
				}
			}
			else
			{
				MessageBox.Show("please upload XML file!", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
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
		
		private void exit_Click(object sender, RoutedEventArgs e)
		{
			vm.exit();
			this.Close();
	
		}
	}
}
