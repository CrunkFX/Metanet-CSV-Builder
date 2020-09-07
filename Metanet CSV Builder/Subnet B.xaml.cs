using ControlzEx.Theming;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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


        string CSVPath = "bin/opt/Backbone/";
        string CSVTemp = "bin/lib/temp/";
        string DB = "bin/lib/db/Database.mcb";
        int GICount = 0;

        OpenFileDialog ofd = new OpenFileDialog();
        SaveFileDialog sfd = new SaveFileDialog();
        string temppath = System.IO.Path.GetTempFileName();
        //Globale Variablen

        //CSV Dateiordner



        //Datenbank





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
            else {
                UTS();
            };
        }
        private async Task UTS()
        {
            (FindName($"gi{GICount}bz") as TextBox).Background = Brushes.Crimson;
            await Task.Delay(200);
            (FindName($"gi{GICount}bz") as TextBox).Background = Brushes.White;
            await Task.Delay(100);
            (FindName($"gi{GICount}bz") as TextBox).Background = Brushes.Crimson;
                await Task.Delay(200);
            (FindName($"gi{GICount}bz") as TextBox).Background = Brushes.White;
                await Task.Delay(100);
            (FindName($"gi{GICount}bz") as TextBox).Background = Brushes.Crimson;
            await Task.Delay(200);
            (FindName($"gi{GICount}bz") as TextBox).Background = Brushes.White;
        }

        void InitCounter()
        {
            SPCount.Children.Add(new Label() { Content = GICount.ToString(), Margin = new Thickness(0, 0, 0, 10), FontWeight = FontWeights.Bold, Height = 27 });
        }

        void InitCSV()
        {
            Button loadCSV = new Button();
            loadCSV.Content = GICount.ToString();
            loadCSV.Name = "Zentrale" + GICount.ToString();
            loadCSV.Margin = new Thickness(0, 0, 0, 10);
            loadCSV.FontWeight = FontWeights.Bold;
            loadCSV.Height = 27;
            loadCSV.Click += LoadCSV;
            SPCSV.Children.Add(loadCSV);
        }

        void InitLastChange()
        {
            TextBlock lastchange = new TextBlock();
            lastchange.Name = $"gi{GICount}lc";
            lastchange.Text = File.GetLastWriteTime(CSVPath + $"Zentrale{GICount}.csv").ToString();
            lastchange.Margin = new Thickness(0, 0, 0, 10);
            lastchange.FontWeight = FontWeights.Bold;
            lastchange.Height = 27;
            HorizontalAlignment = HorizontalAlignment.Center;
            RegisterTextBlock($"gi{GICount}lc", lastchange);
            SPLastChange.Children.Add(lastchange);
        }

        void InitBezeichner()
        {
            TextBox bezeichner = new TextBox();
            bezeichner.Name = $"gi{GICount}bz";
            bezeichner.Margin = new Thickness(0, 0, 0, 10);
            bezeichner.FontWeight = FontWeights.Bold;
            bezeichner.Height = 27;
            bezeichner.MinWidth = 200;
            RegisterNewTextBox($"gi{GICount}bz", bezeichner);
            SPBezeichner.Children.Add(bezeichner);
        }

        void InitMGIStart()
        {
            NumericUpDown numgistart = new NumericUpDown();
            numgistart.Name = $"gi{GICount}s";
            numgistart.Margin = new Thickness(0, 0, 0, 10);
            numgistart.FontWeight = FontWeights.Bold;
            numgistart.Height = 27;
            numgistart.MinWidth = 80;
            numgistart.Minimum = 0;
            numgistart.Maximum = 9999;
            numgistart.NumericInputMode = NumericInput.Numbers;
            numgistart.HideUpDownButtons = true;
            numgistart.Value = 0;
            RegisterNumBlock($"gi{GICount}s", numgistart);
            SPMGS.Children.Add(numgistart);
        }

        void InitMGIEnd()
        {
            NumericUpDown numgiend = new NumericUpDown();
            numgiend.Name = $"gi{GICount}s";
            numgiend.Margin = new Thickness(0, 0, 0, 10);
            numgiend.FontWeight = FontWeights.Bold;
            numgiend.Height = 27;
            numgiend.MinWidth = 80;
            numgiend.Minimum = 0;
            numgiend.Maximum = 9999;
            numgiend.NumericInputMode = NumericInput.Numbers;
            numgiend.HideUpDownButtons = true;
            numgiend.Value = 0;
            RegisterNumBlock($"gi{GICount}s", numgiend);
            SPMGE.Children.Add(numgiend);
        }

        void InitSGIStart()
        {
            NumericUpDown numsgistart = new NumericUpDown();
            numsgistart.Name = $"gi{GICount}s";
            numsgistart.Margin = new Thickness(0, 0, 0, 10);
            numsgistart.FontWeight = FontWeights.Bold;
            numsgistart.Height = 27;
            numsgistart.MinWidth = 80;
            numsgistart.Minimum = 0;
            numsgistart.Maximum = 9999;
            numsgistart.NumericInputMode = NumericInput.Numbers;
            numsgistart.HideUpDownButtons = true;
            numsgistart.Value = 0;
            RegisterNumBlock($"gi{GICount}s", numsgistart);
            SPSGS.Children.Add(numsgistart);
        }

        void InitSGIEnd()
        {
            NumericUpDown numsgiend = new NumericUpDown();
            numsgiend.Name = $"gi{GICount}s";
            numsgiend.Margin = new Thickness(0, 0, 0, 10);
            numsgiend.FontWeight = FontWeights.Bold;
            numsgiend.Height = 27;
            numsgiend.MinWidth = 80;
            numsgiend.Minimum = 0;
            numsgiend.Maximum = 9999;
            numsgiend.NumericInputMode = NumericInput.Numbers;
            numsgiend.HideUpDownButtons = true;
            numsgiend.Value = 0;
            RegisterNumBlock($"gi{GICount}s", numsgiend);
            SPSGE.Children.Add(numsgiend);
        }


        void RegisterTextBlock(string textBlockName, TextBlock textBlock)
        {
            if ((TextBlock)FindName(textBlockName) != null)
                UnregisterName(textBlockName);
            RegisterName(textBlockName, textBlock);
        }

        void RegisterNumBlock(string numname, NumericUpDown num)
        {
            if ((NumericUpDown)FindName(numname) != null)
                UnregisterName(numname);
            RegisterName(numname, num);
        }

        void RegisterNewTextBox(string textboxname, TextBox textbox)
        {
            if ((TextBox)FindName(textboxname) != null)
                UnregisterName(textboxname);
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
        }



        // CSV Datei in Datenbankordner kopieren
        private void LoadCSV(object sender, RoutedEventArgs e)
        {
            ofd.ShowDialog();
            File.Copy(ofd.FileName, CSVPath + ((Button)sender).Name + ".csv", true);
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
            //string[] lines = { gi1bz.Text, gi2bz.Text, gi3bz.Text, gi4bz.Text, gi5bz.Text, gi6bz.Text, gi7bz.Text, gi8bz.Text, gi9bz.Text, gi10bz.Text, gi11bz.Text, gi12bz.Text, gi13bz.Text, gi14bz.Text, gi15bz.Text, gi16bz.Text, gi17bz.Text, gi18bz.Text, gi19bz.Text, gi20bz.Text, gi21bz.Text, gi22bz.Text, gi23bz.Text, gi24bz.Text, gi25bz.Text, gi26bz.Text, gi27bz.Text, gi28bz.Text, gi29bz.Text, gi30bz.Text, gi1s.Value.ToString(), gi2s.Value.ToString(), gi3s.Value.ToString(), gi4s.Value.ToString(), gi5s.Value.ToString(), gi6s.Value.ToString(), gi7s.Value.ToString(), gi8s.Value.ToString(), gi9s.Value.ToString(), gi10s.Value.ToString(), gi11s.Value.ToString(), gi12s.Value.ToString(), gi13s.Value.ToString(), gi14s.Value.ToString(), gi15s.Value.ToString(), gi16s.Value.ToString(), gi17s.Value.ToString(), gi18s.Value.ToString(), gi19s.Value.ToString(), gi20s.Value.ToString(), gi21s.Value.ToString(), gi22s.Value.ToString(), gi23s.Value.ToString(), gi24s.Value.ToString(), gi25s.Value.ToString(), gi26s.Value.ToString(), gi27s.Value.ToString(), gi28s.Value.ToString(), gi29s.Value.ToString(), gi30s.Value.ToString(), gi1e.Value.ToString(), gi2e.Value.ToString(), gi3e.Value.ToString(), gi4e.Value.ToString(), gi5e.Value.ToString(), gi6e.Value.ToString(), gi7e.Value.ToString(), gi8e.Value.ToString(), gi9e.Value.ToString(), gi10e.Value.ToString(), gi11e.Value.ToString(), gi12e.Value.ToString(), gi13e.Value.ToString(), gi14e.Value.ToString(), gi15e.Value.ToString(), gi16e.Value.ToString(), gi17e.Value.ToString(), gi18e.Value.ToString(), gi19e.Value.ToString(), gi20e.Value.ToString(), gi21e.Value.ToString(), gi22e.Value.ToString(), gi23e.Value.ToString(), gi24e.Value.ToString(), gi25e.Value.ToString(), gi26e.Value.ToString(), gi27e.Value.ToString(), gi28e.Value.ToString(), gi29e.Value.ToString(), gi30e.Value.ToString(), sgi1s.Value.ToString(), sgi2s.Value.ToString(), sgi3s.Value.ToString(), sgi4s.Value.ToString(), sgi5s.Value.ToString(), sgi6s.Value.ToString(), sgi7s.Value.ToString(), sgi8s.Value.ToString(), sgi9s.Value.ToString(), sgi10s.Value.ToString(), sgi11s.Value.ToString(), sgi12s.Value.ToString(), sgi13s.Value.ToString(), sgi14s.Value.ToString(), sgi15s.Value.ToString(), sgi16s.Value.ToString(), sgi17s.Value.ToString(), sgi18s.Value.ToString(), sgi19s.Value.ToString(), sgi20s.Value.ToString(), sgi21s.Value.ToString(), sgi22s.Value.ToString(), sgi23s.Value.ToString(), sgi24s.Value.ToString(), sgi25s.Value.ToString(), sgi26s.Value.ToString(), sgi27s.Value.ToString(), sgi28s.Value.ToString(), sgi29s.Value.ToString(), sgi30s.Value.ToString(), sgi1e.Value.ToString(), sgi2e.Value.ToString(), sgi3e.Value.ToString(), sgi4e.Value.ToString(), sgi5e.Value.ToString(), sgi6e.Value.ToString(), sgi7e.Value.ToString(), sgi8e.Value.ToString(), sgi9e.Value.ToString(), sgi10e.Value.ToString(), sgi11e.Value.ToString(), sgi12e.Value.ToString(), sgi13e.Value.ToString(), sgi14e.Value.ToString(), sgi15e.Value.ToString(), sgi16e.Value.ToString(), sgi17e.Value.ToString(), sgi18e.Value.ToString(), sgi19e.Value.ToString(), sgi20e.Value.ToString(), sgi21e.Value.ToString(), sgi22e.Value.ToString(), sgi23e.Value.ToString(), sgi24e.Value.ToString(), sgi25e.Value.ToString(), sgi26e.Value.ToString(), sgi27e.Value.ToString(), sgi28e.Value.ToString(), sgi29e.Value.ToString(), sgi30e.Value.ToString() };
            File.Delete(DB);
            // System.IO.File.AppendAllLines(DB, lines);
            Doit();




        }
        string temppath2 = System.IO.Path.GetTempFileName();
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


        string temppath3 = System.IO.Path.GetTempFileName();
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
            String[] str = uidoe.ToArray();
            File.Copy(temppath2, CSVTemp + "temp1.oix", true);

            for (int i = 1; i < ocount; i++)
            {


                CreateCSV2((i + 1).ToString(), (FindName("gi" + i + "bz") as TextBox).Text, (Convert.ToInt32((FindName("gi" + i + "e") as NumericUpDown).Value) - Convert.ToInt32((FindName("gi" + i + "s") as NumericUpDown).Value) + 1), (FindName("gi" + i + "s") as NumericUpDown).Value.ToString(), (FindName("gi" + i + "e") as NumericUpDown).Value.ToString(), "", "", i + 1);
            }

            const int chunkSize = 2 * 1024; // 2KB


            var inputFiles = uidoe;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV Datei|*.csv";
            sfd.ShowDialog();

            try
            {
                using (var output = File.Create(sfd.FileName))
                {
                    foreach (var file in inputFiles)
                    {
                        using (var input = File.OpenRead(file))
                        {
                            var buffer = new byte[chunkSize];
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


        async Task OK()
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
