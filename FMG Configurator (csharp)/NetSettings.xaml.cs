using System;
using System.Windows;
using System.IO.Ports;


namespace FMG_Configurator__csharp_
{
    /// <summary>
    /// Interaction logic for NetSettings.xaml
    /// </summary>
    public partial class NetSettings : Window
    {
        public static MainWindow mainWindow = new MainWindow();
        public static SerialPort serportNet = new SerialPort();
        
        // temporary storage for values to be sent to the device
        // for now the storage is up to 100 due to device memory limitation
        public static string[] fmg = new string[101];


        public NetSettings() => InitializeComponent();

        // ================= BEGIN Window Event METHODS ================================================= //
        private void NetSettingsWindow_Loaded(object sender, RoutedEventArgs e) => DeviceIDTextBlock.Text = Globals.devID;

        private void NetSettingsWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;

            if (serportNet.IsOpen)
                serportNet.Close();

            Visibility = Visibility.Collapsed;
            mainWindow.Show();
        }
        // ================= END Window Event METHODS =================================================== //

        // ================= BEGIN CHECK Event METHODS ================================================== //
        private void loadData()
        {
            LanIPTextBox.LoadIPAddress(fmg[1]);
            LanSubnetMaskTextBox.LoadIPAddress(fmg[2]);
            LanGatewayTextBox.LoadIPAddress(fmg[3]);
            LanDNS1TextBox.LoadIPAddress(fmg[4]);
            LanDNS2TextBox.LoadIPAddress(fmg[5]);

            WlanPassTextBox.Text = fmg[6];
            WlanSSIDTextBox.Text = fmg[7];
            WlanIPTextBox.LoadIPAddress(fmg[8]);
            WlanSubnetMaskTextBox.LoadIPAddress(fmg[9]);
            WlanGatewayTextBox.LoadIPAddress(fmg[10]);
            WlanDNS1TextBox.LoadIPAddress(fmg[11]);
            WlanDNS2TextBox.LoadIPAddress(fmg[12]);

            MobileIPTextBox.LoadIPAddress(fmg[13]);
            MobileSubnetMaskTextBox.LoadIPAddress(fmg[14]);
            MobileGatewayTextBox.LoadIPAddress(fmg[15]);
            MobileDNS1TextBox.LoadIPAddress(fmg[16]);
            MobileDNS2TextBox.LoadIPAddress(fmg[17]);
            MobileUsernameTextBox.Text = fmg[19];
            MobilePasswordTextBox.Text = fmg[20];
            MobileAPNTextBox.Text = fmg[21];
            
            MQTTPassTextBox.Text = fmg[23];
            LwtTopicTextBox.Text = fmg[24];
            LwtMessTextBox.Text = fmg[25];
            LwtRetTextBox.Text = fmg[26];
            LwtQOSTextBox.Text = fmg[27];
        }

        private void MobileOptionalCheckBox_Checked(object sender, RoutedEventArgs e) => MobileOptionalGrid.IsEnabled = true;

        private void MobileOptionalCheckBox_Unchecked(object sender, RoutedEventArgs e) => MobileOptionalGrid.IsEnabled = false;

        private void LDHCPDis_Checked(object sender, RoutedEventArgs e) => LDHCPGrid.IsEnabled = true;

        private void LDHCPEn_Checked(object sender, RoutedEventArgs e) => LDHCPGrid.IsEnabled = false;

        private void LDNSDis_Checked(object sender, RoutedEventArgs e) => LDNSGrid.IsEnabled = true;

        private void LAutoDNSOn_Checked(object sender, RoutedEventArgs e) => LDNSGrid.IsEnabled = false;

        private void WDHCPDis_Checked(object sender, RoutedEventArgs e) => WDHCPGrid.IsEnabled = true;

        private void WDNSOff_Checked(object sender, RoutedEventArgs e) => WDNSGrid.IsEnabled = true;

        private void WDHCPEn_Checked(object sender, RoutedEventArgs e) => WDHCPGrid.IsEnabled = false;

        private void WDNSOn_Checked(object sender, RoutedEventArgs e) => WDNSGrid.IsEnabled = false;

        private void MQTTLWTCheckBox_Checked(object sender, RoutedEventArgs e) => MQTTLWTGrid.IsEnabled = true;

        private void MQTTLWTCheckBox_Unchecked(object sender, RoutedEventArgs e) => MQTTLWTGrid.IsEnabled = false;
        // ================= END CHECK Event METHODS =================================================== //

        // ================= BEGIN Serial Event METHODS ========================================= //
        private void SendStrings(int a, int b)
        {
            for (int i = a; i <= b; i++)
                if (fmg[i] != "")
                    serportNet.Write($"\r{i}*{fmg[i]}\n");
        }
        // ================= END Serial Event METHODS =========================================== //

        // ================= BEGIN SettingsApply (with globals) METHODS =============================================== //
        private void LanSettingsApply()
        {
            if (LDHCPGrid.IsEnabled)
            {
                fmg[1] = LanIPTextBox.GetString();
                fmg[2] = LanSubnetMaskTextBox.GetString();
                fmg[3] = LanGatewayTextBox.GetString();

                SendStrings(1, 3);
            }

            if (LDNSGrid.IsEnabled)
            {
                fmg[4] = LanDNS1TextBox.GetString();
                fmg[5] = LanDNS2TextBox.GetString();

                SendStrings(4, 5);
            }
        }

