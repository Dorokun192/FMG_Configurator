using System;
using System.Linq;
using System.Windows;
using System.IO.Ports;
using static FMG_Configurator__csharp_.ModbusCard;

namespace FMG_Configurator__csharp_
{
    /// <summary>
    /// Interaction logic for ModbusSettings.xaml
    /// </summary>
    public partial class ModbusSettings : Window
    {
        #region Declarations
        public MainWindow mainWindow = new MainWindow();
        public static string type;
        public SerialPort serportModbus = new SerialPort();
        public static int counter = 1;
        #endregion

        public ModbusSettings()
        {
            InitializeComponent();
            Dev1ModbusCard.SaveButtonClicked += new EventHandler(Dev1Handler);
            Dev2ModbusCard.SaveButtonClicked += new EventHandler(Dev2Handler);
            Dev3ModbusCard.SaveButtonClicked += new EventHandler(Dev3Handler);
            Dev4ModbusCard.SaveButtonClicked += new EventHandler(Dev4Handler);

            Dev2ModbusCard.ComboBoxChanged += new EventHandler(Dev2Combo);
            Dev3ModbusCard.ComboBoxChanged += new EventHandler(Dev3Combo);
            Dev4ModbusCard.ComboBoxChanged += new EventHandler(Dev4Combo);

        }
        #region Window Events
        private void modbusWin_Loaded(object sender, RoutedEventArgs e)
        {
            modbusWin.Title = "Modbus Settings - " + Globals.devID;

            Dev1ModbusCard.DeviceNameTextBlock.Text = "Device01";
            Dev2ModbusCard.DeviceNameTextBlock.Text = "Device02";
            Dev3ModbusCard.DeviceNameTextBlock.Text = "Device03";
            Dev4ModbusCard.DeviceNameTextBlock.Text = "Device04";

            FreeTagsTextBlock.Text = Globals.TagCount.ToString();

        }

        private void modbusWin_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            serportModbus.Close();
            SerialClose.IsEnabled = false;
            SerialOpen.IsEnabled = true;
            mainWindow.Show();
            
            Visibility = Visibility.Collapsed;
        }
       
        #endregion

        #region Button Events
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string ID1 = Dev1ModbusCard.NodeIDTextBox.Text;
            string ID2 = Dev2ModbusCard.NodeIDTextBox.Text;
            string ID3 = Dev3ModbusCard.NodeIDTextBox.Text;
            string ID4 = Dev4ModbusCard.NodeIDTextBox.Text;

            string[] a = { ID1, ID2, ID3, ID4 };

            for (int i = 0; i < 2; i++)
            {
                if (a[i] != "")
                {
                    for (int j = 1; j < 3; j++)
                    {
                        if (a[i + j] != "")
                            if (a[i] == a[i + j])
                            {
                                MessageBox.Show("Similar Node ID found" + a[i] + a[i + j]);
                                return;
                            }
                    }
                }
            }

            //send RTU settings data
            SendData(30, RTUBaudRate.Text);
            SendData(31, RTUBit.Text);
            SendData(32, RTUParity.Text);
            SendData(33, RTUPollTime.Text);
            SendData(34, RTUTimeout.Text);

            if (Dev1ModbusCard.TagSelection.IsEnabled & ID1 != "")
            {
                SendData(35, ID1);

                switch (Dev1ModbusCard.DAQTagTypeComboBox.Text)
                {
                    case "Modbus Coils(0x)":
                        type = "1";
                        break;
                    case "Discrete Inputs(1x)":
                        type = "2";
                        break;
                    case "Input Registers(3x)":
                        type = "3";
                        break;
                    case "Holding Registers(4x)":
                        type = "4";
                        break;
                    default:
                        break;
                }

                SendData(36, type);
                TagSend(36, Dev1ModbusCard);
            }

            if (Dev2ModbusCard.TagSelection.IsEnabled & ID2 != "")
            {
                SendData(54, ID2);
                switch (Dev2ModbusCard.DAQTagTypeComboBox.Text)
                {
                    case "Modbus Coils(0x)":
                        type = "1";
                        break;
                    case "Discrete Inputs(1x)":
                        type = "2";
                        break;
                    case "Input Registers(3x)":
                        type = "3";
                        break;
                    case "Holding Registers(4x)":
                        type = "4";
                        break;
                    default:
                        break;
                }
                SendData(55, type);
                TagSend(55, Dev2ModbusCard);
            }

