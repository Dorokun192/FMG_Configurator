#pragma checksum "..\..\..\ModbusSettings.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "9672CDF238A19A43E257BE4C3316E11454D0172566E3DE75086ED199FB525F15"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FMG_Configurator__csharp_;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace FMG_Configurator__csharp_
{


    /// <summary>
    /// ModbusSettings
    /// </summary>
    public partial class ModbusSettings : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {

#line default
#line hidden


#line 13 "..\..\..\ModbusSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel menubar;

#line default
#line hidden


#line 16 "..\..\..\ModbusSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem SerialOpen;

#line default
#line hidden


#line 17 "..\..\..\ModbusSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem SerialClose;

#line default
#line hidden


#line 39 "..\..\..\ModbusSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox RTUBaudRate;

#line default
#line hidden


#line 47 "..\..\..\ModbusSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox RTUParity;

#line default
#line hidden


#line 53 "..\..\..\ModbusSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox RTUBit;

#line default
#line hidden


#line 58 "..\..\..\ModbusSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox RTUPollTime;

#line default
#line hidden


#line 60 "..\..\..\ModbusSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox RTUTimeout;

#line default
#line hidden


#line 67 "..\..\..\ModbusSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock FreeTagsTextBlock;

#line default
#line hidden


#line 75 "..\..\..\ModbusSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal FMG_Configurator__csharp_.ModbusCard Dev1ModbusCard;

#line default
#line hidden


#line 76 "..\..\..\ModbusSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal FMG_Configurator__csharp_.ModbusCard Dev2ModbusCard;

#line default
#line hidden


#line 77 "..\..\..\ModbusSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal FMG_Configurator__csharp_.ModbusCard Dev3ModbusCard;

#line default
#line hidden


#line 78 "..\..\..\ModbusSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal FMG_Configurator__csharp_.ModbusCard Dev4ModbusCard;

#line default
#line hidden


#line 80 "..\..\..\ModbusSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddCardButton;

#line default
#line hidden


#line 91 "..\..\..\ModbusSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveButton;

#line default
#line hidden


#line 92 "..\..\..\ModbusSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EditButton;

#line default
#line hidden


#line 93 "..\..\..\ModbusSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ResetButton;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/FMG Configurator;component/modbussettings.xaml", System.UriKind.Relative);

#line 1 "..\..\..\ModbusSettings.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler)
        {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.modbusWin = ((FMG_Configurator__csharp_.ModbusSettings)(target));

#line 11 "..\..\..\ModbusSettings.xaml"
                    this.modbusWin.Loaded += new System.Windows.RoutedEventHandler(this.modbusWin_Loaded);

#line default
#line hidden

#line 11 "..\..\..\ModbusSettings.xaml"
                    this.modbusWin.Closing += new System.ComponentModel.CancelEventHandler(this.modbusWin_Closing);

#line default
#line hidden
                    return;
                case 2:
                    this.menubar = ((System.Windows.Controls.DockPanel)(target));
                    return;
                case 3:
                    this.SerialOpen = ((System.Windows.Controls.MenuItem)(target));

#line 16 "..\..\..\ModbusSettings.xaml"
                    this.SerialOpen.Click += new System.Windows.RoutedEventHandler(this.SerialOpen_Click);

#line default
#line hidden
                    return;
                case 4:
                    this.SerialClose = ((System.Windows.Controls.MenuItem)(target));

#line 17 "..\..\..\ModbusSettings.xaml"
                    this.SerialClose.Click += new System.Windows.RoutedEventHandler(this.SerialClose_Click);

#line default
#line hidden
                    return;
                case 5:
                    this.RTUBaudRate = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 6:
                    this.RTUParity = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 7:
                    this.RTUBit = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 8:
                    this.RTUPollTime = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 9:
                    this.RTUTimeout = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 10:
                    this.FreeTagsTextBlock = ((System.Windows.Controls.TextBlock)(target));
                    return;
                case 11:
                    this.Dev1ModbusCard = ((FMG_Configurator__csharp_.ModbusCard)(target));
                    return;
                case 12:
                    this.Dev2ModbusCard = ((FMG_Configurator__csharp_.ModbusCard)(target));
                    return;
                case 13:
                    this.Dev3ModbusCard = ((FMG_Configurator__csharp_.ModbusCard)(target));
                    return;
                case 14:
                    this.Dev4ModbusCard = ((FMG_Configurator__csharp_.ModbusCard)(target));
                    return;
                case 15:
                    this.AddCardButton = ((System.Windows.Controls.Button)(target));

#line 80 "..\..\..\ModbusSettings.xaml"
                    this.AddCardButton.Click += new System.Windows.RoutedEventHandler(this.AddCardButton_Click);

#line default
#line hidden
                    return;
                case 16:
                    this.SaveButton = ((System.Windows.Controls.Button)(target));

#line 91 "..\..\..\ModbusSettings.xaml"
                    this.SaveButton.Click += new System.Windows.RoutedEventHandler(this.SaveButton_Click);

#line default
#line hidden
                    return;
                case 17:
                    this.EditButton = ((System.Windows.Controls.Button)(target));

#line 92 "..\..\..\ModbusSettings.xaml"
                    this.EditButton.Click += new System.Windows.RoutedEventHandler(this.EditButton_Click);

#line default
#line hidden
                    return;
                case 18:
                    this.ResetButton = ((System.Windows.Controls.Button)(target));

#line 93 "..\..\..\ModbusSettings.xaml"
                    this.ResetButton.Click += new System.Windows.RoutedEventHandler(this.ResetButton_Click);

#line default
#line hidden
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Window modbusWin;
    }
}

