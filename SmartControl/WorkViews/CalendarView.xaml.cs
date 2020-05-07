using SmartControl.Api;
using SmartControl.Api.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace SmartControl.WorkViews
{
    /// <summary>
    /// Interaction logic for CalendarView.xaml
    /// </summary>
    public partial class CalendarView : UserControl
    {
        private IClient client;

        public ObservableCollection<CalendarTask> Monday
        {
            get => client?.GetDataManager().Task[(int)DayOfWeek.Monday];
        }

        public Action<object> Delete { get; }

        public CalendarView()
        {
            InitializeComponent();

            Delete = (o) =>
            {
                Debug.WriteLine("Delete: {0}", object.ReferenceEquals(o, Monday[0]));
            };
        }

        public void SetClient(IClient c)
        {
            client = c;
        }
    }
}