        private void WlanSettingsApply()
        {
            if (WlanSSIDTextBox.Text == "" | WlanPassTextBox.Text == "")
            {
                MessageBox.Show("missing Password or SSID data");
                return;
            }
            else
            {
                fmg[28] = WlanPassTextBox.Text;
                fmg[7] = WlanSSIDTextBox.Text;

                SendStrings(7, 7);
                SendStrings(28, 28);
            }

            if (WDHCPGrid.IsEnabled)
            {
                fmg[8] = WlanIPTextBox.GetString();
                fmg[9] = WlanSubnetMaskTextBox.GetString();
                fmg[10] = WlanGatewayTextBox.GetString();

                SendStrings(8, 10);
            }

            if (WDNSGrid.IsEnabled)
            {
                fmg[11] = WlanDNS1TextBox.GetString();
                fmg[12] = WlanDNS2TextBox.GetString();

                SendStrings(11, 12);
            }
        }

        private void MobileSettingsApply()
        {
            if (MobileOptionalGrid.IsEnabled)
            {
                fmg[13] = MobileIPTextBox.GetString();
                fmg[14] = MobileSubnetMaskTextBox.GetString();
                fmg[15] = MobileGatewayTextBox.GetString();
                fmg[16] = MobileDNS1TextBox.GetString();
                fmg[17] = MobileDNS2TextBox.GetString();
                fmg[18] = MobileAuthenticateComboBox.Text;
                fmg[19] = MobileUsernameTextBox.Text;
                fmg[20] = MobilePasswordTextBox.Text;

                SendStrings(13, 20);
            }
            fmg[21] = MobileAPNTextBox.Text;

            SendStrings(21, 21);
        }

        private void MQTTSettingsApply()
        {
            int stringtointeger;
            

            if (analogdelTextBox.Text == "")
            {
                MessageBox.Show("no value for analog time");
                return;
            }
            stringtointeger = Int32.Parse(analogdelTextBox.Text);

            if (timerCombo.Text == "second")
                stringtointeger *= 1000;
            else if (timerCombo.Text == "minute")
                stringtointeger *= 60000;
            else
                stringtointeger *= 3600000;

            fmg[22] = stringtointeger.ToString();
            fmg[23] = MQTTPassTextBox.Text;

            if (MQTTLWTGrid.IsEnabled)
            {
                fmg[24] = LwtTopicTextBox.Text;
                fmg[25] = LwtMessTextBox.Text;
                fmg[26] = LwtRetTextBox.Text;
                fmg[27] = LwtQOSTextBox.Text;

                SendStrings(24, 27);
            }
            SendStrings(22, 23);
        }

        // ================= END SettingsApply (with globals) METHODS ================================================= //

        // ================= BEGIN Button Click Event METHODS ========================================== //
        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (LanTab.IsSelected)
            {
                LanSettingsApply();
                MessageBox.Show("LAN Settings applied");
            }
            else if (WlanTab.IsSelected)
            {
                WlanSettingsApply();
                MessageBox.Show("WLAN Settings applied");
            }
            else if (MobileTab.IsSelected)
            {
                MobileSettingsApply();
                MessageBox.Show("Mobile Settings applied");
            }
            else if (MQTTTab.IsSelected)
            {
                MQTTSettingsApply();
                MessageBox.Show("MQTT settings applied");
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            LanSettingsApply();
            WlanSettingsApply();
            MobileSettingsApply();
            MQTTSettingsApply();

            MessageBox.Show("All settings applied");
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            Close();
        }
        // ================= END Button Click Event METHODS ============================================ //

        // ================= BEGIN Menu Click Event METHODS ========================================== //
        private void LoadDevData_Click(object sender, RoutedEventArgs e) => loadData();

        private void ResetDevData_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure to reset all data?", "Reset Alert", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                for (int i = 1; i < 101; i++)
                    serportNet.Write($"\r{i}*\n");

                MessageBox.Show("all data has been erased");
            }
            else if (result == MessageBoxResult.No)
                return;
        }

        private void SerialOpen_Click(object sender, RoutedEventArgs e)
        {
            serportNet.PortName = Globals.comport;
            serportNet.BaudRate = int.Parse(Globals.baudrate);

            if (Globals.uparity == "None")
                serportNet.Parity = Parity.None;
            else if (Globals.uparity == "Even")
                serportNet.Parity = Parity.Even;
            else
                serportNet.Parity = Parity.Odd;

            serportNet.DataBits = int.Parse(Globals.bit);

            if (Globals.ustopbit == "1")
                serportNet.StopBits = StopBits.One;
            else
                serportNet.StopBits = StopBits.Two;

            serportNet.Open();

            NetworkTabControl.IsEnabled = true;
            SerialOpen.IsEnabled = false;
            SerialClose.IsEnabled = true;
            ApplyButton.IsEnabled = true;
            OKButton.IsEnabled = true;
            CloseButton.IsEnabled = true;
        }

        private void SerialClose_Click(object sender, RoutedEventArgs e)
        {
            if (serportNet.IsOpen)
            {
                serportNet.Write($"\r{500}*END\n");
                serportNet.Close();
            }

            NetworkTabControl.IsEnabled = false;
            SerialOpen.IsEnabled = true;
            SerialClose.IsEnabled = false;
            ApplyButton.IsEnabled = false;
            OKButton.IsEnabled = false;
            CloseButton.IsEnabled = false;
        }
        // ================= END Menu Click Event METHODS ========================================== //

    }
}
