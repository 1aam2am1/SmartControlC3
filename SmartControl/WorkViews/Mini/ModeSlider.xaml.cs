using SmartControl.Api.Data;
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
    /// Interaction logic for ModeSlider.xaml
    /// </summary>
    public partial class ModeSlider : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static readonly DependencyProperty DescriptionProperty =
                    DependencyProperty.Register("Description", typeof(string), typeof(ModeSlider));

        public static readonly DependencyProperty MinimumProperty =
                    DependencyProperty.Register("Minimum", typeof(int), typeof(ModeSlider),
                        new FrameworkPropertyMetadata(0));

        public static readonly DependencyProperty ValueProperty =
                    DependencyProperty.Register("Value", typeof(ModesStatus), typeof(ModeSlider),
                        new FrameworkPropertyMetadata(new ModesStatus(),
                            new PropertyChangedCallback(
                                (d, e) => (d as ModeSlider)?.NotifyPropertyChanged("")
                                )));

        public static readonly DependencyProperty MaximumProperty =
                    DependencyProperty.Register("Maximum", typeof(int), typeof(ModeSlider),
                        new FrameworkPropertyMetadata(100));

        public static readonly DependencyProperty SuffixProperty =
                    DependencyProperty.Register("Suffix", typeof(string), typeof(ModeSlider),
                        new FrameworkPropertyMetadata(""));

        public static readonly DependencyProperty CheckBoxVisibleProperty =
                    DependencyProperty.Register("CheckBoxVisible", typeof(bool), typeof(ModeSlider),
                        new FrameworkPropertyMetadata(true));

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

        public int Minimum
        {
            get
            {
                return (int)GetValue(MinimumProperty);
            }
            set
            {
                try
                {
                    SetValue(MinimumProperty, value);
                    NotifyPropertyChanged("");
                }
                catch
                {

                }

            }
        }

        public int Maximum
        {
            get
            {
                return (int)GetValue(MaximumProperty);
            }
            set
            {
                try
                {
                    SetValue(MaximumProperty, value);
                    NotifyPropertyChanged("");
                }
                catch
                {

                }

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

        public ModesStatus Value
        {
            get
            {
                return (ModesStatus)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
                NotifyPropertyChanged("");
            }
        }

        public bool CheckBoxVisible
        {
            get => (bool)GetValue(CheckBoxVisibleProperty);
            set
            {
                SetValue(CheckBoxVisibleProperty, value);
            }
        }

        public int V
        {
            get => Value.Value;
            set
            {
                var v = Value;
                v.Value = value;
                Value = v;
                NotifyPropertyChanged();
            }
        }

        public bool E
        {
            get => Value.Active;
            set
            {
                var v = Value;
                v.Active = value;
                Value = v;
                NotifyPropertyChanged();
            }
        }


        public ModeSlider()
        {
            InitializeComponent();
        }

        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
