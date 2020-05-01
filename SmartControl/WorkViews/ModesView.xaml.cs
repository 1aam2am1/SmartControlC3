using SmartControl.Api;
using SmartControl.Api.Data;
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

namespace SmartControl.WorkViews
{
    /// <summary>
    /// Interaction logic for ModesView.xaml
    /// </summary>
    public partial class ModesView : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IClient client;

        private ReadOnlyDictionary<int, int> Parameters
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

        public ModesStatus Boost
        {
            get
            {
                if (client != null)
                {
                    client.GetDataManager().Modes.TryGetValue(0, out ModesStatus value);
                    return value;
                }
                return new ModesStatus();
            }
            set
            {
                client?.SaveModesQueue(0, value);
            }
        }

        public ModesStatus Airing
        {
            get
            {
                if (client != null)
                {
                    client.GetDataManager().Modes.TryGetValue(1, out ModesStatus value);
                    return value;
                }
                return new ModesStatus();
            }
            set
            {
                client?.SaveModesQueue(1, value);
            }
        }

        public ModesStatus Sleep
        {
            get
            {
                if (client != null)
                {
                    client.GetDataManager().Modes.TryGetValue(2, out ModesStatus value);
                    return value;
                }
                return new ModesStatus();
            }
            set
            {
                client?.SaveModesQueue(2, value);
            }
        }

        public ModesStatus Holiday
        {
            get
            {
                if (client != null)
                {
                    client.GetDataManager().Modes.TryGetValue(3, out ModesStatus value);
                    return value;
                }
                return new ModesStatus();
            }
            set
            {
                client?.SaveModesQueue(3, value);
            }
        }

        public ModesStatus MaxVent
        {
            get
            {
                if (client != null)
                {
                    client.GetDataManager().Modes.TryGetValue(4, out ModesStatus value);
                    return value;
                }
                return new ModesStatus();
            }
            set
            {
                client?.SaveModesQueue(4, value);
            }
        }

        public ModesStatus AirflowLimitation
        {
            get
            {
                Parameters.TryGetValue(59, out int value);
                return new ModesStatus { Value = value };
            }
            set
            {
                client?.SaveParametersQueue(59, value.Value);
            }
        }

        public ModesStatus ExhaustLimitation
        {
            get
            {
                Parameters.TryGetValue(60, out int value);
                return new ModesStatus { Value = value };
            }
            set
            {
                client?.SaveParametersQueue(59, value.Value);
            }
        }


        public ModesView()
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

    }
}
