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
                    DependencyProperty.Register("Parameters", typeof(ReadOnlyDictionary<int, int>), typeof(Fan),
                        new FrameworkPropertyMetadata(null,
                            new PropertyChangedCallback(
                                (d, e) => (d as Fan)?.NotifyPropertyChanged("")
                                )));

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

        public ReadOnlyDictionary<int, int> Parameters
        {
            get
            {
                return GetValue(ParametersProperty) as ReadOnlyDictionary<int, int>;
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
                int value = 0;
                Parameters?.TryGetValue(TempId, out value);
                return value;
            }
        }

        public int Water
        {
            get
            {
                int value = 0;
                Parameters?.TryGetValue(WaterId, out value);
                return value;
            }
        }

        public int PM
        {
            get
            {
                int value = 0;
                Parameters?.TryGetValue(PMId, out value);
                return value;
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
                if(PMId != -1)
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

        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
