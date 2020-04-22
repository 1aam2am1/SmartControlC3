﻿using SmartControl.WorkViews;
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
    /// Interaction logic for WorkView.xaml
    /// </summary>
    public partial class WorkView : UserControl
    {
        Lazy<DisplayView> display = new Lazy<DisplayView>(new DisplayView());
        Lazy<CalendarView> calendar = new Lazy<CalendarView>(new CalendarView());
        Lazy<ModesView> modes = new Lazy<ModesView>(new ModesView());
        Lazy<ChartsView> charts = new Lazy<ChartsView>(new ChartsView());
        Lazy<SettingsView> settings = new Lazy<SettingsView>(new SettingsView());
        Lazy<ServiceView> service = new Lazy<ServiceView>(new ServiceView());
        public WorkView()
        {
            InitializeComponent();

            DataContext = display.Value;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataContext = display.Value;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DataContext = calendar.Value;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DataContext = modes.Value;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            DataContext = charts.Value;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            DataContext = settings.Value;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            DataContext = service.Value;
        }
    }
}