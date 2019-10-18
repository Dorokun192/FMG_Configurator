# MainWindow
This window will handle all other windows of the app.

## 1. UI(XAML)
First of all, let's set the "look" of the window. This is same as the SerialSettings.xaml. We will only need to add the following code inside "Window".
```
Background="{DynamicResource MaterialDesignPaper}"
TextElement.Foreground="{DynamicResource MaterialDesignBody}"
```
Then we'll set the following settings, `Height="294" Width="281" ResizeMode="CanMinimize" SizeToContent="WidthAndHeight"` and finally, set the name of the window to *mainWin*.

Inside the grid, we first set a dockpanel, which will contain the "menu" of the window.
```
<DockPanel Height="39" LastChildFill="False" VerticalAlignment="Top" Margin="0,0,0,0">
    <Menu DockPanel.Dock="Top" Height="39" HorizontalAlignment="Left" Width="274" Margin="0,0,0,0">
        <MenuItem Header="_File" FontSize="12" SnapsToDevicePixels="True" Height="39"   >
            <MenuItem Header="_New" />
            <MenuItem Header="_Open" />
            <MenuItem Header="_Save" />
            <Separator />
            <MenuItem Header="_Exit" Name="ExitButton" Click="ExitButton_Click" />
        </MenuItem>
        <MenuItem Header="_Settings" SnapsToDevicePixels="True" Height="39">
            <MenuItem Header="_Serial" x:Name="SerialMenuItem" Click="SerialMenuItem_Click"/>
        </MenuItem>
    </Menu>
</DockPanel>
```
>Take note of the click events. For other MenuItems they're on standby for future features.

Then we will use th logo of the FMGuard. Insert it using the `<Image>` element.

Also add buttons and textblocks naming them according to their function.

The end result should look like this:

![](img/mainwindow2.png "Main Window b4 connection")

> When the app is connected to the device, and the instruction was followed, the instruction will disappear, *DevID* will be replaced by the actual device id, and both *Net. Settings* and *Modbus* buttons will be enabled.

## 2. CODE-BEHIND(.xaml.cs)
First, we need to add necessary namespaces:
```cs
using System;
using System.Windows;
using System.IO.Ports;
using System.Windows.Threading;
```
Inside the public partial class, we need to define the necessary variables.

1. Setting up other windows:
```cs
public static SerialSettings serialSettings = new SerialSettings();
public static ModbusSettings modbusSettings = new ModbusSettings();
public static NetSettings netSettings = new NetSettings();
```

2. Setting up a serial port and a string to be used for reading serial data:
```cs
public static SerialPort serportMain = new SerialPort();
public static string indata;
```

3. Lastly, we're going to remove the close button on the window. Honestly, I don't know how this works because I just got it from a post online, but, it works. So we'll leave it as is:
```cs
// Prep stuff needed to remove close button on window
private const int GWL_STYLE = -16;
private const int WS_SYSMENU = 0x80000;
[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
[System.Runtime.InteropServices.DllImport("user32.dll")]
private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
```
Next, we'll need to create "Loaded" and "Closing" events. Do the same as before, or you could just add it on the xaml code.

Inside the "Loaded" event we will set the following to remove the close button first:
```cs
var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
```
Then we'll set up the serial parameters. One way to prevent errors in the future is that we enclose it inside an "if" statement to determine if the window is loaded for the first time or not.
```cs
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
```
> [!TIP]
>serPortMain.DataReceived is critical for receiving information from the device.

For the "Closing" event it's the same as before:
```cs
private void MainWin_Closing(object sender, System.ComponentModel.CancelEventArgs e)
{
    e.Cancel = true;
    Globals.firstTime = false;
    Visibility = Visibility.Collapsed;
}
```
For button "click" event, just call the window stated in the button and close the main window:
```cs
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
```
### SerialDataReceived event
1. Set the void for *DataReceivedHandler* event
```cs
private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
{

}
```
2. Inside it we need to set an serial port listener. Then use the string that we defined earlier to contain the serial data received.
```cs
SerialPort sp = (SerialPort)sender;
indata = sp.ReadLine();
```
> [NOTE]
> having this in readline is critical for receiving serial data. It determines where the message should stop. This is also reflected in the device IDE.

After that we will invoke a thread on a background that will check whenever the app receives something. It goes like this:
```cs
Application.Current.Dispatcher.BeginInvoke(
    DispatcherPriority.Background,
    new Action(() => 
    {               
        Globals.devID = indata.Split('*')[1];
        serportMain.Close();
        DevIDTextBlock.Text = Globals.devID;
        NetworkSettingsButton.IsEnabled = true;
        ModbusSettingsButton.IsEnabled = true;
        Instruction.Visibility = Visibility.Collapsed;
        MessageBox.Show("Device ID received");
    }));
```
As you can see inside the Action() the received data is split by a delimiter, then we close the serial port to prevent other information from coming back to us, then we saved it to a global variable.

Once the data has been received we enable the buttons, change the value of textblock, and hide the visibility of the instruction. Finally, we inform the user that we have already received the data from the device.

We now have finished creating the mainWindow! Good job!
Only a few more windows to go, and we will be finished before you can say **"I need help and work is the only thing that makes me happy right now because I don't feel worthless at all in here"** :smile: