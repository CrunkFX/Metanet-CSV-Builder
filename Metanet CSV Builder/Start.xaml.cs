using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace Metanet_CSV_Builder
{
    /// <summary>
    /// Interaktionslogik für Start.xaml
    /// </summary>
    public partial class Start : MetroWindow
    {
        public Start()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Settings win2 = new Settings();
            win2.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Backbone win2 = new Backbone();
            win2.Show();
            Visibility = Visibility.Collapsed;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SubnetA win2 = new SubnetA();
            win2.Show();
            Visibility = Visibility.Collapsed;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SubnetB win2 = new SubnetB();
            win2.Show();
            Visibility = Visibility.Collapsed;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            SubnetC win2 = new SubnetC();
            win2.Show();
            Visibility = Visibility.Collapsed;
        }
        
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            File.Copy("bin/opt/SubnetA/current.csv",path+"/.SUBNET-A Lange Bezeichner.csv",true);
            File.Copy("bin/opt/SubnetB/current.csv", path + "/.SUBNET-B Lange Bezeichner.csv", true);
            File.Copy("bin/opt/SubnetC/current.csv", path + "/.SUBNET-C Lange Bezeichner.csv", true);
            File.Copy("bin/opt/Backbone/current.csv", path + "/.Backbone Lange Bezeichner.csv", true);
            //Visibility = Visibility.Collapsed;
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            File.Delete(path + "/.SUBNET-A Lange Bezeichner.csv");
            File.Delete(path + "/.SUBNET-B Lange Bezeichner.csv");
            File.Delete(path + "/.SUBNET-C Lange Bezeichner.csv");
            File.Delete(path + "/.Backbone Lange Bezeichner.csv");
            base.OnClosing(e);
        }
    }
}
