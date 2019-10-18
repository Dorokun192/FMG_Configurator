using System;
using System.Windows;
using System.IO.Ports;
using System.Windows.Threading;
namespace FMG_Configurator__csharp_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static SerialSettings serialSettings = new SerialSettings();
        public static ModbusSettings modbusSettings = new ModbusSettings();
        public static NetSettings netSettings = new NetSettings();

        public static SerialPort serportMain = new SerialPort();
        public static string indata;

        // Prep stuff needed to remove close button on window
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public MainWindow() => InitializeComponent();

        // ================= BEGIN Window Event METHODS ========================================= //
        private void MainWin_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);

            if (Globals.firstTime)
            {
                //first time loaded
                serportMain.PortName = Globals.comport;
                serportMain.BaudRate = int.Parse(Globals.baudrate);

                if (Globals.uparity == "None")
                    serportMain.Parity = Parity.None;
                else if (Globals.uparity == "Even")
                    serportMain.Parity = Parity.Even;
                else
                    serportMain.Parity = Parity.Odd;

                serportMain.DataBits = int.Parse(Globals.bit);

                if (Globals.ustopbit == "1")
                    serportMain.StopBits = StopBits.One;
                else
                    serportMain.StopBits = StopBits.Two;

                serportMain.Open();

                serportMain.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                return;
            }
            
            DevIDTextBlock.Text = Globals.devID;
            NetworkSettingsButton.IsEnabled = true;
            ModbusSettingsButton.IsEnabled = true;
            Instruction.Visibility = Visibility.Collapsed;

        }

        private void MainWin_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Globals.firstTime = false;
            this.Visibility = Visibility.Collapsed;
        }
        // ================= END Window Event METHODS =========================================== //

        // ================= BEGIN Button Click Event METHODS =================================== //
        private void NetworkSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            netSettings.Show();
            Close();
        }

        private void SerialMenuItem_Click(object sender, RoutedEventArgs e)
        {
            serialSettings.Show();
            Close();
        }

        private void ModbusSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            modbusSettings.Show();
            Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "EXIT", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                Environment.Exit(0);
            else
                return;
        }

        // ================= END Button Click Event METHODS ===================================== //

        // ================= BEGIN Serial Event METHODS ========================================= //

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            indata = sp.ReadLine();

            // a thread application???
            Application.Current.Dispatcher.BeginInvoke(
                 DispatcherPriority.Background,
                 new Action(() => {               
                    Globals.devID = indata.Split('*')[1];
                    serportMain.Close();
                    DevIDTextBlock.Text = Globals.devID;
                    NetworkSettingsButton.IsEnabled = true;
                    ModbusSettingsButton.IsEnabled = true;
                    Instruction.Visibility = Visibility.Collapsed;
                    MessageBox.Show("Device ID received");
                 }));
        }

        // ================= END Serial Event METHODS =========================================== //

    }
}
