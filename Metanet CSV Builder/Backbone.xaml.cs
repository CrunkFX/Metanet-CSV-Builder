using ControlzEx.Theming;
using MahApps.Metro.Controls;
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
    public partial class Backbone : MetroWindow
    {
        private readonly string CSVPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/Hussmann/MetanetCSV/DB/Backbone/CSV/";
        private readonly string CSVTemp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/Hussmann/MetanetCSV/Temp/Backbone/CSV/";
        private readonly string ConfigFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/Hussmann/MetanetCSV/Temp/Backbone/";
        private readonly string SysDB = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/Hussmann/MetanetCSV/DB/Backbone/DB.mcb";
        private int GICount = 1;


        public Backbone()
        {
            InitializeComponent();
            string Theme = File.ReadLines(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/Hussmann/MetanetCSV/settings.xml").Skip(2).Take(1).First();
            ThemeManager.Current.ChangeTheme(this, Theme);
            App.Current.Properties["DevMGR"] = "Backbone";

            newline();
            getDevNames();


            // Für jedes Gruppenintervall eine Zeile anlegen
            for (int i = 1; i < Convert.ToInt32(File.ReadLines(SysDB).Skip(0).Take(1).First()); i++)
            {
                GICount = GICount + 1;
                newline();
                getDevNames();
            }
            /************************************/



            // Gespeicherte Konfiguration laden
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
            /************************************/

        }



        // Zentralen aus dem DeviceManager laden
        public void getDevNames()
        {
            foreach (string file in System.IO.Directory.GetFiles(CSVPath))
            {
                (FindName($"cbx{GICount}") as ComboBox).Items.Add(System.IO.Path.GetFileNameWithoutExtension(file));
            }
        }
        /************************************/



        // Zentralen aus dem DeviceManager aktualisieren
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
        /************************************/



        // Neue Zeile mit Gruppenintervallen hinzufügen
        private void AddToGI_click(object sender, RoutedEventArgs e)
        {
            if (GICount==0)
            {
                GICount = GICount + 1;
                newline();
                foreach (string file in System.IO.Directory.GetFiles(CSVPath))
                {
                    (FindName($"cbx{GICount}") as ComboBox).Items.Add(System.IO.Path.GetFileNameWithoutExtension(file));
                }
            }
            else
            {

                if ((FindName($"gi{GICount}bz") as TextBox).Text.Length >= 1)
                {
                    GICount = GICount + 1;
                    newline();
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
            
        }
        /************************************/


        // Anzeige bei leerem Bezeichner
        private async Task UTS()
        {
            for (int i = 0; i < 5; i++)
            {
                (FindName($"gi{GICount}bz") as TextBox).Background = Brushes.OrangeRed;
                await Task.Delay(100);
                (FindName($"gi{GICount}bz") as TextBox).Background = Brushes.White;
                await Task.Delay(50);
            }

        }
        /************************************/



        // Benutzerelemente zu neuer Zeile hinzufügen
        private void newline()
        {
            // Zähler hinzufügen
            SPCount.Children.Add(new Label() { Content = GICount.ToString(), Margin = new Thickness(0, 0, 0, 10), FontWeight = FontWeights.SemiBold, Height = 27 });
            /************************************/



            // Zentralenauswahl hinzufügen
            ComboBox cbx = new ComboBox
            {

                Name = "Zentrale" + GICount.ToString(),
                Margin = new Thickness(0, 0, 0, 10),
                FontWeight = FontWeights.SemiBold,
                Height = 27,

            };

            cbx.SelectionChanged += LastChange;
            RegisterCBX($"cbx{GICount}", cbx);
            SPCSV.Children.Add(cbx);
            /************************************/



            // Letzte Änderung auswerten und Wert hinzufügen 
            string datex;
            if (File.Exists(CSVPath + $"Zentrale{GICount}.csv"))
            {
                datex = File.GetLastWriteTime(CSVPath + $"Zentrale{GICount}.csv").ToString();
            }
            else
            {
                datex = "Keine CSV Datei!!";
            }
            Label lastchange = new Label
            {
                Name = $"gi{GICount}lc",
                Content = datex,
                Margin = new Thickness(0, 0, 0, 10),
                FontWeight = FontWeights.SemiBold,
                Height = 27,
                VerticalAlignment = VerticalAlignment.Center

            };


            RegisterLabel($"gi{GICount}lc", lastchange);
            SPLastChange.Children.Add(lastchange);
            /************************************/



            // Bezeichnerfeld hinzufügen
            TextBox bezeichner = new TextBox
            {
                Name = $"gi{GICount}bz",
                Margin = new Thickness(0, 0, 0, 10),
                FontWeight = FontWeights.SemiBold,
                Height = 27,
                MinWidth = 200
            };
            RegisterNewTextBox($"gi{GICount}bz", bezeichner);
            SPBezeichner.Children.Add(bezeichner);
            /************************************/



            //Meldegruppenstart hinzufügen
            NumericUpDown numgistart = new NumericUpDown
            {
                Name = $"gi{GICount}s",
                Margin = new Thickness(0, 0, 0, 10),
                FontWeight = FontWeights.SemiBold,
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
            /************************************/



            // Meldegruppenende hinzufügen
            NumericUpDown numgiend = new NumericUpDown
            {
                Name = $"gi{GICount}e",
                Margin = new Thickness(0, 0, 0, 10),
                FontWeight = FontWeights.SemiBold,
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
            /************************************/



            // Steuergruppenstart hinzufügen
            NumericUpDown numsgistart = new NumericUpDown
            {
                Name = $"sgi{GICount}s",
                Margin = new Thickness(0, 0, 0, 10),
                FontWeight = FontWeights.SemiBold,
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
            /************************************/



            // Steuergruppenende hinzufügen
            NumericUpDown numsgiend = new NumericUpDown
            {
                Name = $"sgi{GICount}e",
                Margin = new Thickness(0, 0, 0, 10),
                FontWeight = FontWeights.SemiBold,
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
            /************************************/
        }



        // Benutzersteuerelemente zur weiteren Verwendung registrieren

        private void RegisterLabel(string LabelName, Label Label)
        {
            if ((Label)FindName(LabelName) != null)
            {
                UnregisterName(LabelName);
            }
            RegisterName(LabelName, Label);
        }

        private void RegisterCBX(string CBXName, ComboBox CBX)
        {
            if ((ComboBox)FindName(CBXName) != null)
            {
                UnregisterName(CBXName);
            }
            RegisterName(CBXName, CBX);
        }

        private void RegisterNumBlock(string NumBlockName, NumericUpDown NumBlock)
        {
            if ((NumericUpDown)FindName(NumBlockName) != null)
            {
                UnregisterName(NumBlockName);
            }
            RegisterName(NumBlockName, NumBlock);
        }

        private void RegisterNewTextBox(string TextBoxName, TextBox TextBox)
        {
            if ((TextBox)FindName(TextBoxName) != null)
            {
                UnregisterName(TextBoxName);
            }
            RegisterName(TextBoxName, TextBox);
        }
        /************************************/


        // Zeile mit Gruppenintervallen entfernen
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
        /************************************/



        // Letzte Änderung auswerten
        private void LastChange(object sender, SelectionChangedEventArgs e)
        {
            for (int i = 1; i < GICount + 1; i++)
            {
                (FindName("gi" + i + "lc") as Label).Content = File.GetLastWriteTime(CSVPath + (FindName("cbx" + i) as ComboBox).SelectedItem + ".csv").ToString();
            }
        }
        /************************************/


        // Gespeicherte Konfiguration bestätigen
        private async Task Saved()
        {
            ok.Visibility = Visibility.Visible;
            await Task.Delay(2000);
            ok.Visibility = Visibility.Hidden;
        }
        /************************************/


        // Konfiguration speichern
        private void SaveConfig()
        {
            File.Delete(SysDB);
            File.AppendAllText(SysDB, GICount.ToString());
            //Reserve
            File.AppendAllText(SysDB, "\n");
            File.AppendAllText(SysDB, "\n");
            File.AppendAllText(SysDB, "\n");
            File.AppendAllText(SysDB, "\n");
            //-------
            try
            {
                for (int i = 0; i < GICount; i++)
                {
                    File.AppendAllText(SysDB, (FindName($"gi{i + 1}bz") as TextBox).Text + "," + (FindName($"gi{i + 1}s") as NumericUpDown).Value.ToString() + "," + (FindName($"gi{i + 1}e") as NumericUpDown).Value.ToString() + "," + (FindName($"sgi{i + 1}s") as NumericUpDown).Value.ToString() + "," + (FindName($"sgi{i + 1}e") as NumericUpDown).Value.ToString() + "," + (FindName($"cbx{i + 1}") as ComboBox).SelectedItem.ToString() + "\n");
                }
                _ = Saved();
            }
            catch
            {
                MessageBox.Show("Bitte Konfiguration überprüfen!", "Speichern nicht möglich");

            }
        }
        /************************************/


        // Konfiguration speichern
        private void savedb_Click(object sender, RoutedEventArgs e)
        {
            SaveConfig();
        }
        /************************************/


        // CSV Header und Gruppenintervall 1 erstellen
        private readonly string CSVP1Temp = System.IO.Path.GetTempFileName();
        public void CreateCSVP1(string Bezeichner, int RepeatGA, string GAStart, string SGAStart, int RepeatSGA)
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
                System.IO.File.AppendAllText(CSVP1Temp, "Meldergruppe " + c + ";;G/" + c + ";" + Bezeichner + "." + c + "\r\n");
            }

            for (int i = 0; i < RepeatSGA; i++)
            {
                int c = i + Convert.ToInt32(SGAStart);
                System.IO.File.AppendAllText(CSVP1Temp, "Steuergruppen " + c + ";;S/" + c + ";" + Bezeichner + "." + c + "\r\n");
            }

        }
        /************************************/

        // Restliche CSV Dateien erstellen
        string CSVP2Temp = System.IO.Path.GetTempFileName();
        public void CreateCSV2(string csvno, string Bezeichner, int RepeatGA, string GAStart, string SGAStart, int RepeatSGA)
        {
            using (StreamReader reader = new StreamReader(CSVPath + (FindName($"cbx{csvno}") as ComboBox).SelectedItem + ".csv"))
            {
                using (StreamWriter writer = new StreamWriter(CSVP2Temp))
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
                System.IO.File.AppendAllText(CSVP2Temp, "Meldergruppe " + c + ";;G/" + c + ";" + Bezeichner + "." + c + "\r\n");
            }
            for (int i = 0; i < RepeatSGA; i++)
            {
                int c = i + Convert.ToInt32(SGAStart);
                System.IO.File.AppendAllText(CSVP2Temp, "Steuergruppe " + c + ";;S/" + c + ";" + Bezeichner + "." + c + "\r\n");
            }
        }
        /************************************/


        // Konfiguration erstellen
        private void createbt_Click(object sender, RoutedEventArgs e)
        {
            SaveConfig();
            try
            {
                List<string> uidoe = new List<string>();
                CreateCSVP1((FindName("gi1bz") as TextBox).Text, (Convert.ToInt32((FindName("gi1e") as NumericUpDown).Value) - Convert.ToInt32((FindName("gi1s") as NumericUpDown).Value) + 1), (FindName("gi1s") as NumericUpDown).Value.ToString(), (FindName("sgi1s") as NumericUpDown).Value.ToString(), (Convert.ToInt32((FindName("sgi1e") as NumericUpDown).Value) - Convert.ToInt32((FindName("sgi1s") as NumericUpDown).Value) + 1));
                File.Copy(CSVP1Temp, CSVTemp + "temp1.oix", true);
                uidoe.Add(CSVTemp + "temp1.oix");
                for (int i = 2; i < GICount + 1; i++)
                {
                    CreateCSV2((i).ToString(), (FindName("gi" + i + "bz") as TextBox).Text, (Convert.ToInt32((FindName("gi" + i + "e") as NumericUpDown).Value) - Convert.ToInt32((FindName("gi" + i + "s") as NumericUpDown).Value) + 1), (FindName("gi" + i + "s") as NumericUpDown).Value.ToString(), (FindName("sgi" + i + "s") as NumericUpDown).Value.ToString(), (Convert.ToInt32((FindName("sgi" + i + "e") as NumericUpDown).Value) - Convert.ToInt32((FindName("sgi" + i + "s") as NumericUpDown).Value) + 1));
                    File.Copy(CSVP2Temp, CSVTemp + "temp" + (i) + ".oix", true);
                    uidoe.Add(CSVTemp + "temp" + i + ".oix");
                }
                const int chunkSize = 2 * 1024; // 2KB
                List<string> inputFiles = uidoe;
                using (FileStream output = File.Create(ConfigFolder + "intervals.xml"))
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
                MessageBox.Show("Bitte Konfiguration überprüfen!","Erstellen nicht möglich");
            }
        }
        /************************************/

        // GeräteManager öffnen
        private void OpenDevMGR(object sender, RoutedEventArgs e)
        {
            Anlagen win2 = new Anlagen();
            win2.Show();
            win2.Owner = this;
        }
        /************************************/


        // Startfenster nach dem Schließen öffnen
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Möchten Sie die Änderungen speichern?",
 "Änderungen Speichern?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                SaveConfig();
                Start win2 = new Start();
                win2.Show();
            }
            else
            {
                Start win2 = new Start();
                win2.Show();
            }
            

        }
        /************************************/
    }
}
