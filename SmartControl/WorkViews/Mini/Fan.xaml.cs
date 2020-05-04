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

namespace SmartControl.WorkViews.Mini
{
    /// <summary>
    /// Interaction logic for Fan.xaml
    /// </summary>
    public partial class Fan : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static readonly DependencyProperty DescriptionProperty =
                    DependencyProperty.Register("Description", typeof(string), typeof(Fan));

        public static readonly DependencyProperty ParametersProperty =
                    DependencyProperty.Register("Parameters", typeof(ObservableCollection<int>), typeof(Fan),
                        new FrameworkPropertyMetadata(null,
                            new PropertyChangedCallback(OnParametersSourceChanged)));

        public static readonly DependencyProperty TempIdProperty =
                    DependencyProperty.Register("TempId", typeof(int), typeof(Fan),
                        new FrameworkPropertyMetadata(0,
                            new PropertyChangedCallback(
                                (d, e) => (d as Fan)?.NotifyPropertyChanged("Temp")
                                )));

        public static readonly DependencyProperty WaterIdProperty =
                    DependencyProperty.Register("WaterId", typeof(int), typeof(Fan),
                        new FrameworkPropertyMetadata(0,
                            new PropertyChangedCallback(
                                (d, e) => (d as Fan)?.NotifyPropertyChanged("Water")
                                )));

        public static readonly DependencyProperty PMIdProperty =
                    DependencyProperty.Register("PMId", typeof(int), typeof(Fan),
                        new FrameworkPropertyMetadata(-1,
                            new PropertyChangedCallback(
                                (d, e) => (d as Fan)?.NotifyPropertyChanged("PM")
                                )));

        public string Description
        {
            get
            {
                return GetValue(DescriptionProperty) as string;
            }
            set
            {
                SetValue(DescriptionProperty, value);
            }
        }

        public ObservableCollection<int> Parameters
        {
            get
            {
                return GetValue(ParametersProperty) as ObservableCollection<int>;
            }
            set
            {
                SetValue(ParametersProperty, value);
            }
        }

        public int TempId
        {
            get
            {
                return (int)GetValue(TempIdProperty);
            }
            set
            {
                SetValue(TempIdProperty, value);
            }
        }
        public int WaterId
        {
            get
            {
                return (int)GetValue(WaterIdProperty);
            }
            set
            {
                SetValue(WaterIdProperty, value);
            }
        }
        public int PMId
        {
            get
            {
                return (int)GetValue(PMIdProperty);
            }
            set
            {
                SetValue(PMIdProperty, value);
            }
        }

        public int Temp
        {
            get
            {
                if (Parameters != null)
                {
                    int value = 0;
                    BindingOperations.AccessCollection(Parameters, () =>
                    {
                        value = Parameters[TempId];
                    }, false);
                    return value;
                }
                return 0;
            }
        }

        public int Water
        {
            get
            {
                if (Parameters != null)
                {
                    int value = 0;
                    BindingOperations.AccessCollection(Parameters, () =>
                    {
                        value = Parameters[WaterId];
                    }, false);
                    return value;
                }
                return 0;
            }
        }

        public int PM
        {
            get
            {
                if (Parameters != null && PMId != -1)
                {
                    int value = 0;
                    BindingOperations.AccessCollection(Parameters, () =>
                    {
                        value = Parameters[PMId];
                    }, false);
                    return value;
                }
                return 0;
            }
        }

        public Brush PMColor
        {
            get
            {
                if (PMId != -1)
                {
                    return new SolidColorBrush(Colors.Red);
                }
                else
                {
                    return new SolidColorBrush(Colors.Transparent);
                }
            }
        }

        public Brush PMVisible
        {
            get
            {
                if (PMId != -1)
                {
                    return new SolidColorBrush(Colors.White);
                }
                else
                {
                    return new SolidColorBrush(Colors.Transparent);
                }
            }
        }

        public Fan()
        {
            InitializeComponent();
        }

        private static void OnParametersSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Fan attachedObject)) return;

            if (e.OldValue is ObservableCollection<int> oldItems)
            {
                oldItems.CollectionChanged -= (s, e) => attachedObject.NotifyPropertyChanged("");
            }

            if (e.NewValue is ObservableCollection<int> sourceItems)
            {
                sourceItems.CollectionChanged += (s, e) => attachedObject.NotifyPropertyChanged("");
            }

            attachedObject.NotifyPropertyChanged("");
        }



        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
