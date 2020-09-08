using ControlzEx.Theming;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
        private readonly string CSVPath = "bin/opt/Backbone/";
        private readonly string CSVTemp = "bin/lib/temp/";
        private readonly string DB = "bin/lib/db/Database.mcb";
        private readonly string SysDB = "bin/lib/db/Backbone.mcb";
        private int GICount = 0;
        private readonly OpenFileDialog ofd = new OpenFileDialog();
        private readonly SaveFileDialog sfd = new SaveFileDialog();
        private readonly string temppath = System.IO.Path.GetTempFileName();



        public SubnetB()
        {
            InitializeComponent();

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

            }

            /* for (int i = 1; i < 31; i++)
             {
                 (FindName("gi" + i + "lc") as Label).Content = File.GetLastWriteTime(CSVPath + "Zentrale " + i + ".csv").ToString();
                 (FindName("gi" + i + "lc") as Label).Height = 27;
             }

             string[] result = File.ReadAllLines(DB);
             // try
             // {
             Parallel.For(0, result.Length, i =>
             {
                 if (string.IsNullOrWhiteSpace(result[i]))
                 {
                     result[i] = null;
                 }
             });
             for (int i = 0; i < 30; i++)
             {
                 (FindName("gi" + (i + 1) + "bz") as TextBox).Text = result[i];
                 (FindName("gi" + (i + 1) + "bz") as TextBox).Height = 27;
                 (FindName("gi" + (i + 1) + "bz") as TextBox).Visibility = Visibility.Hidden;
             }
             for (int i = 0; i < 30; i++)
             {
                 (FindName("gi" + (i + 1) + "s") as NumericUpDown).Value = Convert.ToInt16(result[(i + 30)]);
                 (FindName("gi" + (i + 1) + "s") as NumericUpDown).Height = 27;
                 (FindName("gi" + (i + 1) + "s") as NumericUpDown).Visibility = Visibility.Hidden;
             }
             for (int i = 0; i < 30; i++)
             {
                 (FindName("gi" + (i + 1) + "e") as NumericUpDown).Value = Convert.ToInt16(result[i + 60]);
                 (FindName("gi" + (i + 1) + "e") as NumericUpDown).Height = 27;
                 (FindName("gi" + (i + 1) + "e") as NumericUpDown).Visibility = Visibility.Hidden;
             }
             for (int i = 0; i < 30; i++)
             {
                 (FindName("sgi" + (i + 1) + "s") as NumericUpDown).Value = Convert.ToInt16(result[(i + 90)]);
                 (FindName("sgi" + (i + 1) + "s") as NumericUpDown).Height = 27;
                 (FindName("sgi" + (i + 1) + "s") as NumericUpDown).Visibility = Visibility.Hidden;
             }
             for (int i = 0; i < 30; i++)
             {
                 (FindName("sgi" + (i + 1) + "e") as NumericUpDown).Value = Convert.ToInt16(result[i + 120]);
                 (FindName("sgi" + (i + 1) + "e") as NumericUpDown).Height = 27;
                 (FindName("sgi" + (i + 1) + "e") as NumericUpDown).Visibility = Visibility.Hidden;
             }
             for (int i = 0; i < 30; i++)
             {

                 (FindName("gi" + (i + 1) + "load") as Button).Height = 27;
                 (FindName("gi" + (i + 1) + "load") as Button).Visibility = Visibility.Hidden;
             }

             for (int i = 1; i < 30; i++)
             {

                 if ((FindName("gi" + (i) + "bz") as TextBox).Text.Length > 0)
                 {


                     (FindName("gi" + (i + 1) + "load") as Button).Visibility = Visibility.Visible;
                     (FindName("gi" + (i + 1) + "lc") as Label).Visibility = Visibility.Visible;
                     (FindName("gi" + (i + 1) + "bz") as TextBox).Visibility = Visibility.Visible;
                     (FindName("gi" + (i + 1) + "s") as NumericUpDown).Visibility = Visibility.Visible;
                     (FindName("gi" + (i + 1) + "e") as NumericUpDown).Visibility = Visibility.Visible;
                     (FindName("sgi" + (i + 1) + "s") as NumericUpDown).Visibility = Visibility.Visible;
                     (FindName("sgi" + (i + 1) + "e") as NumericUpDown).Visibility = Visibility.Visible;
                 }
             }
             gi1load.Visibility = Visibility.Visible;
             gi1lc.Visibility = Visibility.Visible;
             gi1bz.Visibility = Visibility.Visible;
             gi1s.Visibility = Visibility.Visible;
             gi1e.Visibility = Visibility.Visible;
             sgi1s.Visibility = Visibility.Visible;
             sgi1e.Visibility = Visibility.Visible;

     */
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
            RegisterNumBlock($"gi{GICount}s", numgiend);
            SPMGE.Children.Add(numgiend);
        }

        private void InitSGIStart()
        {
            NumericUpDown numsgistart = new NumericUpDown
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
            RegisterNumBlock($"gi{GICount}s", numsgistart);
            SPSGS.Children.Add(numsgistart);
        }

        private void InitSGIEnd()
        {
            NumericUpDown numsgiend = new NumericUpDown
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
            RegisterNumBlock($"gi{GICount}s", numsgiend);
            SPSGE.Children.Add(numsgiend);
        }


        private void RegisterTextBlock(string textBlockName, TextBlock textBlock)
        {
            if ((TextBlock)FindName(textBlockName) != null)
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
        private void LoadCSV(object sender, RoutedEventArgs e)
        {
            ofd.ShowDialog();

            try
            {
                File.Copy(ofd.FileName, CSVPath + ((Button)sender).Name + ".csv", true);
            }
            catch
            {
                MessageBox.Show("Du musst schon ne Datei auswählen!");
            }
            for (int i = 1; i < GICount + 1; i++)
            {
                (FindName("gi" + i + "lc") as TextBlock).Text = File.GetLastWriteTime(CSVPath + "Zentrale" + i + ".csv").ToString();

            }
        }




        // Numbers Only für Gruppenintervalle
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

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


        public void CSVImport(string csvpath)
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
            try
            {
                File.Copy(temppath, CSVPath + csvpath, true);
            }
            catch
            {
                MessageBox.Show("Probiers nochmal!");
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ThemeManager.Current.ChangeTheme(this, "Dark.Red");
        }

        //CSV Laden


        private async Task Doit()
        {
            ok.Visibility = Visibility.Visible;
            await Task.Delay(2000);
            ok.Visibility = Visibility.Hidden;
        }


        private void savedb_Click(object sender, RoutedEventArgs e)
        {
            ok.Visibility = Visibility.Visible;
            string line = string.Empty;

            string tempdb = System.IO.Path.GetTempFileName();
            string[] lines = {GICount.ToString(),  };
            File.Delete(SysDB);
            System.IO.File.AppendAllLines(SysDB, lines);
            Doit();




        }

        private readonly string temppath2 = System.IO.Path.GetTempFileName();
        public void CreateCSV(string csvno, string Zentralenadresse, int Repeat, string GAStart, string GAEnd, string SGAStart, string SGAEnd)
        {

            using (StreamReader reader = new StreamReader(CSVPath + "Zentrale " + csvno + ".csv"))
            {


                using (StreamWriter writer = new StreamWriter(temppath2))
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
                        if (line.Contains("Steuergruppen") == true)
                        {


                            writer.WriteLine(line);
                        };




                        counter++;
                    }


                }
            }
            for (int i = 0; i < Repeat; i++)
            {
                int c = i + Convert.ToInt32(GAStart);
                System.IO.File.AppendAllText(temppath2, "Meldergruppe " + SGAStart + c + ";;G/" + SGAStart + c + ";" + Zentralenadresse + "." + SGAStart + c + "\r\n");

            }

        }

        private readonly string temppath3 = System.IO.Path.GetTempFileName();
        public void CreateCSV2(string csvno, string Zentralenadresse, int Repeat, string GAStart, string GAEnd, string SGAStart, string SGAEnd, int numnn)
        {

            using (StreamReader reader = new StreamReader(CSVPath + "Zentrale " + csvno + ".csv"))
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
                        };
                        if (line.Contains("Steuergruppen") == true)
                        {


                            writer.WriteLine(line);
                        };




                        counter++;
                    }


                }
            }
            for (int i = 0; i < Repeat; i++)
            {
                int c = i + Convert.ToInt32(GAStart);
                System.IO.File.AppendAllText(temppath3, "Meldergruppe " + SGAStart + c + ";;G/" + SGAStart + c + ";" + Zentralenadresse + "." + SGAStart + c + "\r\n");

            }
            File.Copy(temppath3, CSVTemp + "temp" + numnn + 1 + ".oix", true);

        }



        private void createbt_Click(object sender, RoutedEventArgs e)
        {


            int ocount = 1;
            for (int i = 1; i < 30; i++)
            {

                if ((FindName("gi" + (i) + "bz") as TextBox).Text.Length > 0)
                {
                    ocount = ocount + 1;


                }
            }
            List<string> uidoe = new List<string>();
            for (int i = 1; i < ocount; i++)
            {



                uidoe.Add(CSVTemp + "temp" + i + ".oix");

            }
            string[] str = uidoe.ToArray();
            File.Copy(temppath2, CSVTemp + "temp1.oix", true);

            for (int i = 1; i < ocount; i++)
            {


                CreateCSV2((i + 1).ToString(), (FindName("gi" + i + "bz") as TextBox).Text, (Convert.ToInt32((FindName("gi" + i + "e") as NumericUpDown).Value) - Convert.ToInt32((FindName("gi" + i + "s") as NumericUpDown).Value) + 1), (FindName("gi" + i + "s") as NumericUpDown).Value.ToString(), (FindName("gi" + i + "e") as NumericUpDown).Value.ToString(), "", "", i + 1);
            }

            const int chunkSize = 2 * 1024; // 2KB


            List<string> inputFiles = uidoe;
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "CSV Datei|*.csv"
            };
            sfd.ShowDialog();

            try
            {
                using (FileStream output = File.Create(sfd.FileName))
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
            }
            catch
            {
                MessageBox.Show("Du musst schon ne Datei auswählen!!!");
            }
        }

        private async Task OK()
        {
            Thread.Sleep(2000);
            ok.Visibility = Visibility.Visible;
            Thread.Sleep(2000);
            ok.Visibility = Visibility.Hidden;
        }

        private void gi1bz_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


    }
}
