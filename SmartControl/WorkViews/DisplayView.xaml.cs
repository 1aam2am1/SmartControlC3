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

        public ObservableCollection<int> Parameters
        {
            get => client?.GetDataManager().Parameters;
        }


        public int Power
        {
            get
            {
                int[] heater = { 42, 43 };
                int[] fan = { 27, 29 };
                int result = 0;
                BindingOperations.AccessCollection(Parameters, () =>
                {
                    foreach (var i in heater)
                    {
                        result += Parameters[i] * 10;
                    }
                    foreach (var i in fan)
                    {
                        result += (int)PowerFromFan(Parameters[i]);
                    }
                }, false);


                return result + 5;
            }
        }

        public int Recovered
        {
            get
            {
                int value = 0;
                BindingOperations.AccessCollection(Parameters, () =>
                {
                    value = Parameters[37];
                }, false);
                return value;
            }
        }

        public int Humidity1
        {
            get
            {
                int value = 0;
                BindingOperations.AccessCollection(Parameters, () =>
                {
                    value = Parameters[26];
                }, false);
                return value;
            }
            set
            {
                BindingOperations.AccessCollection(Parameters, () =>
                {
                    Parameters[26] = value;
                }, true);
            }
        }
        public int Rpm1
        {
            get
            {
                int value = 0;
                BindingOperations.AccessCollection(Parameters, () =>
                {
                    value = Parameters[27];
                }, false);
                return value;
            }
            set
            {
                BindingOperations.AccessCollection(Parameters, () =>
                {
                    Parameters[27] = value;
                }, true);
            }
        }
        public int Humidity2
        {
            get
            {
                int value = 0;
                BindingOperations.AccessCollection(Parameters, () =>
                {
                    value = Parameters[28];
                }, false);
                return value;
            }
            set
            {
                BindingOperations.AccessCollection(Parameters, () =>
                {
                    Parameters[28] = value;
                }, true);
            }
        }
        public int Rpm2
        {
            get
            {
                int value = 0;
                BindingOperations.AccessCollection(Parameters, () =>
                {
                    value = Parameters[29];
                }, false);
                return value;
            }
            set
            {
                BindingOperations.AccessCollection(Parameters, () =>
                {
                    Parameters[29] = value;
                }, true);
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
        //TODO: Remove setclient move to constructor
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