            if (Dev3ModbusCard.TagSelection.IsEnabled & ID3 != "")
            {
                SendData(73, ID3);

                switch (Dev3ModbusCard.DAQTagTypeComboBox.Text)
                {
                    case "Modbus Coils(0x)":
                        type = "1";
                        break;
                    case "Discrete Inputs(1x)":
                        type = "2";
                        break;
                    case "Input Registers(3x)":
                        type = "3";
                        break;
                    case "Holding Registers(4x)":
                        type = "4";
                        break;
                    default:
                        break;
                }
                SendData(74, type);
                TagSend(74, Dev3ModbusCard);
            }

            if (Dev4ModbusCard.TagSelection.IsEnabled & ID4 != "")
            {
                //SendData(54, ID4);
                //SendData(55, Dev2ModbusCard.DAQTagTypeComboBox.Text);
                //foreach (var x in Dev2ModbusCard.TagSelection.Items.OfType<User>().ToList())
                //{
                //    if (x.IsSelected)
                //    {
                //        TagLeft -= 1;
                //        SendData(55 + x.TagAddress, x.TagAddress.ToString());
                //    }// send id, and name to device?
                //}
                //Dev4ModbusCard.TagSelection.IsReadOnly = true;
            }
            FreeTagsTextBlock.Text = Globals.TagCount.ToString();
            MessageBox.Show("sent to device successful");
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            Dev1ModbusCard.TagSelection.IsEnabled = Dev1ModbusCard.TagSelection.IsReadOnly = false;
            Dev1ModbusCard.TagSelection.ItemsSource = null;
            Dev2ModbusCard.TagSelection.IsEnabled = Dev2ModbusCard.TagSelection.IsReadOnly = false;
            Dev2ModbusCard.TagSelection.ItemsSource = null;
            Dev3ModbusCard.TagSelection.IsEnabled = Dev3ModbusCard.TagSelection.IsReadOnly = false;
            Dev3ModbusCard.TagSelection.ItemsSource = null;
            Dev4ModbusCard.TagSelection.IsEnabled = Dev4ModbusCard.TagSelection.IsReadOnly = false;
            Dev4ModbusCard.TagSelection.ItemsSource = null;
            Dev1ModbusCard.CardSaveButton.IsEnabled = true;
            Dev2ModbusCard.CardSaveButton.IsEnabled = true;
            Dev3ModbusCard.CardSaveButton.IsEnabled = true;
            Dev4ModbusCard.CardSaveButton.IsEnabled = true;
            Globals.TagCount = 64;
            FreeTagsTextBlock.Text = Globals.TagCount.ToString();

            // reset tag addresses
            for (int i = 30; i <= 100; i++)
            {
                SendData(i, "");
            }
            MessageBox.Show("reset all data");
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // reset tag addresses
            for (int i = 30; i <= 100; i++)
            {
                SendData(i, "");
            }

            Dev1ModbusCard.TagSelection.IsReadOnly = false;
            Dev2ModbusCard.TagSelection.IsReadOnly = false;
            Dev3ModbusCard.TagSelection.IsReadOnly = false;
            Dev4ModbusCard.TagSelection.IsReadOnly = false;
            Dev1ModbusCard.CardSaveButton.IsEnabled = true;
            Dev2ModbusCard.CardSaveButton.IsEnabled = true;
            Dev3ModbusCard.CardSaveButton.IsEnabled = true;
            Dev4ModbusCard.CardSaveButton.IsEnabled = true;

