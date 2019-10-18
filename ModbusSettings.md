# ModbusSettings
This is the window where I got the hardest time to code, as it is quite difficult to implement userControl, but now it is finished, and is working the way I want it to work, for now at least.

## 1. UI(xaml)
For now, I'm going to skip the window and other stuff. This time, what I'm going to apply is placing a ModbusCard element.

[How to make ModbusCard](ModbusCard.md)

They are placed inside a `<StackPanel>` set the orientation to Horizontal, and the height slightly greater than to the card height.
```xml
<StackPanel Orientation="Horizontal" Height="428" Margin="5">
    <local:ModbusCard Margin="5" x:Name="Dev1ModbusCard" />
    <local:ModbusCard Margin="5" x:Name="Dev2ModbusCard" />
    <local:ModbusCard Margin="5" x:Name="Dev3ModbusCard" />
    <local:ModbusCard Margin="5" x:Name="Dev4ModbusCard" IsEnabled="False"/>
</StackPanel>
```
>The `Dev4ModbusCard` is disabled due to device limit capable of accepting maximum of 100 strings saved to FRAM.

The end product should look like this, or something better if you have one in mind.

![](img/Modbus_Devices.png)

## 2. CODE-BEHIND (.xaml.cs)
This is simple as this is almost same as NetSettings.xaml.cs when it comes to concept.

1. Define namespaces
```cs
using System.Linq;
using System.Windows;
using System.IO.Ports;
using static FMG_Configurator__csharp_.ModbusCard;
```
> The 4th line will be useful in handling events when sending data to NX-400 via serial communication

2. Declare some variables.
```cs
public static int TagLeft = 64;
public MainWindow mainWindow = new MainWindow();
public static string type;
public SerialPort serportModbus = new SerialPort();
```

3. For `Loaded` event, set the title of each ModbusCard according to order. Leftmost is Device01 and rightmost is Device04. For `Closing`, do the same as every window: cancel, close serial port, enable and disable some buttons, show the main window, then set the visibility to `Visibility.Collapsed`

4. Create a `SendData()` method that accepts an int (ID) and a string (content). Inside that method perform a serial write with string interpolation.

Button Events
---
There are five (5) buttons that we need to set a click event: three (3) actual buttons, and two (2) menu buttons. Let us first focus on the Menu buttons:

#### Menu button click events
For `SerialOpen_Click` event, you just set the serial parameters and open the serial port. As for the `SerialClose_Click`, send a message to NX-400 that will close the serial port there, then close the serial port of the app.

#### `SaveButton_Click` event
First, we have to save it to a string array. Because we will need to check this later.

An important factor in Modbus is that no Node ID's should be similar. That's why we need to strictly implement a checker to determine if there are similar Node ID with other ModbusCards. If one was found, the event would immediately be cancelled.

The logic here is that I used nested for loops, which first checks if the specified address in the array has value, then for every count it will check if there are similar values inside that array. This will run only once for every click event.
```cs
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
```
> I know, I know. *But O great master, there is a simpler way to do this.* Yeah, but I don't know how to do it at the time I'm making this documentation. If you can create a simpler code for this, then do it! If it would make the program function better, why not? This is a free world. Be adventurous!

Next up, we will call `SendData()` method to send all RTU settings data such as baud rate, parity, etc. Just in case you don't know, RTU is the serial communication for MODBUS protocol. Feel free to learn it, it's awesome!

Then we will look at the `ModbusCards`. First, if they have a Node ID, and if their datagrid is enabled, then we will send all of the data inside that card.

1. Send the Node ID.
2. Determine what Tag type is selected, then send the selected type to NX-400. `0` for Modbus Coils(0x), `1`, for Discrete Inputs(1x), and so forth.
3. For every checked tag in the datagrid, we will send it to NX-400.
4. Lastly, we will set the datagrid `.IsReadOnly` to true.

To give you an example, here is the code for the 1st `ModbusCard`:
```cs
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
    foreach (var x in Dev1ModbusCard.TagSelection.Items.OfType<User>().ToList())
    {
        if (x.IsSelected)
        {
            TagLeft -= 1;
            SendData(36 + x.TagAddress, x.TagAddress.ToString());
        }               
        // send id, and name to device?
    }
    Dev1ModbusCard.TagSelection.IsReadOnly = true;
}
```
>IMPORTANT! Be careful again with the addresses used, as they are the same address location saved in NX-400. e.g. `SendData(36, type);` The 36 here is the address, and the type is the content.

Do this for all the other `ModbusCards`. I only set up to 3 due to device address limitations for now.

Count all the number of tags used and display it in the UI. Lastly, when all is done, prompt the user that all data has been sent.

#### `ResetButton_Click` event
This method only "resets" the `ModbusCards` and the UI then sends empty data to NX-400 from address `30` to `100`. Lastly, the user is prompted that all data has been reset.

#### `EditButton_Click` event
Almost the same as `ResetButton_Click` event, except that this doesn't reset the `ModbusCards`.

----

That's it! You've done it. Tell me, are we finished here?

NO.

This is just the beginning.

The next task is to improve this software, and add other important features. Make this the best software for this application.

Best of luck Jedi. May the force be with you.
