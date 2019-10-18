using System.Windows;
using System.IO.Ports;
using System;

namespace FMG_Configurator__csharp_
{
    /// <summary>
    /// Interaction logic for SerialSettings.xaml
    /// </summary>
    public partial class SerialSettings : Window
    {
        public static MainWindow mainWindow = new MainWindow();
        public static NetSettings netSettings = new NetSettings();
        

        public SerialSettings() => InitializeComponent();

        // ================= BEGIN Window Event METHODS ========================================= //
        private void SerialMenu_Loaded(object sender, RoutedEventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (var port in ports)
                ComPortComboBox.Items.Add(port);
        }

        private void SerialMenu_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Globals.first = false;
            e.Cancel = true;
            Visibility = Visibility.Collapsed;
        }
        // ================= END Window Event METHODS =========================================== //

        // ================= BEGIN Button Click Event METHODS =================================== //
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //check for comport and baudrate values
            if (ComPortComboBox.Text == "" | BaudRateComboBox.Text == "")
            {
                MessageBox.Show("Please fill all values!");
                return;
            }

            Globals.comport = ComPortComboBox.Text;
            Globals.baudrate = BaudRateComboBox.Text;
            Globals.uparity = ParityComboBox.Text;
            Globals.bit = BitComboBox.Text;
            Globals.ustopbit = StopBitComboBox.Text;

            MessageBox.Show("Settings applied!");
            mainWindow.Show();
            Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (Globals.first)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure?", "Alert", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

                if (result == MessageBoxResult.Yes)
                    Environment.Exit(0);
            }
            else
            {
                mainWindow.Show();
                Close();
            }
            
        }
        // ================= END Button Click Event METHODS ===================================== //


        // ================= BEGIN CHECK Event METHODS ========================================== //
        private void ParityNone(object sender, RoutedEventArgs e)
        {
            BitComboBox.Items.Clear();
            BitComboBox.Items.Add("8");
        }

        private void ParityOther(object sender, RoutedEventArgs e)
        {
            BitComboBox.Items.Clear();
            BitComboBox.Items.Add("7");
            BitComboBox.Items.Add("8");
        }
        // ================= END CHECK Event METHODS ============================================ //

    }
}
