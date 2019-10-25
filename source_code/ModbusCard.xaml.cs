using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FMG_Configurator__csharp_
{
    /// <summary>
    /// Interaction logic for ModbusCard.xaml
    /// </summary>
    public partial class ModbusCard : UserControl
    {
        public event EventHandler SaveButtonClicked;
        public event EventHandler ComboBoxChanged;
        List<User> users = new List<User>();
        private static readonly Regex regex = new Regex("[^0-9]+");
        private static ModbusSettings modbusSettings = new ModbusSettings();
        public ModbusCard() => InitializeComponent();

        public class User
        {
            public int TagAddress { get; set; }
            //public string TagID { get; set; }
            //public string TagName { get; set; }
            public bool IsSelected { get; set; }
        }

        private void DITagType_Selected(object sender, RoutedEventArgs e)
        {
            users.Clear();

            if (!TagSelection.IsEnabled)
                TagSelection.IsEnabled = true;

            AddUsers();
                

            TagSelection.ItemsSource = null;
            TagSelection.ItemsSource = users;
        }

        private void IRTagType_Selected(object sender, RoutedEventArgs e)
        {
            users.Clear();

            if (!TagSelection.IsEnabled)
                TagSelection.IsEnabled = true;

            AddUsers();
                

            TagSelection.ItemsSource = null;
            TagSelection.ItemsSource = users;
        }

        private void MCTagType_Selected(object sender, RoutedEventArgs e)
        {
            users.Clear();

            if (!TagSelection.IsEnabled)
                TagSelection.IsEnabled = true;

            AddUsers();

            TagSelection.ItemsSource = null;
            TagSelection.ItemsSource = users;
        }

        private void HRTagType_Selected(object sender, RoutedEventArgs e)
        {
            users.Clear();

            if (!TagSelection.IsEnabled)
                TagSelection.IsEnabled = true;

            AddUsers();

            TagSelection.ItemsSource = null;
            TagSelection.ItemsSource = users;
        }

        private void AddUsers()
        {
            for (int i = 1; i <= Globals.TagCount; i++)
            {
                //users.Add(new User() { TagAddress = i, TagID = $"HR{i}", TagName = $"A0{i}", IsSelected = false });
                users.Add(new User() { TagAddress = i});
            }
                
        }

        private void chkall_Checked(object sender, RoutedEventArgs e)
        {
            TagSelection.Items.OfType<User>().ToList().ForEach(x => x.IsSelected = true);
            TagSelection.ItemsSource = null;
            TagSelection.ItemsSource = users;
        }

        private void chkall_Unchecked(object sender, RoutedEventArgs e)
        {
            TagSelection.Items.OfType<User>().ToList().ForEach(x => x.IsSelected = false);
            TagSelection.ItemsSource = null;
            TagSelection.ItemsSource = users;
        }

        private void NodeIDTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) => e.Handled = !IsTextAllowed(e.Text);

        private static bool IsTextAllowed(string text) => !regex.IsMatch(text);

        private void NodeIDTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                    e.CancelCommand();
            }
            else
                e.CancelCommand();
        }

        private void NodeIDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NodeIDTextBox.Text == "")
                return;

            if (int.Parse(NodeIDTextBox.Text) > 127)
            {
                MessageBox.Show("Maximum node ID is 127!");
                NodeIDTextBox.Text = "";
            }
                
            return;
        }

        private void SaveButton_Clicked(object sender, RoutedEventArgs e)
        {
            SaveButtonClicked?.Invoke(sender, e);
        }

        private void DAQTagTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxChanged?.Invoke(sender, e);
        }
    }
}
