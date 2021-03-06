﻿using SmartControl.Api;
using Api.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
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
using SmartControl.Settings;

namespace SmartControl.WorkViews
{
    /// <summary>
    /// Interaction logic for ServiceView.xaml
    /// </summary>
    public partial class ServiceView : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IClient client;

        static public string Version
        {
            get
            {
                var v = Assembly.GetExecutingAssembly().GetName().Version;
                return string.Format("{0}.{1}.{2}.{3}", v.Major, v.Minor, v.Build, v.Revision);
            }
        }

        static public string BuildTime
        {
            get
            {
                return Settings.BuildTime.CompilationTimestampUtc.ToString();
            }
        }

        public string ApiVersion
        {
            get
            {
                var v = client.GetDataManager().ApiVersion;
                return string.Format("{0}.{1}.{2}.{3}", v.Major, v.Minor, v.Build, v.Revision);
            }
        }

        public string ServerVersion
        {
            get
            {
                var v = client.GetDataManager().ServerVersion;
                return string.Format("{0}.{1}.{2}.{3}", v.Major, v.Minor, v.Build, v.Revision);
            }
        }

        public string DeviceVersion
        {
            get
            {
                var v = client.GetDataManager().DeviceVersion;
                return string.Format("{0}.{1}.{2}.{3}", v.Major, v.Minor, v.Build, v.Revision);
            }
        }

        public ServiceView(IClient c)
        {
            InitializeComponent();

            client = c;
            client.GetDataManager().PropertyChanged += OnChange;
        }

        void OnChange(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged("");
        }

        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
