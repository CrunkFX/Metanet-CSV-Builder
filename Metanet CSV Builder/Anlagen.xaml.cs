using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Metanet_CSV_Builder
{



    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class Anlagen : MetroWindow
    {

        private readonly string CSVPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/Hussmann/MetanetCSV/DB/" + App.Current.Properties["DevMGR"]+"/CSV/";
        private readonly OpenFileDialog ofd = new OpenFileDialog();
        private readonly string temppath = System.IO.Path.GetTempFileName();
        


        public Anlagen()
        {
            InitializeComponent();
            Uri myUri = new Uri(CSVPath);
            wb1.Source = myUri;
            
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (App.Current.Properties["DevMGR"].ToString()== "Backbone")
            {
                ((Backbone)this.Owner).refreshFilenames();
            }
            if (App.Current.Properties["DevMGR"].ToString() == "SubnetA")
            {
                ((SubnetA)this.Owner).refreshFilenames();
            }
            if (App.Current.Properties["DevMGR"].ToString() == "SubnetB")
            {
                ((SubnetB)this.Owner).refreshFilenames();
            }
            if (App.Current.Properties["DevMGR"].ToString() == "SubnetC")
            {
                ((SubnetC)this.Owner).refreshFilenames();
            }

        }




        public void CSVImport()
        {
            ofd.Filter = "CSV Datei|*.csv";
            ofd.ShowDialog();
            try
            {
                File.Copy(ofd.FileName, temppath, true);
                for (int i = 0; i < 12; i++)
                {
                    read(1);

                }
                File.Copy(temppath, CSVPath + tb1.Text + ".csv", true);
               
            }
            catch
            {
                MessageBox.Show("Du musst schon ne Datei auswählen ;)");
            }
            

        }


        //CSV Laden

        public void read(int lineNumber)
        {
            int counter = 0;
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(temppath);
            string newstr = null;
            while ((line = file.ReadLine()) != null)
            {
                // Console.WriteLine(line);
                if ((counter + 1).Equals(lineNumber))
                {

                }
                else
                {
                    newstr += line + Environment.NewLine;

                }
                counter++;
            }

            file.Close();

            if (System.IO.File.Exists(temppath))
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(temppath);
                sw.WriteLine(newstr);
                sw.Close();
            }

        }

       
        private void LoadCSV(object sender, RoutedEventArgs e)
        {
            CSVImport();
        }

        private void tb1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb1.Text.Length > 1)
            {
                AddtoGI.IsEnabled = true;
            }
            else
            {
                AddtoGI.IsEnabled = false;
            }

        }
    }
}
