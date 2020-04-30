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


        int _Power = 19;
        public int Power
        {
            get => _Power;
            set
            {
                _Power = value;
                NotifyPropertyChanged();
            }
        }

        public int Recovered
        {
            get => 20;
        }

        public Fan Intake
        {
            get => new Fan("Czerpnia");
        }

        public Fan Launch
        {
            get => new Fan("Wyrzutnia");
        }

        public Fan Extract
        {
            get => new Fan("Wywiew");
        }

        public Fan Suplay
        {
            get => new Fan("Nawiew");
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

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Power += 1;
        }

        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
