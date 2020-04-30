using System;
using System.Collections.Generic;
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

        string _Description;
        public string Description
        {
            get => _Description;
            set
            {
                _Description = value;
                NotifyPropertyChanged();
            }
        }

        public int Temp
        {
            get => 0;
        }

        public int Water
        {
            get => 0;
        }

        public int PM
        {
            get => 0;
        }

        public Brush PMColor
        {
            get => new SolidColorBrush(Colors.Red);
        }

        public Fan()
        {
            InitializeComponent();
        }

        public Fan(string n) : this()
        {
            Description = n;
        }


        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
