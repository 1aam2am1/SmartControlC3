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
        readonly Lazy<DisplayView> display = new Lazy<DisplayView>(new DisplayView());
        readonly Lazy<CalendarView> calendar = new Lazy<CalendarView>(new CalendarView());
        readonly Lazy<ModesView> modes = new Lazy<ModesView>(new ModesView());
        readonly Lazy<ChartsView> charts = new Lazy<ChartsView>(new ChartsView());
        readonly Lazy<WorkViews.SettingsView> settings = new Lazy<WorkViews.SettingsView>(new WorkViews.SettingsView());
        readonly Lazy<ServiceView> service = new Lazy<ServiceView>(new ServiceView());
        public WorkView()
        {
            InitializeComponent();

            DataContext = display.Value;
        }

        public async void SetClient(IClient c)
        {
            await Task.Run(() =>
            {
                display.Value.SetClient(c);
                calendar.Value.SetClient(c);
                modes.Value.SetClient(c);
                charts.Value.SetClient(c);
                settings.Value.SetClient(c);
                service.Value.SetClient(c);
            });
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
