using ControlzEx.Theming;
using MahApps.Metro.Controls;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Metanet_CSV_Builder
{
    /// <summary>
    /// Interaktionslogik für Start.xaml
    /// </summary>
    public partial class Settings : MetroWindow
    {
        private readonly string ConfigFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/Hussmann/MetanetCSV/settings.xml";
        public Settings()
        {
            InitializeComponent();
            string line1 = File.ReadLines(ConfigFolder).Skip(0).Take(1).First();
            string line2 = File.ReadLines(ConfigFolder).Skip(1).Take(1).First();
            ThemeCBX1.SelectedIndex = Convert.ToInt32(line1);
            ThemeCBX2.SelectedIndex = Convert.ToInt32(line2);
            ThemeManager.Current.ChangeTheme(this, ThemeCBX1.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", "") + "." + ThemeCBX2.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", ""));


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Start win2 = new Start();
            win2.Show();
            Visibility = Visibility.Collapsed;
        }

     
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            File.Delete(ConfigFolder);
            File.AppendAllText(ConfigFolder, ThemeCBX1.SelectedIndex.ToString() + "\n");
            File.AppendAllText(ConfigFolder, ThemeCBX2.SelectedIndex.ToString() + "\n");
            string Theme = ThemeCBX1.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", "") + "." + ThemeCBX2.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", "");
            File.AppendAllText(ConfigFolder, Theme);

            
            ThemeManager.Current.ChangeTheme(this, File.ReadLines(ConfigFolder).Skip(2).Take(1).First());
            
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (File.Exists(path + "/#### Backbone Lange Bezeichner.csv"))
            {
                File.Delete(path + "/#### Backbone Lange Bezeichner.csv");
            }
            if (File.Exists(path + "/#### Subnet A Lange Bezeichner.csv"))
            {
                File.Delete(path + "/#### Subnet A Lange Bezeichner.csv");
            }
            if (File.Exists(path + "/#### Subnet B Lange Bezeichner.csv"))
            {
                File.Delete(path + "/#### Subnet B Lange Bezeichner.csv");
            }
            if (File.Exists(path + "/#### Subnet C Lange Bezeichner.csv"))
            {
                File.Delete(path + "/#### Subnet C Lange Bezeichner.csv");
            }
            if (File.Exists(path + "/#### SEI 2 Projektdatei.sei2projr.csv"))
            {
                File.Copy(path + "/#### SEI 2 Projektdatei.sei2proj", ConfigFolder + "DB/Backbone/mainconfig.xml", true);
                File.Delete(path + "/#### SEI 2 Projektdatei.sei2proj");
            }


            System.Windows.Application.Current.Shutdown();
            base.OnClosing(e);
        }
    }
}
