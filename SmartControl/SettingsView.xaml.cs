using SmartControl.Api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartControl
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        IConnectSettings settings;

        public bool ConnectSettingsEnabled { get => settings != null; }
        public string Url { get => settings.Url; set { settings.Url = value; } }

        public SettingsView()
        {
            InitializeComponent();
        }

        public void setConnectSettings(IConnectSettings s)
        {
            settings = s;
        }
    }
}
