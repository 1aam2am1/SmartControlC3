using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using SmartControl.Api;
using SmartControl.WorkViews.Mini;

namespace SmartControl.WorkViews
{
    /// <summary>
    /// Interaction logic for DisplayView.xaml
    /// </summary>
    public partial class DisplayView : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IClient client;

        public ReadOnlyDictionary<int, int> Parameters
        {
            get => client?.GetDataManager().Parameters;
            set
            {
                foreach (var v in value)
                {
                    client?.SaveParametersQueue(v.Key, v.Value);
                }
            }
        }


        public int Power
        {
            get
            {
                int[] heater = { 42, 43 };
                int[] fan = { 27, 29 };
                int result = 0;
                foreach (var i in heater)
                {
                    Parameters.TryGetValue(i, out int value);
                    result += value * 10;
                }
                foreach (var i in fan)
                {
                    Parameters.TryGetValue(i, out int value);
                    result += (int)PowerFromFan(value);
                }


                return result + 5;
            }
        }

        public int Recovered
        {
            get { Parameters.TryGetValue(37, out int value); return value; }
        }

        public int Humidity1
        {
            get { Parameters.TryGetValue(26, out int value); return value; }
            set
            {
                client?.SaveParametersQueue(26, value);
            }
        }
        public int Rpm1
        {
            get { Parameters.TryGetValue(27, out int value); return value; }
            set
            {
                client?.SaveParametersQueue(27, value);
            }
        }
        public int Humidity2
        {
            get { Parameters.TryGetValue(28, out int value); return value; }
            set
            {
                client?.SaveParametersQueue(28, value);
            }
        }
        public int Rpm2
        {
            get { Parameters.TryGetValue(29, out int value); return value; }
            set
            {
                client?.SaveParametersQueue(29, value);
            }
        }

        public int FilterOne
        {
            get => 50;
        }

        public int FilterTwo
        {
            get => 40;
        }

        public DisplayView()
        {
            InitializeComponent();
        }

        public void SetClient(IClient c)
        {
            if (client != null)
            {
                client.GetDataManager().PropertyChanged -= OnChange;
            }
            client = c;
            if (client != null)
            {
                client.GetDataManager().PropertyChanged += OnChange;
            }

            NotifyPropertyChanged();
        }

        void OnChange(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged("");
        }

        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        static double PowerFromFan(long speed)
        {
            if (speed == 0) { return 0; }

            double result = 0;
            result = Math.Pow(speed, 2) * (0.00000000280730835942661 * speed - 0.00000355746926477736) +
                     0.00732582964032312 * speed - 0.759107715961406;
            ///= 0,00000000280730835942661x3 - 0,00000355746926477736000x2 + 0,00732582964032312000000x - 0,75910771596140600000000
            return result;
        }
    }
}
