# NetSettings
This window will handle all events related to network and MQTT connection.

First of all, I want to congratulate you for getting this far, it means that you really want to learn about this software. Have you patted yourself on the back? Good. Let's proceed.

## Prerequisite
You must first create a userControl for your IPMaskTextBox. This is also critical. Go to IPMaskedTextBox.md before continuing right here.

The next prerequisite is that you know what are the groupings of the parameters, such as for DCHP, inside it has the IP address, Subnet Mask, and Gateway. Then for DNS, it has DNS1 and DNS2. This is important for creating the UI later. I will not show the xaml code exactly but you'll get the picture of how it should look like. Also, again, the whole source code will be included together with this documentation.


## 1. UI(XAML)
This part is quite tedious as we have so many network parameters. There are three (3) network configurations that are available in NX-400: LAN, WLAN, and MOBILE. Each has their own IP addresses, subnets, gateways, and DNS's. But do not be afraid, young padawan, for I am here to guide you slowly but surely. Also, all actual source code is available here, so don't you worry about a thing.

1. Add UI elements to the `<Window>` element.
```
Background="{DynamicResource MaterialDesignPaper}"
TextElement.Foreground="{DynamicResource MaterialDesignBody}"
```
2. Set the physical parameters of the window:
```
Title="NetSettings" Height="695.871" Width="302.239" ResizeMode="NoResize" Closing="NetSettingsWindow_Closing" Loaded="NetSettingsWindow_Loaded" Icon="Images/favicon.ico" ShowInTaskbar="True" WindowStartupLocation="CenterScreen" Topmost="True" >
```
>I have already added the "Loaded" and "Closing" event, as well as behavior of the window.

3. Create a `<DockPanel></DockPanel>` for Menu buttons:
```xml
<DockPanel Height="39" LastChildFill="False" VerticalAlignment="Top" Margin="0,0,0,0">
    <Menu DockPanel.Dock="Top" Height="39" HorizontalAlignment="Left" Width="296" Margin="0,0,0,0" Background="#FF673AB7">
        <MenuItem Header="_Serial" FontSize="12" SnapsToDevicePixels="True" Height="39" Foreground="White"   >
            <MenuItem x:Name="SerialOpen" Header="_Open" Click="SerialOpen_Click" />
            <MenuItem x:Name="SerialClose" Header="_Close" Click="SerialClose_Click" IsEnabled="False" />
        </MenuItem>
        <MenuItem Header="_Data" FontSize="12" SnapsToDevicePixels="True" Height="39" Foreground="White"   >
            <MenuItem x:Name="LoadDevData" Header="_Load" Click="LoadDevData_Click" />
            <MenuItem x:Name="ResetDevData" Header="_Reset" Click="ResetDevData_Click" />
        </MenuItem>
    </Menu>
</DockPanel>
```
>NOTE: Again, take note of the click events, we will use all of this later on...

4. Create a `<TabControl></TabControl>` element and under it add four (4) tabs naming them LanTab, WlanTab, MobileTab, and MQTTTab with appropriate headers
```xml
<TabControl x:Name="NetworkTabControl" Margin="10,56,10,50.4" Background="#FF7B1FA2" BorderBrush="#FF7B1FA2" IsEnabled="False">
    <TabItem x:Name="LanTab" Header="LAN" >
        <Grid Background="{DynamicResource MaterialDesignPaper}" TextElement.Foreground="{DynamicResource MaterialDesignBody}">
        <!--all necessary params for LAN (ip,subnetmask,etc-->
        </Grid>
    </TabItem>
    <TabItem x:Name="WlanTab" Header="WLAN">
        <Grid Background="{DynamicResource MaterialDesignPaper}" TextElement.Foreground="{DynamicResource MaterialDesignBody}" Margin="0,0,0,0.4">
        <!--all necessary params for WLAN (ip,subnetmask,etc-->
        </Grid>
    </TabItem>
    <TabItem x:Name="MobileTab" Header="Mobile" >
        <Grid Background="{DynamicResource MaterialDesignPaper}" TextElement.Foreground="{DynamicResource MaterialDesignBody}">
            <!--all necessary params for Mobile (ip,subnetmask,etc-->
        </Grid>
    </TabItem>
    <TabItem x:Name="MQTTTab" Header="MQTT">
        <Grid Background="{DynamicResource MaterialDesignPaper}" TextElement.Foreground="{DynamicResource MaterialDesignBody}">
            <!--all necessary params for MQTT (passwords, analog timeout length, etc-->
        </Grid>
    </TabItem>
</TabControl>
```
> I won't post all code here as it would be too long for this document. The whole source code is provided together with this document.

