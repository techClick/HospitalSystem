using Hospital_System_2._0.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;

namespace Hospital_System_2._0
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }
        private void Text_Changed1(object sender, RoutedEventArgs e)
        {
            Settings.Default.IDSetting2 = ID.Text;
            Settings.Default.Save();
        }
        private void Text_Changed2(object sender, RoutedEventArgs e)
        {
            Settings.Default.AmtSetting = Amount.Text;
            Settings.Default.Save();
        }

        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Settings.Default.IDSetting2))
            {
                ID.Text = Settings.Default.IDSetting2;
            }
            if (!string.IsNullOrWhiteSpace(Settings.Default.AmtSetting))
            {
                Amount.Text = Settings.Default.AmtSetting;
            }
        }
        public JArray GetPatientDetails()
        {
            string URI = "http://jubeeapps.000webhostapp.com/workNew/checkID2.php";
            string Parameters = "ID=" + Uri.EscapeDataString(ID.Text);
            string HtmlResult = "";
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                try
                {
                    HtmlResult = wc.UploadString(URI, Parameters);
                }
                catch (WebException)
                {
                    MessageBoxEx1.Show(new MainWindow().Wind(), "CONNECTION ERROR: Check your internet connection");
                    string[] connectionE = new string[1]; connectionE[0] = "NULL";
                    string connectionError = JsonConvert.SerializeObject(connectionE);
                    return JArray.Parse(connectionError);
                }
            }
            return JArray.Parse(HtmlResult);
        }
        public void DisplayPatientDetails(JArray patientDetailsTmp, Boolean checkTotal)
        {
            if (patientDetailsTmp[0].ToString() == "Nothing")
            {
                MessageBoxEx1.Show(new MainWindow().Wind(), "This ID does not exist in our system!");
                return;
            }
            else if (patientDetailsTmp[0].ToString() == "NULL")
            {
                return;
            }
            var patientDetails = patientDetailsTmp[1];
            PatientName.Text = "NAME :  " + patientDetails[0][0];
            PatientID.Text = "ID :  " + patientDetails[0][1];
            Balance.Text = "LAST BALANCE :  ₦" + patientDetails[0][2];
            PatientTotal.Visibility = Visibility.Collapsed;
            AmtNow.Visibility = Visibility.Collapsed;
            if (checkTotal)
            {
                PatientTotal.Visibility = Visibility.Visible;
                AmtNow.Visibility = Visibility.Visible;
                AmtNow.Text = "TODAYS PURCHASE :  Data error";
                PatientTotal.Text = "TOTAL :  Data error";
                if (!string.IsNullOrWhiteSpace(Amount.Text))
                {
                    for (int i = 0; i < Amount.Text.Length; i++)
                    {
                        if (!Char.IsDigit(Amount.Text[i]))
                        {
                            MessageBoxEx1.Show(new MainWindow().Wind(), "Only numbers are required to fill a purchase!");
                            return;
                        }
                    }
                    AmtNow.Text = "TODAYS PURCHASE :  ₦" + Amount.Text;
                    int totalAmount = Convert.ToInt32(Amount.Text) + Convert.ToInt32(patientDetails[0][2]);
                    PatientTotal.Text = "TOTAL :  ₦" + totalAmount.ToString();

                    string URI;
                    string Parameters;
                    string HtmlResult;
                    URI = "http://jubeeapps.000webhostapp.com/workNew/savepurchase.php";
                    Parameters = "ID=" + Uri.EscapeDataString(ID.Text) + "&Amt=" + Amount.Text;
                    using (WebClient wc = new WebClient())
                    {
                        try
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                            HtmlResult = wc.UploadString(URI, Parameters);
                        }
                        catch (WebException)
                        {
                            MessageBoxEx1.Show(new MainWindow().Wind(), "Purchase was not saved. CONNECTION ERROR: " +
                                "Check your internet connection");
                            AmtNow.Text = "TODAYS PURCHASE :  Error";
                            PatientTotal.Text = "TOTAL :  Error";
                            return;
                        }
                    }
                }
                MessageBoxEx1.Show(new MainWindow().Wind(), "Patient purchase successfully updated!");
                return;
            }
            MessageBoxEx1.Show(new MainWindow().Wind(), "Patient purchase statement loaded");
        }
        public void ShowPreviousBalance(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ID.Text))
            {
                MessageBoxEx1.Show(new MainWindow().Wind(), "Please fill in the Patient's ID");
                return;
            }

            DisplayPatientDetails(GetPatientDetails(), false);

        }
        public void AddAmountToDB(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ID.Text))
            {
                MessageBoxEx1.Show(new MainWindow().Wind(), "Please fill in the Patient's ID");
                return;
            }
            if (string.IsNullOrWhiteSpace(Amount.Text))
            {
                MessageBoxEx1.Show(new MainWindow().Wind(), "Please fill in the amount to add");
                return;
            }

            DisplayPatientDetails(GetPatientDetails(), true);
        }

    }
}
