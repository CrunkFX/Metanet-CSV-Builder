using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Metanet_CSV_Builder
{



    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class SubnetB : MetroWindow
    {
        private readonly string CSVPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/Hussmann/MetanetCSV/DB/SubnetB/CSV/";
        private readonly string CSVTemp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/Hussmann/MetanetCSV/Temp/SubnetB/CSV/";
        private readonly string ConfigFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/Hussmann/MetanetCSV/Temp/SubnetB/";
        private readonly string SysDB = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/Hussmann/MetanetCSV/DB/SubnetB/DB.mcb";
        private int GICount = 0;
        private readonly OpenFileDialog ofd = new OpenFileDialog();
        private readonly SaveFileDialog sfd = new SaveFileDialog();
        private readonly string temppath = System.IO.Path.GetTempFileName();



        public SubnetB()
        {
            InitializeComponent();
            App.Current.Properties["DevMGR"] = "SubnetB";


            ok.Visibility = Visibility.Hidden;
            GICount = 1;
            InitCounter();
            InitLastChange();
            InitBezeichner();
            InitCSV();
            InitMGIStart();
            InitMGIEnd();
            InitSGIStart();
            InitSGIEnd();
            getFileNames();

            for (int i = 1; i < Convert.ToInt32(File.ReadLines(SysDB).Skip(0).Take(1).First()); i++)
            {
                GICount = GICount + 1;
                InitCounter();
                InitLastChange();
                InitBezeichner();
                InitCSV();
                InitMGIStart();
                InitMGIEnd();
                InitSGIStart();
                InitSGIEnd();
                getFileNames();
            }

            for (int i = 0; i < GICount; i++)
            {
                try
                {
                    string line = File.ReadLines(SysDB).Skip(i + 4).Take(1).First();
                    List<string> List1 = line.Split(',').ToList();
                    (FindName($"gi{i + 1}bz") as TextBox).Text = List1[0];
                    (FindName($"gi{i + 1}s") as NumericUpDown).Value = Convert.ToInt32(List1[1]);
                    (FindName($"gi{i + 1}e") as NumericUpDown).Value = Convert.ToInt32(List1[2]);
                    (FindName($"sgi{i + 1}s") as NumericUpDown).Value = Convert.ToInt32(List1[3]);
                    (FindName($"sgi{i + 1}e") as NumericUpDown).Value = Convert.ToInt32(List1[4]);
                    (FindName($"cbx{i + 1}") as ComboBox).SelectedItem = (List1[5]);
                }
                catch { }
            }

        }

        public void getFileNames()
        {
            foreach (string file in System.IO.Directory.GetFiles(CSVPath))
            {
                (FindName($"cbx{GICount}") as ComboBox).Items.Add(System.IO.Path.GetFileNameWithoutExtension(file));
            }
        }
        public void refreshFilenames()
        {




            for (int i = 0; i < GICount; i++)
            {

                (FindName($"cbx{i + 1}") as ComboBox).Items.Clear();

                foreach (string file1 in System.IO.Directory.GetFiles(CSVPath))
                {



                    (FindName($"cbx{i + 1}") as ComboBox).Items.Add(System.IO.Path.GetFileNameWithoutExtension(file1));
                }
                try
                {
                    string line = File.ReadLines(SysDB).Skip(i + 4).Take(1).First();
                    List<string> List1 = line.Split(',').ToList();
                    (FindName($"cbx{i + 1}") as ComboBox).SelectedItem = (List1[5]);
                }
                catch { }
            }

        }
        private void AddToGI_click(object sender, RoutedEventArgs e)
        {

            if ((FindName($"gi{GICount}bz") as TextBox).Text.Length >= 1)
            {
                GICount = GICount + 1;
                InitCounter();
                InitLastChange();
                InitBezeichner();
                InitCSV();
                InitMGIStart();
                InitMGIEnd();
                InitSGIStart();
                InitSGIEnd();
                foreach (string file in System.IO.Directory.GetFiles(CSVPath))
                {
                    (FindName($"cbx{GICount}") as ComboBox).Items.Add(System.IO.Path.GetFileNameWithoutExtension(file));
                }
            }
            else
            {
                _ = UTS();
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
            ComboBox cbx = new ComboBox
            {

                Name = "Zentrale" + GICount.ToString(),
                Margin = new Thickness(0, 0, 0, 10),
                FontWeight = FontWeights.Bold,
                Height = 27,

            };

            cbx.SelectionChanged += LoadCSV;
            RegisterCBX($"cbx{GICount}", cbx);
            SPCSV.Children.Add(cbx);
            /*
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
            */
        }



        private void InitLastChange()

        {
            string datex;
            if (File.Exists(CSVPath + $"Zentrale{GICount}.csv"))
            {
                datex = File.GetLastWriteTime(CSVPath + $"Zentrale{GICount}.csv").ToString();
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



        private void InitMGIStart()
        {
            NumericUpDown numgistart = new NumericUpDown
            {
                Name = $"gi{GICount}s",
                Margin = new Thickness(0, 0, 0, 10),
                FontWeight = FontWeights.Bold,
                Height = 27,
                MinWidth = 80,
                Minimum = 0,
                Maximum = 9999,
                NumericInputMode = NumericInput.Numbers,
                HideUpDownButtons = true,
                Value = 0
            };
            RegisterNumBlock($"gi{GICount}s", numgistart);
            SPMGS.Children.Add(numgistart);
        }



        private void InitMGIEnd()
        {
            NumericUpDown numgiend = new NumericUpDown
            {
                Name = $"gi{GICount}e",
                Margin = new Thickness(0, 0, 0, 10),
                FontWeight = FontWeights.Bold,
                Height = 27,
                MinWidth = 80,
                Minimum = 0,
                Maximum = 9999,
                NumericInputMode = NumericInput.Numbers,
                HideUpDownButtons = true,
                Value = 0
            };
            RegisterNumBlock($"gi{GICount}e", numgiend);
            SPMGE.Children.Add(numgiend);
        }



        private void InitSGIStart()
        {
            NumericUpDown numsgistart = new NumericUpDown
            {
                Name = $"sgi{GICount}s",
                Margin = new Thickness(0, 0, 0, 10),
                FontWeight = FontWeights.Bold,
                Height = 27,
                MinWidth = 80,
                Minimum = 0,
                Maximum = 9999,
                NumericInputMode = NumericInput.Numbers,
                HideUpDownButtons = true,
                Value = 0
            };
            RegisterNumBlock($"sgi{GICount}s", numsgistart);
            SPSGS.Children.Add(numsgistart);
        }



        private void InitSGIEnd()
        {
            NumericUpDown numsgiend = new NumericUpDown
            {
                Name = $"sgi{GICount}e",
                Margin = new Thickness(0, 0, 0, 10),
                FontWeight = FontWeights.Bold,
                Height = 27,
                MinWidth = 80,
                Minimum = 0,
                Maximum = 9999,
                NumericInputMode = NumericInput.Numbers,
                HideUpDownButtons = true,
                Value = 0
            };
            RegisterNumBlock($"sgi{GICount}e", numsgiend);
            SPSGE.Children.Add(numsgiend);
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

        private void RegisterNumBlock(string numname, NumericUpDown num)
        {
            if ((NumericUpDown)FindName(numname) != null)
            {
                UnregisterName(numname);
            }

            RegisterName(numname, num);
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
                SPMGE.Children.RemoveAt(SPCount.Children.Count);
                SPMGS.Children.RemoveAt(SPCount.Children.Count);
                SPSGE.Children.RemoveAt(SPCount.Children.Count);
                SPSGS.Children.RemoveAt(SPCount.Children.Count);
                GICount = GICount - 1;
            }
            try
            {
                File.Delete(CSVPath + "Zentrale" + (GICount + 1) + ".csv");
            }
            catch { }
        }



        // CSV Datei in Datenbankordner kopieren
        private void LoadCSV(object sender, SelectionChangedEventArgs e)
        {
            for (int i = 1; i < GICount + 1; i++)
            {
                (FindName("gi" + i + "lc") as TextBlock).Text = File.GetLastWriteTime(CSVPath + "Zentrale" + i + ".csv").ToString();

            }


            for (int i = 1; i < GICount + 1; i++)
            {
                (FindName("gi" + i + "lc") as TextBlock).Text = File.GetLastWriteTime(CSVPath + (FindName("cbx" + i) as ComboBox).SelectedItem + ".csv").ToString();

            }
        }











        private async Task Doit()
        {
            ok.Visibility = Visibility.Visible;
            await Task.Delay(2000);
            ok.Visibility = Visibility.Hidden;
        }
        private void SaveConfig()
        {
            ok.Visibility = Visibility.Visible;
            File.Delete(SysDB);
            File.AppendAllText(SysDB, GICount.ToString());
            File.AppendAllText(SysDB, "\n");
            File.AppendAllText(SysDB, "\n");
            File.AppendAllText(SysDB, "\n");
            File.AppendAllText(SysDB, "\n");
            try
            {
                for (int i = 0; i < GICount; i++)
                {
                    File.AppendAllText(SysDB, (FindName($"gi{i + 1}bz") as TextBox).Text + "," + (FindName($"gi{i + 1}s") as NumericUpDown).Value.ToString() + "," + (FindName($"gi{i + 1}e") as NumericUpDown).Value.ToString() + "," + (FindName($"sgi{i + 1}s") as NumericUpDown).Value.ToString() + "," + (FindName($"sgi{i + 1}e") as NumericUpDown).Value.ToString() + "," + (FindName($"cbx{i + 1}") as ComboBox).SelectedItem.ToString() + "\n");
                }
            }
            catch
            {
                MessageBox.Show("Bitte Konfiguration überprüfen");
            }
            _ = Doit();

        }

        private void savedb_Click(object sender, RoutedEventArgs e)
        {


            SaveConfig();

        }

        private readonly string CSVP1Temp = System.IO.Path.GetTempFileName();
        public void CreateCSVP1(string csvno, string Zentralenadresse, int RepeatGA, string GAStart, string GAEnd, string SGAStart, string SGAEnd, int RepeatSGA)
        {

            using (StreamReader reader = new StreamReader(CSVPath + (FindName($"cbx1") as ComboBox).SelectedItem + ".csv"))
            {


                using (StreamWriter writer = new StreamWriter(CSVP1Temp))
                {
                    int counter = 0;
                    string line;
                    writer.WriteLine("Komponente,Zusatztext,Bezeichnung,Bezeichner");
                    while ((line = reader.ReadLine()) != null)
                    {



                        if (line.Contains("Zentrale") == true)
                        {


                            writer.WriteLine(line);
                        };
                        if (line.Contains("Primärleitung") == true)
                        {


                            writer.WriteLine(line);
                        };





                        counter++;
                    }


                }
            }
            for (int i = 0; i < RepeatGA; i++)
            {
                int c = i + Convert.ToInt32(GAStart);
                System.IO.File.AppendAllText(CSVP1Temp, "Meldergruppe " + c + ";;G/" + c + ";" + Zentralenadresse + "." + c + "\r\n");

            }

            for (int i = 0; i < RepeatSGA; i++)
            {
                int c = i + Convert.ToInt32(SGAStart);
                System.IO.File.AppendAllText(CSVP1Temp, "Steuergruppen " + c + ";;S/" + c + ";" + Zentralenadresse + "." + c + "\r\n");

            }

        }

        string temppath3 = System.IO.Path.GetTempFileName();
        public void CreateCSV2(string csvno, string Zentralenadresse, int RepeatGA, string GAStart, string GAEnd, string SGAStart, string SGAEnd, int RepeatSGA)
        {

            using (StreamReader reader = new StreamReader(CSVPath + (FindName($"cbx{csvno}") as ComboBox).SelectedItem + ".csv"))
            {


                using (StreamWriter writer = new StreamWriter(temppath3))
                {
                    int counter = 0;
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {



                        if (line.Contains("Zentrale") == true)
                        {


                            writer.WriteLine(line);
                        };
                        if (line.Contains("Primärleitung") == true)
                        {


                            writer.WriteLine(line);
                        }




                        counter++;
                    }


                }
            }
            for (int i = 0; i < RepeatGA; i++)
            {
                int c = i + Convert.ToInt32(GAStart);
                System.IO.File.AppendAllText(temppath3, "Meldergruppe " + c + ";;G/" + c + ";" + Zentralenadresse + "." + c + "\r\n");

            }
            for (int i = 0; i < RepeatSGA; i++)
            {
                int c = i + Convert.ToInt32(SGAStart);
                System.IO.File.AppendAllText(temppath3, "Steuergruppe " + c + ";;S/" + c + ";" + Zentralenadresse + "." + c + "\r\n");

            }


        }



        private void createbt_Click(object sender, RoutedEventArgs e)
        {
            SaveConfig();

            List<string> uidoe = new List<string>();



            CreateCSVP1("1", (FindName("gi1bz") as TextBox).Text, (Convert.ToInt32((FindName("gi1e") as NumericUpDown).Value) - Convert.ToInt32((FindName("gi1s") as NumericUpDown).Value) + 1), (FindName("gi1s") as NumericUpDown).Value.ToString(), (FindName("gi1e") as NumericUpDown).Value.ToString(), (FindName("sgi1s") as NumericUpDown).Value.ToString(), (FindName("sgi1s") as NumericUpDown).Value.ToString(), (Convert.ToInt32((FindName("sgi1e") as NumericUpDown).Value) - Convert.ToInt32((FindName("sgi1s") as NumericUpDown).Value) + 1));

            File.Copy(CSVP1Temp, CSVTemp + "temp1.oix", true);
            uidoe.Add(CSVTemp + "temp1.oix");




            for (int i = 2; i < GICount + 1; i++)
            {

                CreateCSV2((i).ToString(), (FindName("gi" + i + "bz") as TextBox).Text, (Convert.ToInt32((FindName("gi" + i + "e") as NumericUpDown).Value) - Convert.ToInt32((FindName("gi" + i + "s") as NumericUpDown).Value) + 1), (FindName("gi" + i + "s") as NumericUpDown).Value.ToString(), (FindName("gi" + i + "e") as NumericUpDown).Value.ToString(), (FindName("sgi" + i + "s") as NumericUpDown).Value.ToString(), (FindName("sgi" + i + "e") as NumericUpDown).Value.ToString(), (Convert.ToInt32((FindName("sgi" + i + "e") as NumericUpDown).Value) - Convert.ToInt32((FindName("sgi" + i + "s") as NumericUpDown).Value) + 1));
                File.Copy(temppath3, CSVTemp + "temp" + (i) + ".oix", true);
                uidoe.Add(CSVTemp + "temp" + i + ".oix");
            }












            const int chunkSize = 2 * 1024; // 2KB


            List<string> inputFiles = uidoe;
            

            //try
            //{
            using (FileStream output = File.Create(ConfigFolder+"intervals.xml"))
            {
                foreach (string file in inputFiles)
                {
                    using (FileStream input = File.OpenRead(file))
                    {
                        byte[] buffer = new byte[chunkSize];
                        int bytesRead;
                        while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            output.Write(buffer, 0, bytesRead);
                        }

                    }
                }
            }
            System.IO.DirectoryInfo di = new DirectoryInfo(CSVTemp);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            //}
            // catch
            // {
            //    MessageBox.Show("Du musst schon ne Datei auswählen!!!");
            // }
        }

        private async Task OK()
        {
            await Task.Delay(2000);
            ok.Visibility = Visibility.Visible;
            await Task.Delay(2000);
            ok.Visibility = Visibility.Hidden;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OpenDevMGR(object sender, RoutedEventArgs e)
        {
            Anlagen win2 = new Anlagen();
            win2.Show();
            win2.Owner = this;



        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Start win2 = new Start();
            win2.Show();

        }
    }
}