5. Lastly, add three buttons for the window. *Apply* button handles current selected tab, *OK* button handles all tabs, and *Close* button closes the window. (*Yeah, I know, two (2) close buttons? Am I crazy?* No, no I'm not. Well, at least not yet.)

```xml
<Button x:Name="ApplyButton" Content="Apply" Style="{StaticResource MaterialDesignRaisedDarkButton}" HorizontalAlignment="Left" Margin="10,621,0,10.4" Width="75" Height="Auto" Click="ApplyButton_Click" IsEnabled="False" />
<Button x:Name="OKButton" Content="OK" Style="{StaticResource MaterialDesignRaisedDarkButton}" HorizontalAlignment="Left" Margin="102,621,0,10.4" Width="75" Click="OKButton_Click" Height="Auto" IsEnabled="False"/>
<Button x:Name="CloseButton" Content="Close" Style="{StaticResource MaterialDesignFlatButton}" Margin="196,621,10,10.4" Click="CloseButton_Click" Height="Auto" IsEnabled="False"/>
```
Good, after all of that, your UI should look like this:

![](img/netsettings.png "Network Settings")

## 2. CODE-BEHIND (.xaml.cs)

Now for the fun part. We will write a code to handle all the events and such.

1. Define the namespaces that we will use
```cs
using System;
using System.Windows;
using System.IO.Ports;
```
2. Define the window, a serial port, and a string array to contain all values of user (limit it to 100 for now due to device limitation)
```cs
public static Mainwindow mainWindow = new MainWindow();
public static SerialPort serportNet = new SerialPort();
public static string[] fmg = new string[101];
```
3. Write the loaded and closing event. For loaded, this is as simple as loading the device ID global variable:
```cs
private void NetSettingsWindow_Loaded(object sender, RoutedEventArgs e) => DeviceIDTextBlock.Text = Globals.devID;

private void NetSettingsWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
{
    e.Cancel = true;

    if (serportNet.IsOpen)
        serportNet.Close();

    Visibility = Visibility.Collapsed;
    mainWindow.Show();
}
```

CHECK Events
---
This event handles what will happen to the grids when the checkbox or the radiobutton is checked

e.g.
```cs
private void MobileOptionalCheckBox_Checked(object sender, RoutedEventArgs e) => MobileOptionalGrid.IsEnabled = true;    
private void MobileOptionalCheckBox_Unchecked(object sender, RoutedEventArgs e) => MobileOptionalGrid.IsEnabled = false;
```
>Do this for all grids inside the tab per network configuration.

Then we will create a loadData() method to load all previously saved data to the UI.

For example:
```cs
private void loadData()
{
    LanIPTextBox.LoadIPAddress(fmg[1]);
    LanSubnetMaskTextBox.LoadIPAddress(fmg[2]);
    LanGatewayTextBox.LoadIPAddress(fmg[3]);
    LanDNS1TextBox.LoadIPAddress(fmg[4]);
    LanDNS2TextBox.LoadIPAddress(fmg[5]);

    // same as others for WLAN, Mobile, and MQTT
}
```
>NOTE: The *LoadIPAddress* is defined the code-behind of the userControl for IPMaskedTextBox. Go check it out.

---
Serial Events
---
We only have one event listed in here, and that is related to sending the string values to NX-400. We will call the method **SendStrings**. Pretty straightforward, right? We will also require two (2) integers from the user as inputs.
```cs
private void SendStrings(int a, int b)
{
    for (int i = a; i <= b; i++)
        if (fmg[i] != "")
            serportNet.Write($"\r{i}*{fmg[i]}\n");
}
```
> The content of the Write method is critical for sending data to the device. Here we encapsulate the message with \r (Carriage Return) and end it with \n (NewLine). *i* stands for the ID of the message, and is critical for identification in NX-400. **Be careful in remembering the ID as the ID in here is similar to the ID to be used in NX-400 for it to be placed in the right address**. Also, I implemented <a target="_blank" rel="noopener noreferrer" href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated">string interpolation</a> to make the format easier to understand through the eyes of the developer.
---

Settings Apply
---
In this section we will handle the methods to send data via serial network. For a concrete example, I will provide the code for LanSettingsApply(). For other networks, they have the same concept.

First we will create a method called *LanSettingsApply* then inside it we will determine a two conditions. First condition is that if the `LDHCPGrid.IsEnabled` returns true, then it will save all values in the string array and then send all values to NX-400.
```cs
if (LDHCPGrid.IsEnabled)
{
    fmg[1] = LanIPTextBox.GetString();
    fmg[2] = LanSubnetMaskTextBox.GetString();
    fmg[3] = LanGatewayTextBox.GetString();

    SendStrings(1, 3);
}
```
> The `GetString()` method is defined in the IPMaskedTextBox. Go check it out yo!

> IMPORTANT! Always take good care in remembering the addresses of the string values, as they are critical in sending it to the device. This addresses serve as their ID that will be recognized by NX-400 when being received through serial communication.

The next condition is that if `LDNSGrid.IsEnabled` returns true, then it will save the values to the string array, and send all that values to NX-400.
```cs
if (LDNSGrid.IsEnabled)
{
    fmg[4] = LanDNS1TextBox.GetString();
    fmg[5] = LanDNS2TextBox.GetString();

    SendStrings(4, 5);
}
```

The whole code for the said method should look like this:
```cs
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
```
Got it? Good. Now do the same for other network and MQTT methods.

---

Button Click Events
---
This one is easy, as for *Apply* and *OK* button click events we only need to call out necessary methods already written above.

```cs
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
```
---

Menu Click Events
---
This handles the click event for menu items. For `LoadDevData_Click`, we just need to call the `loadData()` method. Then for `ResetDevData_Click` we just need to send empty data to NX-400, pretty similar to `SendStrings()` method.

```cs
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
```

For serial click event, this is the same as in MainWindow "Loaded" event, so I will show you the whole code below.
```cs
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
```
We're done! Hats off to you for finishing it on your own! I'm so proud of you. Next up, we will tackle the last window, ModbusSettings. Goodluck!