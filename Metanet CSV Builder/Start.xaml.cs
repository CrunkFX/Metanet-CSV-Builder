using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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
        public readonly string dbfolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/Hussmann";
        public string DevMGR = "";

        public Start()
        {
            InitializeComponent();

            if (Directory.Exists(dbfolder))
            {
                dblc.Content = Directory.GetLastWriteTime(dbfolder).ToString("dd.MM.yyyy HH:mm:ss");
                bntexport.IsEnabled = true;
                btnbb.IsEnabled = true;
                btnsa.IsEnabled = true;
                btnsb.IsEnabled = true;
                btnsc.IsEnabled = true;
                btns2s.IsEnabled = true;
                lbl1.Content = "Netzwerk zum Editieren auswählen!!";
            }
            else
            { dblc.Content = "Keine Datenbank!!";
                lbl1.Content = "Datenbank importieren!!";
             }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Settings win2 = new Settings();
            win2.Show();
            Visibility = Visibility.Collapsed;
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
            Anlagen win2 = new Anlagen();
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
            try
            {
                File.Delete(path + "/.SUBNET-A Lange Bezeichner.csv");
                File.Delete(path + "/.SUBNET-B Lange Bezeichner.csv");
                File.Delete(path + "/.SUBNET-C Lange Bezeichner.csv");
                File.Delete(path + "/.Backbone Lange Bezeichner.csv");
            }
            catch
            {
                MessageBox.Show("Err. 21  ---  Bitte die Dateien auf dem Desktop Mauell löschen!");
            }
            System.Windows.Application.Current.Shutdown();
            base.OnClosing(e);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter="Metanet Datenbank|*.ndb";
            try { 
            ofd.ShowDialog();
                if (Directory.Exists(dbfolder) && File.Exists(ofd.FileName))
                {
                    Directory.Delete(dbfolder, true);
                    bntexport.IsEnabled = true;
                    btnbb.IsEnabled = true;
                    btnsa.IsEnabled = true;
                    btnsb.IsEnabled = true;
                    btnsc.IsEnabled = true;
                    btns2s.IsEnabled = true;
                    lbl1.Content = "Netzwerk zum Editieren auswählen!!";

                }
                
                ZipFile.ExtractToDirectory(ofd.FileName, dbfolder + "/");




            }
            catch
            {
                MessageBox.Show("Du musst schon ne Datei auswählen!");
            }

            dblc.Content = Directory.GetLastWriteTime(dbfolder).ToString("dd.MM.yyyy HH:mm:ss"); ;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
           
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Metanet Datenbank|*.ndb";
            sfd.ShowDialog();
            try {
                File.Delete(sfd.FileName);
                ZipFile.CreateFromDirectory(dbfolder, sfd.FileName);


            }
            catch
            {
                MessageBox.Show("Du musst schon ne Datei auswählen!");
            }

           

        }
    }
}