            Globals.TagCount = 64;
            FreeTagsTextBlock.Text = Globals.TagCount.ToString();
        }


        private void SerialOpen_Click(object sender, RoutedEventArgs e)
        {
            serportModbus.PortName = Globals.comport;
            serportModbus.BaudRate = int.Parse(Globals.baudrate);
            switch (Globals.uparity)
            {
                case "None":
                    serportModbus.Parity = Parity.None;
                    break;
                case "Odd":
                    serportModbus.Parity = Parity.Odd;
                    break;
                case "Even":
                    serportModbus.Parity = Parity.Even;
                    break;
                default:
                    break;
            }
            serportModbus.DataBits = int.Parse(Globals.bit);
            if (Globals.ustopbit == "1")
                serportModbus.StopBits = StopBits.One;
            else if (Globals.ustopbit == "2")
                serportModbus.StopBits = StopBits.Two;

            serportModbus.Open();

            SerialClose.IsEnabled = true;
            SaveButton.IsEnabled = true;
            EditButton.IsEnabled = true;
            ResetButton.IsEnabled = true;
            SerialOpen.IsEnabled = false;
        }

        private void SerialClose_Click(object sender, RoutedEventArgs e)
        {
            if (serportModbus.IsOpen)
            {
                serportModbus.Write($"\r{500}*END\n");
                serportModbus.Close();

                SerialClose.IsEnabled = false;
                SaveButton.IsEnabled = false;
                EditButton.IsEnabled = false;
                ResetButton.IsEnabled = false;
                SerialOpen.IsEnabled = true;
            }
        }

        private void AddCardButton_Click(object sender, RoutedEventArgs e)
        {
            switch (counter)
            {
                case 1:
                    Dev1ModbusCard.Visibility = Visibility.Visible;
                    counter++;
                    break;
                case 2:
                    if (Dev1ModbusCard.TagSelection.IsReadOnly)
                    {
                        Dev2ModbusCard.Visibility = Visibility.Visible;
                        counter++;
                    }
                    else
                        MessageBox.Show("Save previous Modbus first!", "Save MODBUS Card", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                    
                case 3:
                    if (Dev2ModbusCard.TagSelection.IsReadOnly)
                    {
                        Dev3ModbusCard.Visibility = Visibility.Visible;
                        counter++;
                    }
                    else
                        MessageBox.Show("Save previous Modbus first!", "Save MODBUS Card", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case 4:
                    if (Dev3ModbusCard.TagSelection.IsReadOnly)
                    {
                        Dev4ModbusCard.Visibility = Visibility.Visible;
                        counter++;
                    }
                    else
                        MessageBox.Show("Save previous Modbus first!", "Save MODBUS Card", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                default:
                    MessageBox.Show("Maximum number of devices reached!", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
            
        }
        #endregion

        #region Card Handler Events

        private void Dev1Handler(object sender, EventArgs e) => TagSelect(Dev1ModbusCard);
        private void Dev2Handler(object sender, EventArgs e) => TagSelect(Dev2ModbusCard);
        private void Dev3Handler(object sender, EventArgs e) => TagSelect(Dev3ModbusCard);
        private void Dev4Handler(object sender, EventArgs e) => TagSelect(Dev4ModbusCard);

        private void Dev2Combo(object sender, EventArgs e)
        {
            if (!Dev1ModbusCard.TagSelection.IsReadOnly)
            {
                MessageBox.Show("Please save previous MODBUS first.","Unsaved MODBUS", MessageBoxButton.OK,MessageBoxImage.Warning);
                return;
            }
        }
        private void Dev3Combo(object sender, EventArgs e)
        {
            if (!Dev2ModbusCard.TagSelection.IsReadOnly)
            {
                MessageBox.Show("Please save previous MODBUS first.", "Unsaved MODBUS", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }
        private void Dev4Combo(object sender, EventArgs e)
        {
            if (!Dev3ModbusCard.TagSelection.IsReadOnly)
            {
                MessageBox.Show("Please save previous MODBUS first.", "Unsaved MODBUS", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }

        private void TagSelect(ModbusCard modbus)
        {
            if (!modbus.TagSelection.IsEnabled)
            {
                MessageBox.Show("Please select tag type");
                return;
            }
            foreach (var x in modbus.TagSelection.Items.OfType<User>().ToList())
                if (x.IsSelected)
                    Globals.TagCount--;

            modbus.TagSelection.IsReadOnly = true;
            FreeTagsTextBlock.Text = Globals.TagCount.ToString();
            modbus.CardSaveButton.IsEnabled = false;
        }
        #endregion

        #region Data Send Events
        private void TagSend(int address, ModbusCard modbus)
        {
            foreach (var x in modbus.TagSelection.Items.OfType<User>().ToList())
                if (x.IsSelected)
                    SendData(address + x.TagAddress, x.TagAddress.ToString());
        }
        private void SendData(int ID, string content) => serportModbus.Write($"\r{ID}*{content}\n");
        #endregion
       
    }
}
