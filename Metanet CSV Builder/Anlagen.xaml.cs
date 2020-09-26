using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Metanet_CSV_Builder
{



    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class Anlagen : MetroWindow
    {

        private readonly string CSVPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/Hussmann/MetanetCSV/Temp/CSV/" + App.Current.Properties["DevMGR"]+"/";
        string SysDB = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/Hussmann/MetanetCSV/Temp/" + App.Current.Properties["DevMGR"] + "/DevMGR.mcb";
        private int GICount = 0;
        private readonly OpenFileDialog ofd = new OpenFileDialog();
        private readonly string temppath = System.IO.Path.GetTempFileName();
        


        public Anlagen()
        {
            InitializeComponent();


            
            GICount = 1;
            InitCounter();
            InitLastChange();
            InitBezeichner();
            InitCSV();

            
            for (int i = 1; i < Convert.ToInt32(File.ReadLines(SysDB).Skip(0).Take(1).First()); i++)
            {
                GICount = GICount + 1;
                InitCounter();
                InitLastChange();
                InitBezeichner();
                InitCSV();


            }
            for (int i = 0; i < GICount; i++)
            {
                string line = File.ReadLines(SysDB).Skip(i + 4).Take(1).First();
                List<string> List1 = line.Split(',').ToList();
                (FindName($"gi{i + 1}bz") as TextBox).Text = List1[0];
                
            }



        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {

            File.Delete(SysDB);
            File.AppendAllText(SysDB, GICount.ToString());
            File.AppendAllText(SysDB, "\n");
            File.AppendAllText(SysDB, "\n");
            File.AppendAllText(SysDB, "\n");
            File.AppendAllText(SysDB, "\n");
            for (int i = 0; i < GICount; i++)
            {
                File.AppendAllText(SysDB, (FindName($"gi{i + 1}bz") as TextBox).Text + "\n");
            }
            
        }

        private void AddToGI_click(object sender, RoutedEventArgs e)
        {

            if ((FindName($"gi{GICount}bz") as TextBox).Text.Length >= 1 )
            {
                GICount = GICount + 1;
                InitCounter();
                InitLastChange();
                InitBezeichner();
                InitCSV();

            }
            else
            {
                UTS();
            };
        }
        private async Task UTS()
        {
            (FindName($"gi{GICount}bz") as TextBox).Background = Brushes.OrangeRed;
            await Task.Delay(100);
            (FindName($"gi{GICount}bz") as TextBox).Background = Brushes.White;
            await Task.Delay(50);
            (FindName($"gi{GICount}bz") as TextBox).Background = Brushes.OrangeRed;
            await Task.Delay(100);
            (FindName($"gi{GICount}bz") as TextBox).Background = Brushes.White;
            await Task.Delay(50);
            (FindName($"gi{GICount}bz") as TextBox).Background = Brushes.OrangeRed;
            await Task.Delay(100);
            (FindName($"gi{GICount}bz") as TextBox).Background = Brushes.White;
        }


        // INIT START--------------

        private void InitCounter()
        {
            SPCount.Children.Add(new Label() { Content = GICount.ToString(), Margin = new Thickness(0, 0, 0, 10), FontWeight = FontWeights.Bold, Height = 27 });
        }



        private void InitCSV()
        {

            Button loadCSV = new Button
            {
                Content = "CSV Laden",
                Name = "Zentrale" + GICount.ToString(),
                Margin = new Thickness(0, 0, 0, 10),
                FontWeight = FontWeights.Bold,
                Height = 27
            };
            loadCSV.Click += LoadCSV;
            SPCSV.Children.Add(loadCSV);

        }



        private void InitLastChange()

        {
            string datex;
            if (File.Exists(CSVPath + (FindName($"gi{GICount+1}bz") as TextBox).Text+".csv"))
            {
                datex = File.GetLastWriteTime(CSVPath + (FindName($"gi{GICount + 1}bz") as TextBox).Text + ".csv").ToString();
            }
            else
            {
                datex = "Keine CSV Datei!!";
            }
            TextBlock lastchange = new TextBlock
            {
                Name = $"gi{GICount}lc",
                Text = datex,
                Margin = new Thickness(0, 0, 0, 10),
                FontWeight = FontWeights.Bold,
                Height = 27
            };


            HorizontalAlignment = HorizontalAlignment.Center;
            RegisterTextBlock($"gi{GICount}lc", lastchange);
            SPLastChange.Children.Add(lastchange);
        }



        private void InitBezeichner()
        {
            TextBox bezeichner = new TextBox
            {
                Name = $"gi{GICount}bz",
                Margin = new Thickness(0, 0, 0, 10),
                FontWeight = FontWeights.Bold,
                Height = 27,
                MinWidth = 200
            };
            RegisterNewTextBox($"gi{GICount}bz", bezeichner);
            SPBezeichner.Children.Add(bezeichner);
        }




        // INIT END ------------------------------




        // REGISTER USERCONTROL-ELEMENTS ---------

        private void RegisterTextBlock(string textBlockName, TextBlock textBlock)
        {
            if ((TextBlock)FindName(textBlockName) != null)
            {
                UnregisterName(textBlockName);
            }

            RegisterName(textBlockName, textBlock);
        }

        private void RegisterCBX(string textBlockName, ComboBox textBlock)
        {
            if ((ComboBox)FindName(textBlockName) != null)
            {
                UnregisterName(textBlockName);
            }

            RegisterName(textBlockName, textBlock);
        }



        private void RegisterNewTextBox(string textboxname, TextBox textbox)
        {
            if ((TextBox)FindName(textboxname) != null)
            {
                UnregisterName(textboxname);
            }

            RegisterName(textboxname, textbox);
        }
        // REGISTER USERCONTROL-ELEMENTS END------



        private void RemoveFromGI_click(object sender, RoutedEventArgs e)
        {
            if (SPCount.Children.Count > 1)
            {
                SPCount.Children.RemoveAt(SPCount.Children.Count - 1);
                SPCSV.Children.RemoveAt(SPCount.Children.Count);
                SPBezeichner.Children.RemoveAt(SPCount.Children.Count);
                SPLastChange.Children.RemoveAt(SPCount.Children.Count);

                GICount = GICount - 1;
            }
            try
            {
                File.Delete(CSVPath + "Zentrale" + (GICount + 1) + ".csv");
            }
            catch { }
        }



        // CSV Datei in Datenbankordner kopieren
        private void LoadCSV(object sender, RoutedEventArgs e)
        {
            string filename = (FindName($"gi{Convert.ToInt32(((Button)sender).Name.ToString().Replace("Zentrale", ""))}bz") as TextBox).Text;
                
           
            CSVImport(filename);


            
            for (int i = 1; i < GICount + 1; i++)
            {
                (FindName("gi" + i + "lc") as TextBlock).Text = File.GetLastWriteTime(CSVPath + "Zentrale" + i + ".csv").ToString();

            }


            
        }






        public void CSVImport(string csvfilename)
        {
            ofd.Filter = "CSV Datei|*.csv";
            ofd.ShowDialog();
            try
            {
                File.Copy(ofd.FileName, temppath, true);
            }
            catch
            {
                MessageBox.Show("Du musst schon ne Datei auswählen ;)");
            }
            for (int i = 0; i < 12; i++)
            {
                read(1);

            }
            File.Copy(temppath, CSVPath + csvfilename + ".csv", true);
            try
            {
                
            }
            catch
            {
                MessageBox.Show("Probiers nochmal!");
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
    }
}
