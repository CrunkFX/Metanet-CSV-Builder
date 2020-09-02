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

namespace Metanet_CSV_Builder
{



    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class SubnetB : MetroWindow
    {
        OpenFileDialog ofd = new OpenFileDialog();
        SaveFileDialog sfd = new SaveFileDialog();
        string temppath = System.IO.Path.GetTempFileName();
        //Globale Variablen

        //CSV Dateiordner
        string CSVPath = "bin/opt/Backbone/";
        string CSVTemp = "bin/lib/temp/";

        //Datenbank
        string DB = "bin/lib/db/Database.mcb";




        public SubnetB()
        {
            InitializeComponent();
            ok.Visibility = Visibility.Hidden;
            for (int i = 1; i < 31; i++)
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

        private void gi1load_Click(object sender, RoutedEventArgs e)
        {
            CSVImport("Zentrale 1.csv");
            gi1lc.Content = File.GetLastWriteTime(CSVPath + "Zentrale 1.csv").ToString();
        }
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
            string[] lines = { gi1bz.Text, gi2bz.Text, gi3bz.Text, gi4bz.Text, gi5bz.Text, gi6bz.Text, gi7bz.Text, gi8bz.Text, gi9bz.Text, gi10bz.Text, gi11bz.Text, gi12bz.Text, gi13bz.Text, gi14bz.Text, gi15bz.Text, gi16bz.Text, gi17bz.Text, gi18bz.Text, gi19bz.Text, gi20bz.Text, gi21bz.Text, gi22bz.Text, gi23bz.Text, gi24bz.Text, gi25bz.Text, gi26bz.Text, gi27bz.Text, gi28bz.Text, gi29bz.Text, gi30bz.Text, gi1s.Value.ToString(), gi2s.Value.ToString(), gi3s.Value.ToString(), gi4s.Value.ToString(), gi5s.Value.ToString(), gi6s.Value.ToString(), gi7s.Value.ToString(), gi8s.Value.ToString(), gi9s.Value.ToString(), gi10s.Value.ToString(), gi11s.Value.ToString(), gi12s.Value.ToString(), gi13s.Value.ToString(), gi14s.Value.ToString(), gi15s.Value.ToString(), gi16s.Value.ToString(), gi17s.Value.ToString(), gi18s.Value.ToString(), gi19s.Value.ToString(), gi20s.Value.ToString(), gi21s.Value.ToString(), gi22s.Value.ToString(), gi23s.Value.ToString(), gi24s.Value.ToString(), gi25s.Value.ToString(), gi26s.Value.ToString(), gi27s.Value.ToString(), gi28s.Value.ToString(), gi29s.Value.ToString(), gi30s.Value.ToString(), gi1e.Value.ToString(), gi2e.Value.ToString(), gi3e.Value.ToString(), gi4e.Value.ToString(), gi5e.Value.ToString(), gi6e.Value.ToString(), gi7e.Value.ToString(), gi8e.Value.ToString(), gi9e.Value.ToString(), gi10e.Value.ToString(), gi11e.Value.ToString(), gi12e.Value.ToString(), gi13e.Value.ToString(), gi14e.Value.ToString(), gi15e.Value.ToString(), gi16e.Value.ToString(), gi17e.Value.ToString(), gi18e.Value.ToString(), gi19e.Value.ToString(), gi20e.Value.ToString(), gi21e.Value.ToString(), gi22e.Value.ToString(), gi23e.Value.ToString(), gi24e.Value.ToString(), gi25e.Value.ToString(), gi26e.Value.ToString(), gi27e.Value.ToString(), gi28e.Value.ToString(), gi29e.Value.ToString(), gi30e.Value.ToString(), sgi1s.Value.ToString(), sgi2s.Value.ToString(), sgi3s.Value.ToString(), sgi4s.Value.ToString(), sgi5s.Value.ToString(), sgi6s.Value.ToString(), sgi7s.Value.ToString(), sgi8s.Value.ToString(), sgi9s.Value.ToString(), sgi10s.Value.ToString(), sgi11s.Value.ToString(), sgi12s.Value.ToString(), sgi13s.Value.ToString(), sgi14s.Value.ToString(), sgi15s.Value.ToString(), sgi16s.Value.ToString(), sgi17s.Value.ToString(), sgi18s.Value.ToString(), sgi19s.Value.ToString(), sgi20s.Value.ToString(), sgi21s.Value.ToString(), sgi22s.Value.ToString(), sgi23s.Value.ToString(), sgi24s.Value.ToString(), sgi25s.Value.ToString(), sgi26s.Value.ToString(), sgi27s.Value.ToString(), sgi28s.Value.ToString(), sgi29s.Value.ToString(), sgi30s.Value.ToString(), sgi1e.Value.ToString(), sgi2e.Value.ToString(), sgi3e.Value.ToString(), sgi4e.Value.ToString(), sgi5e.Value.ToString(), sgi6e.Value.ToString(), sgi7e.Value.ToString(), sgi8e.Value.ToString(), sgi9e.Value.ToString(), sgi10e.Value.ToString(), sgi11e.Value.ToString(), sgi12e.Value.ToString(), sgi13e.Value.ToString(), sgi14e.Value.ToString(), sgi15e.Value.ToString(), sgi16e.Value.ToString(), sgi17e.Value.ToString(), sgi18e.Value.ToString(), sgi19e.Value.ToString(), sgi20e.Value.ToString(), sgi21e.Value.ToString(), sgi22e.Value.ToString(), sgi23e.Value.ToString(), sgi24e.Value.ToString(), sgi25e.Value.ToString(), sgi26e.Value.ToString(), sgi27e.Value.ToString(), sgi28e.Value.ToString(), sgi29e.Value.ToString(), sgi30e.Value.ToString() };
            File.Delete(DB);
            System.IO.File.AppendAllLines(DB, lines);
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
            CreateCSV("1", gi1bz.Text, (Convert.ToInt32(gi1e.Value) - Convert.ToInt32(gi1s.Value) + 1), gi1s.Value.ToString(), gi1e.Value.ToString(), "", "");
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
        private void gi1bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi1bz.Text.Length > 0)
            {
                gi2load.Visibility = Visibility.Visible;
                gi2lc.Visibility = Visibility.Visible;
                gi2bz.Visibility = Visibility.Visible;
                gi2s.Visibility = Visibility.Visible;
                gi2e.Visibility = Visibility.Visible;
                sgi2s.Visibility = Visibility.Visible;
                sgi2e.Visibility = Visibility.Visible;
            }
        }
        private void gi2bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi2bz.Text.Length > 0)
            {
                gi3load.Visibility = Visibility.Visible;
                gi3lc.Visibility = Visibility.Visible;
                gi3bz.Visibility = Visibility.Visible;
                gi3s.Visibility = Visibility.Visible;
                gi3e.Visibility = Visibility.Visible;
                sgi3s.Visibility = Visibility.Visible;
                sgi3e.Visibility = Visibility.Visible;
            }
        }
        private void gi3bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi3bz.Text.Length > 0)
            {
                gi4load.Visibility = Visibility.Visible;
                gi4lc.Visibility = Visibility.Visible;
                gi4bz.Visibility = Visibility.Visible;
                gi4s.Visibility = Visibility.Visible;
                gi4e.Visibility = Visibility.Visible;
                sgi4s.Visibility = Visibility.Visible;
                sgi4e.Visibility = Visibility.Visible;
            }
        }
        private void gi4bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi4bz.Text.Length > 0)
            {
                gi5load.Visibility = Visibility.Visible;
                gi5lc.Visibility = Visibility.Visible;
                gi5bz.Visibility = Visibility.Visible;
                gi5s.Visibility = Visibility.Visible;
                gi5e.Visibility = Visibility.Visible;
                sgi5s.Visibility = Visibility.Visible;
                sgi5e.Visibility = Visibility.Visible;
            }
        }
        private void gi5bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi5bz.Text.Length > 0)
            {
                gi6load.Visibility = Visibility.Visible;
                gi6lc.Visibility = Visibility.Visible;
                gi6bz.Visibility = Visibility.Visible;
                gi6s.Visibility = Visibility.Visible;
                gi6e.Visibility = Visibility.Visible;
                sgi6s.Visibility = Visibility.Visible;
                sgi6e.Visibility = Visibility.Visible;
            }
        }
        private void gi6bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi6bz.Text.Length > 0)
            {
                gi7load.Visibility = Visibility.Visible;
                gi7lc.Visibility = Visibility.Visible;
                gi7bz.Visibility = Visibility.Visible;
                gi7s.Visibility = Visibility.Visible;
                gi7e.Visibility = Visibility.Visible;
                sgi7s.Visibility = Visibility.Visible;
                sgi7e.Visibility = Visibility.Visible;
            }
        }
        private void gi7bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi7bz.Text.Length > 0)
            {
                gi8load.Visibility = Visibility.Visible;
                gi8lc.Visibility = Visibility.Visible;
                gi8bz.Visibility = Visibility.Visible;
                gi8s.Visibility = Visibility.Visible;
                gi8e.Visibility = Visibility.Visible;
                sgi8s.Visibility = Visibility.Visible;
                sgi8e.Visibility = Visibility.Visible;
            }
        }
        private void gi8bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi8bz.Text.Length > 0)
            {
                gi9load.Visibility = Visibility.Visible;
                gi9lc.Visibility = Visibility.Visible;
                gi9bz.Visibility = Visibility.Visible;
                gi9s.Visibility = Visibility.Visible;
                gi9e.Visibility = Visibility.Visible;
                sgi9s.Visibility = Visibility.Visible;
                sgi9e.Visibility = Visibility.Visible;
            }
        }
        private void gi9bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi9bz.Text.Length > 0)
            {
                gi10load.Visibility = Visibility.Visible;
                gi10lc.Visibility = Visibility.Visible;
                gi10bz.Visibility = Visibility.Visible;
                gi10s.Visibility = Visibility.Visible;
                gi10e.Visibility = Visibility.Visible;
                sgi10s.Visibility = Visibility.Visible;
                sgi10e.Visibility = Visibility.Visible;
            }
        }
        private void gi10bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi10bz.Text.Length > 0)
            {
                gi11load.Visibility = Visibility.Visible;
                gi11lc.Visibility = Visibility.Visible;
                gi11bz.Visibility = Visibility.Visible;
                gi11s.Visibility = Visibility.Visible;
                gi11e.Visibility = Visibility.Visible;
                sgi11s.Visibility = Visibility.Visible;
                sgi11e.Visibility = Visibility.Visible;
            }
        }
        private void gi11bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi11bz.Text.Length > 0)
            {
                gi12load.Visibility = Visibility.Visible;
                gi12lc.Visibility = Visibility.Visible;
                gi12bz.Visibility = Visibility.Visible;
                gi12s.Visibility = Visibility.Visible;
                gi12e.Visibility = Visibility.Visible;
                sgi12s.Visibility = Visibility.Visible;
                sgi12e.Visibility = Visibility.Visible;
            }
        }
        private void gi12bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi12bz.Text.Length > 0)
            {
                gi13load.Visibility = Visibility.Visible;
                gi13lc.Visibility = Visibility.Visible;
                gi13bz.Visibility = Visibility.Visible;
                gi13s.Visibility = Visibility.Visible;
                gi13e.Visibility = Visibility.Visible;
                sgi13s.Visibility = Visibility.Visible;
                sgi13e.Visibility = Visibility.Visible;
            }
        }
        private void gi13bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi13bz.Text.Length > 0)
            {
                gi14load.Visibility = Visibility.Visible;
                gi14lc.Visibility = Visibility.Visible;
                gi14bz.Visibility = Visibility.Visible;
                gi14s.Visibility = Visibility.Visible;
                gi14e.Visibility = Visibility.Visible;
                sgi14s.Visibility = Visibility.Visible;
                sgi14e.Visibility = Visibility.Visible;
            }
        }
        private void gi14bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi14bz.Text.Length > 0)
            {
                gi15load.Visibility = Visibility.Visible;
                gi15lc.Visibility = Visibility.Visible;
                gi15bz.Visibility = Visibility.Visible;
                gi15s.Visibility = Visibility.Visible;
                gi15e.Visibility = Visibility.Visible;
                sgi15s.Visibility = Visibility.Visible;
                sgi15e.Visibility = Visibility.Visible;
            }
        }
        private void gi15bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi15bz.Text.Length > 0)
            {
                gi16load.Visibility = Visibility.Visible;
                gi16lc.Visibility = Visibility.Visible;
                gi16bz.Visibility = Visibility.Visible;
                gi16s.Visibility = Visibility.Visible;
                gi16e.Visibility = Visibility.Visible;
                sgi16s.Visibility = Visibility.Visible;
                sgi16e.Visibility = Visibility.Visible;
            }
        }
        private void gi16bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi16bz.Text.Length > 0)
            {
                gi17load.Visibility = Visibility.Visible;
                gi17lc.Visibility = Visibility.Visible;
                gi17bz.Visibility = Visibility.Visible;
                gi17s.Visibility = Visibility.Visible;
                gi17e.Visibility = Visibility.Visible;
                sgi17s.Visibility = Visibility.Visible;
                sgi17e.Visibility = Visibility.Visible;
            }
        }
        private void gi17bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi17bz.Text.Length > 0)
            {
                gi18load.Visibility = Visibility.Visible;
                gi18lc.Visibility = Visibility.Visible;
                gi18bz.Visibility = Visibility.Visible;
                gi18s.Visibility = Visibility.Visible;
                gi18e.Visibility = Visibility.Visible;
                sgi18s.Visibility = Visibility.Visible;
                sgi18e.Visibility = Visibility.Visible;
            }
        }
        private void gi18bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi18bz.Text.Length > 0)
            {
                gi19load.Visibility = Visibility.Visible;
                gi19lc.Visibility = Visibility.Visible;
                gi19bz.Visibility = Visibility.Visible;
                gi19s.Visibility = Visibility.Visible;
                gi19e.Visibility = Visibility.Visible;
                sgi19s.Visibility = Visibility.Visible;
                sgi19e.Visibility = Visibility.Visible;
            }
        }
        private void gi19bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi19bz.Text.Length > 0)
            {
                gi20load.Visibility = Visibility.Visible;
                gi20lc.Visibility = Visibility.Visible;
                gi20bz.Visibility = Visibility.Visible;
                gi20s.Visibility = Visibility.Visible;
                gi20e.Visibility = Visibility.Visible;
                sgi20s.Visibility = Visibility.Visible;
                sgi20e.Visibility = Visibility.Visible;
            }
        }
        private void gi20bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi20bz.Text.Length > 0)
            {
                gi21load.Visibility = Visibility.Visible;
                gi21lc.Visibility = Visibility.Visible;
                gi21bz.Visibility = Visibility.Visible;
                gi21s.Visibility = Visibility.Visible;
                gi21e.Visibility = Visibility.Visible;
                sgi21s.Visibility = Visibility.Visible;
                sgi21e.Visibility = Visibility.Visible;
            }
        }
        private void gi21bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi21bz.Text.Length > 0)
            {
                gi22load.Visibility = Visibility.Visible;
                gi22lc.Visibility = Visibility.Visible;
                gi22bz.Visibility = Visibility.Visible;
                gi22s.Visibility = Visibility.Visible;
                gi22e.Visibility = Visibility.Visible;
                sgi22s.Visibility = Visibility.Visible;
                sgi22e.Visibility = Visibility.Visible;
            }
        }
        private void gi22bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi22bz.Text.Length > 0)
            {
                gi23load.Visibility = Visibility.Visible;
                gi23lc.Visibility = Visibility.Visible;
                gi23bz.Visibility = Visibility.Visible;
                gi23s.Visibility = Visibility.Visible;
                gi23e.Visibility = Visibility.Visible;
                sgi23s.Visibility = Visibility.Visible;
                sgi23e.Visibility = Visibility.Visible;
            }
        }
        private void gi23bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi23bz.Text.Length > 0)
            {
                gi24load.Visibility = Visibility.Visible;
                gi24lc.Visibility = Visibility.Visible;
                gi24bz.Visibility = Visibility.Visible;
                gi24s.Visibility = Visibility.Visible;
                gi24e.Visibility = Visibility.Visible;
                sgi24s.Visibility = Visibility.Visible;
                sgi24e.Visibility = Visibility.Visible;
            }
        }
        private void gi24bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi24bz.Text.Length > 0)
            {
                gi25load.Visibility = Visibility.Visible;
                gi25lc.Visibility = Visibility.Visible;
                gi25bz.Visibility = Visibility.Visible;
                gi25s.Visibility = Visibility.Visible;
                gi25e.Visibility = Visibility.Visible;
                sgi25s.Visibility = Visibility.Visible;
                sgi25e.Visibility = Visibility.Visible;
            }
        }
        private void gi25bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi25bz.Text.Length > 0)
            {
                gi26load.Visibility = Visibility.Visible;
                gi26lc.Visibility = Visibility.Visible;
                gi26bz.Visibility = Visibility.Visible;
                gi26s.Visibility = Visibility.Visible;
                gi26e.Visibility = Visibility.Visible;
                sgi26s.Visibility = Visibility.Visible;
                sgi26e.Visibility = Visibility.Visible;
            }
        }
        private void gi26bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi26bz.Text.Length > 0)
            {
                gi27load.Visibility = Visibility.Visible;
                gi27lc.Visibility = Visibility.Visible;
                gi27bz.Visibility = Visibility.Visible;
                gi27s.Visibility = Visibility.Visible;
                gi27e.Visibility = Visibility.Visible;
                sgi27s.Visibility = Visibility.Visible;
                sgi27e.Visibility = Visibility.Visible;
            }
        }
        private void gi27bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi27bz.Text.Length > 0)
            {
                gi28load.Visibility = Visibility.Visible;
                gi28lc.Visibility = Visibility.Visible;
                gi28bz.Visibility = Visibility.Visible;
                gi28s.Visibility = Visibility.Visible;
                gi28e.Visibility = Visibility.Visible;
                sgi28s.Visibility = Visibility.Visible;
                sgi28e.Visibility = Visibility.Visible;
            }
        }
        private void gi28bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi28bz.Text.Length > 0)
            {
                gi29load.Visibility = Visibility.Visible;
                gi29lc.Visibility = Visibility.Visible;
                gi29bz.Visibility = Visibility.Visible;
                gi29s.Visibility = Visibility.Visible;
                gi29e.Visibility = Visibility.Visible;
                sgi29s.Visibility = Visibility.Visible;
                sgi29e.Visibility = Visibility.Visible;
            }
        }
        private void gi29bz_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gi29bz.Text.Length > 0)
            {
                gi30load.Visibility = Visibility.Visible;
                gi30lc.Visibility = Visibility.Visible;
                gi30bz.Visibility = Visibility.Visible;
                gi30s.Visibility = Visibility.Visible;
                gi30e.Visibility = Visibility.Visible;
                sgi30s.Visibility = Visibility.Visible;
                sgi30e.Visibility = Visibility.Visible;
            }
        }

         async  Task OK() {
            Thread.Sleep(2000);
            ok.Visibility = Visibility.Visible;
            Thread.Sleep(2000);
            ok.Visibility = Visibility.Hidden;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (themecbx.SelectedIndex == 0)
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Red");
            }
            if (themecbx.SelectedIndex == 1)
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Green");
            }
            if (themecbx.SelectedIndex == 2)
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Blue");
            }
            if (themecbx.SelectedIndex == 3)
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Purple");
            }
            if (themecbx.SelectedIndex == 4)
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Orange");
            }
            if (themecbx.SelectedIndex == 5)
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Lime");
            }
            if (themecbx.SelectedIndex == 6)
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Emerald");
            }
            if (themecbx.SelectedIndex == 7)
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Teal");
            }
            if (themecbx.SelectedIndex == 8)
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Cyan");
            }
            if (themecbx.SelectedIndex == 9)
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Cobalt");
            }
            if (themecbx.SelectedIndex == 10)
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Indigo");
            }
            if (themecbx.SelectedIndex == 11)
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Violet");
            }
            if (themecbx.SelectedIndex == 12)
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Pink");
            }
            if (themecbx.SelectedIndex == 13)
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Magenta");
            }
            if (themecbx.SelectedIndex == 14)
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Crimson");
            }
            if (themecbx.SelectedIndex == 15)
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Amber");
            }
            if (themecbx.SelectedIndex == 16)
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Yellow");
            }
            if (themecbx.SelectedIndex == 17)
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Brown");
            }
            if (themecbx.SelectedIndex == 18)
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Olive");
            }
            if (themecbx.SelectedIndex == 19)
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Steel");
            }
            if (themecbx.SelectedIndex == 20)
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Mauve");
            }
            if (themecbx.SelectedIndex == 21)
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Taupe");
            }
            if (themecbx.SelectedIndex == 22)
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Sienna");
            }
            if (themecbx.SelectedIndex == 23)
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Red");
            }
            if (themecbx.SelectedIndex == 24)
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Green");
            }
            if (themecbx.SelectedIndex == 25)
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Blue");
            }
            if (themecbx.SelectedIndex == 26)
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Purple");
            }
            if (themecbx.SelectedIndex == 27)
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Orange");
            }
            if (themecbx.SelectedIndex == 28)
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Lime");
            }
            if (themecbx.SelectedIndex == 29)
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Emerald");
            }
            if (themecbx.SelectedIndex == 30)
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Teal");
            }
            if (themecbx.SelectedIndex == 31)
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Cyan");
            }
            if (themecbx.SelectedIndex == 32)
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Cobalt");
            }
            if (themecbx.SelectedIndex == 33)
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Indigo");
            }
            if (themecbx.SelectedIndex == 34)
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Violet");
            }
            if (themecbx.SelectedIndex == 35)
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Pink");
            }
            if (themecbx.SelectedIndex == 36)
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Magenta");
            }
            if (themecbx.SelectedIndex == 37)
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Crimson");
            }
            if (themecbx.SelectedIndex == 38)
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Amber");
            }
            if (themecbx.SelectedIndex == 39)
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Yellow");
            }
            if (themecbx.SelectedIndex == 40)
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Brown");
            }
            if (themecbx.SelectedIndex == 41)
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Olive");
            }
            if (themecbx.SelectedIndex == 42)
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Steel");
            }
            if (themecbx.SelectedIndex == 43)
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Mauve");
            }
            if (themecbx.SelectedIndex == 44)
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Taupe");
            }
            if (themecbx.SelectedIndex == 45)
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Sienna");
            }
        }
    }
}
