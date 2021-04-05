using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for SliderControl.xaml
    /// </summary>
     partial class SliderControl : UserControl
    {
        VMslider vm;
        public SliderControl()
        {
            InitializeComponent();
        }
        public VMslider VM_slider
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

        /*private void Speed_Click(object sender, RoutedEventArgs e)
        {
            string num;// .header ToString();
            Mspeed.Text = num;// ; // ToString;// "0.75";
            //double d = Convert.ToDouble(num);
            //vm.VM_speedsend = d;// Convert.ToDouble(num, CultureInfo.InvariantCulture);
        }*/
        private void Speed_Click_075(object sender, RoutedEventArgs e)
        {
            Mspeed.Text = "0.75";
            vm.VM_speedsend = 0.75;
        }

        private void Speed_Click_05(object sender, RoutedEventArgs e)
        {
            Mspeed.Text = "0.5";
            vm.VM_speedsend = 0.5;
        }

        private void Speed_Click_1(object sender, RoutedEventArgs e)
        {
            Mspeed.Text = "1.0";
            vm.VM_speedsend = 1;
        }

        private void Speed_Click_125(object sender, RoutedEventArgs e)
        {
            Mspeed.Text = "1.25";
            vm.VM_speedsend = 1.25;
        }

        private void Speed_Click_15(object sender, RoutedEventArgs e)
        {
            Mspeed.Text = "1.5";
            vm.VM_speedsend = 1.5;
        }

        private void Speed_Click_2(object sender, RoutedEventArgs e)
        {
            Mspeed.Text = "2";
            vm.VM_speedsend = 2;
        }

        private void Speed_Click_175(object sender, RoutedEventArgs e)
        {
            Mspeed.Text = "1.75";
            vm.VM_speedsend = 1.75;
        }

        private void Speed_Click_225(object sender, RoutedEventArgs e)
        {
            Mspeed.Text = "2.25";
            vm.VM_speedsend = 2.25;
        }

        private void Speed_Click_25(object sender, RoutedEventArgs e)
        {
            Mspeed.Text = "2.5";
            vm.VM_speedsend = 2.5;
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            if ((vm.VM_IndexRow - 100) < 0)
                vm.VM_IndexRow = 0;
            else
                vm.VM_IndexRow -= 100;

        }


        private void goHead(object sender, RoutedEventArgs e)
        {
            if ((vm.VM_IndexRow + 100) > vm.VM_NumRows)
                vm.VM_IndexRow = vm.VM_NumRows;
            else
                vm.VM_IndexRow += 100;
        }

        private void pause(object sender, RoutedEventArgs e)
        {
            vm.Stop();
            pauseButton.Background = Brushes.White;
            playButton.Background = Brushes.DarkRed;
            //= "#FF3FD422"
        }

        private void play(object sender, RoutedEventArgs e)
        {
            vm.play();
            playButton.Background = Brushes.White;
            pauseButton.Background = Brushes.DarkRed;

        }
    }
}

    

