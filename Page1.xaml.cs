using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Hospital_System_2._0.Properties;

namespace Hospital_System_2._0
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }
        string patientID = "";
        bool thisIDIsSaved = false;
        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Settings.Default.NameSetting))
            {
                userName.Text = Settings.Default.NameSetting;
            }
            if (!string.IsNullOrWhiteSpace(Settings.Default.IDSetting))
            {
                patientID = Settings.Default.IDSetting;
                info1.Text = "This User's ID is " + Settings.Default.IDSetting;
                info2.Text = Settings.Default.IDSetting;
            }
            thisIDIsSaved = Settings.Default.IDIsSaved;
        }
        private void Text_Changed(object sender, RoutedEventArgs e)
        {
            Settings.Default.NameSetting = userName.Text;
            Settings.Default.Save();
        }
        public void CopyToClipBoard(object sender, RoutedEventArgs e)
        {
            if (info2.Text != "")
            {
                System.Windows.Clipboard.SetText(patientID);
                MessageBoxEx1.Show(new MainWindow().Wind(), "Patient ID copied to clipboard");
                return;
            }
            MessageBoxEx1.Show(new MainWindow().Wind(), "Nothing to copy!");
        }
        public void SaveData(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(userName.Text))
            {
                MessageBoxEx1.Show(new MainWindow().Wind(), "Please fill in the Costumers Name");
                return;
            }
            if (patientID == "" || thisIDIsSaved )
            {
                MessageBoxEx1.Show(new MainWindow().Wind(), "This Patient is already saved, please generate a new Patient ID");
                return;
            }
            string URI = "http://jubeeapps.000webhostapp.com/workNew/savelist2.php";
            string Parameters = "name=" + Uri.EscapeDataString(userName.Text) + "&numID=" + patientID;
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string HtmlResult = wc.UploadString(URI, Parameters);
                }
                catch (WebException)
                {
                    MessageBoxEx1.Show(new MainWindow().Wind(), "CONNECTION ERROR: Check your internet connection");
                    return;
                }
            }
            info1.Text = "This User's ID is " + patientID;
            patientID = "";
            thisIDIsSaved = true;
            Settings.Default.IDIsSaved = true;
            Settings.Default.Save();
            MessageBoxEx1.Show(new MainWindow().Wind(), "Patient details has been saved!");
        }
        private void GenerateID(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(userName.Text))
            {
                MessageBoxEx1.Show(new MainWindow().Wind(), "Please fill in the Costumers Name");
                return;
            }
            if (patientID != "" && !thisIDIsSaved )
            {
                MessageBoxEx1.Show(new MainWindow().Wind(), "Please save this Patient ID");
                return;
            }
            string resultNum = "";
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            Random rnd = new Random();
            for (int i = 0; i < 9; i++)
            {
                resultNum = resultNum + characters[rnd.Next(62)];
            }
            info1.Text = "Generated ID is " + resultNum;
            patientID = resultNum;
            info2.Text = resultNum;
            thisIDIsSaved = false;
            Settings.Default.IDIsSaved = false;
            Settings.Default.IDSetting = resultNum;
            Settings.Default.Save();
            MessageBoxEx1.Show(new MainWindow().Wind(), "Patient ID has been generated");
        }
    }
}
