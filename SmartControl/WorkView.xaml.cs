using SmartControl.Api;
using SmartControl.WorkViews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        readonly DisplayView display;
        readonly CalendarView calendar;
        readonly ModesView modes;
        readonly ChartsView charts;
        readonly WorkViews.SettingsView settings;
        readonly ServiceView service;
        public WorkView(IClient c)
        {
            InitializeComponent();

            display = new DisplayView(c);
            calendar = new CalendarView(c);
            modes = new ModesView(c);
            charts = new ChartsView(c);
            settings = new WorkViews.SettingsView(c);
            service = new ServiceView(c);

            DataContext = display;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataContext = display;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DataContext = calendar;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DataContext = modes;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            DataContext = charts;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            DataContext = settings;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            DataContext = service;
        }
    }
}
