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

        public ObservableCollection<int> Parameters
        {
            get => client?.GetDataManager().Parameters;
        }

        public ModesStatus Boost
        {
            get
            {
                if (client != null)
                {
                    ModesStatus value = new ModesStatus();
                    BindingOperations.AccessCollection(client.GetDataManager().Modes, () =>
                    {
                        value = client.GetDataManager().Modes[0];
                    }, false);
                    return value;
                }
                return new ModesStatus();
            }
            set
            {
                BindingOperations.AccessCollection(client.GetDataManager().Modes, () =>
                {
                    client.GetDataManager().Modes[0] = value;
                }, true);
            }
        }

        public ModesStatus Airing
        {
            get
            {
                if (client != null)
                {
                    ModesStatus value = new ModesStatus();
                    BindingOperations.AccessCollection(client.GetDataManager().Modes, () =>
                    {
                        value = client.GetDataManager().Modes[1];
                    }, false);
                    return value;
                }
                return new ModesStatus();
            }
            set
            {
                BindingOperations.AccessCollection(client.GetDataManager().Modes, () =>
                {
                    client.GetDataManager().Modes[1] = value;
                }, true);
            }
        }

        public ModesStatus Sleep
        {
            get
            {
                if (client != null)
                {
                    ModesStatus value = new ModesStatus();
                    BindingOperations.AccessCollection(client.GetDataManager().Modes, () =>
                    {
                        value = client.GetDataManager().Modes[2];
                    }, false);
                    return value;
                }
                return new ModesStatus();
            }
            set
            {
                BindingOperations.AccessCollection(client.GetDataManager().Modes, () =>
                {
                    client.GetDataManager().Modes[2] = value;
                }, true);
            }
        }

        public ModesStatus Holiday
        {
            get
            {
                if (client != null)
                {
                    ModesStatus value = new ModesStatus();
                    BindingOperations.AccessCollection(client.GetDataManager().Modes, () =>
                    {
                        value = client.GetDataManager().Modes[3];
                    }, false);
                    return value;
                }
                return new ModesStatus();
            }
            set
            {
                BindingOperations.AccessCollection(client.GetDataManager().Modes, () =>
                {
                    client.GetDataManager().Modes[3] = value;
                }, true);
            }
        }

        public ModesStatus MaxVent
        {
            get
            {
                if (client != null)
                {
                    ModesStatus value = new ModesStatus();
                    BindingOperations.AccessCollection(client.GetDataManager().Modes, () =>
                    {
                        value = client.GetDataManager().Modes[4];
                    }, false);
                    return value;
                }
                return new ModesStatus();
            }
            set
            {
                BindingOperations.AccessCollection(client.GetDataManager().Modes, () =>
                {
                    client.GetDataManager().Modes[4] = value;
                }, true);
            }
        }

        public ModesStatus AirflowLimitation
        {
            get
            {
                int value = 0;
                BindingOperations.AccessCollection(Parameters, () =>
                {
                    value = Parameters[59];
                }, false);

                return new ModesStatus { Value = value };
            }
            set
            {
                BindingOperations.AccessCollection(Parameters, () =>
                {
                    Parameters[59] = value.Value;
                }, true);
            }
        }

        public ModesStatus ExhaustLimitation
        {
            get
            {
                int value = 0;
                BindingOperations.AccessCollection(Parameters, () =>
                {
                    value = Parameters[60];
                }, false);

                return new ModesStatus { Value = value };
            }
            set
            {
                BindingOperations.AccessCollection(Parameters, () =>
                {
                    Parameters[60] = value.Value;
                }, true);
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
