using System;
using System.Collections.Generic;
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
    /// Interaction logic for NumericText.xaml
    /// </summary>
    public partial class NumericText : UserControl
    {
        public static readonly DependencyProperty TextProperty =
                    DependencyProperty.Register("Text", typeof(int), typeof(NumericText));

        public static readonly DependencyProperty IsReadOnlyProperty =
                    DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(NumericText), new FrameworkPropertyMetadata(false));


        public static readonly DependencyProperty SuffixProperty =
                    DependencyProperty.Register("Suffix", typeof(string), typeof(NumericText),
                        new FrameworkPropertyMetadata(""));

        public static readonly DependencyProperty ButtonsVisibleProperty =
                    DependencyProperty.Register("ButtonsVisible", typeof(bool), typeof(NumericText),
                        new FrameworkPropertyMetadata(true));

        public string Text
        {
            get
            {
                return GetValue(TextProperty) as string;
            }
            set
            {
                try
                {
                    SetValue(TextProperty, int.Parse(value));
                }
                catch
                {

                }

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

        public bool ButtonsVisible
        {
            get
            {
                return (bool)GetValue(ButtonsVisibleProperty);
            }
            set
            {
                SetValue(ButtonsVisibleProperty, value);
            }
        }

        public NumericText()
        {
            InitializeComponent();
        }

        private void Button_Minus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int value = int.Parse(T.Text);

                SetValue(TextProperty, value - 1);
            }
            catch
            {
                SetValue(TextProperty, 0);
            }
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int value = int.Parse(T.Text);

                SetValue(TextProperty, value + 1);
            }
            catch
            {
                SetValue(TextProperty, 0);
            }
        }
    }
}
