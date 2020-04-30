using System;
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
    /// Interaction logic for TextSuffix.xaml
    /// </summary>
    public partial class TextSuffix : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static readonly DependencyProperty TextProperty =
                    DependencyProperty.Register("Text", typeof(string), typeof(TextSuffix));

        public static readonly DependencyProperty IsReadOnlyProperty =
                    DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(TextSuffix), new FrameworkPropertyMetadata(true));

        public static readonly DependencyProperty SuffixProperty =
                    DependencyProperty.Register("Suffix", typeof(string), typeof(TextSuffix),
                        new FrameworkPropertyMetadata(
                            "",
                            new PropertyChangedCallback(OnSuffixPropertyChanged)));

        public string Text
        {
            get
            {
                return GetValue(TextProperty) as string;
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return (bool)GetValue(IsReadOnlyProperty);
            }
            set
            {
                SetValue(IsReadOnlyProperty, value);
            }
        }

        public string Suffix
        {
            get
            {
                return GetValue(SuffixProperty) as string;
            }
            set
            {
                SetValue(SuffixProperty, value);
            }
        }

        public bool SVisible
        {
            get => Suffix?.Length != 0;
        }

        public TextSuffix()
        {
            InitializeComponent();
        }

        private static void OnSuffixPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is TextSuffix c)
            {
                c.NotifyPropertyChanged("SVisible");
            }
        }

        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
