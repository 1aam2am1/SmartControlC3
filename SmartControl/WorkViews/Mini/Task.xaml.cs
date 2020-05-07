using SmartControl.Api.Data;
using SmartControl.Api.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
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
    /// Interaction logic for Task.xaml
    /// </summary>
    public partial class Task : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static readonly DependencyProperty ValueProperty =
                    DependencyProperty.Register("Value", typeof(CalendarTask), typeof(Task),
                        new FrameworkPropertyMetadata(new CalendarTask(),
                            new PropertyChangedCallback(
                                (d, e) => (d as Task)?.NotifyPropertyChanged("")
                                )));

        public static readonly DependencyProperty DeleteProperty =
                    DependencyProperty.Register("Delete", typeof(Action<CalendarTask>), typeof(Task));

        public CalendarTask Value
        {
            get
            {
                return (CalendarTask)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
                NotifyPropertyChanged("");
            }
        }

        public Action<CalendarTask> Delete
        {
            get
            {
                return (Action<CalendarTask>)GetValue(DeleteProperty);
            }
            set
            {
                SetValue(DeleteProperty, value);
                NotifyPropertyChanged("");
            }
        }

        public bool Active
        {
            get => Value.Enabled;
            set
            {
                var v = Value;
                v.Enabled = value;
                Value = v;
            }
        }

        public string StartHour
        {
            get => Value.Hour.ToString();
            set
            {
                var v = Value;
                var h = int.Parse(value).LimitToRange(0, 23);
                v.Duration += (v.Hour - h) * 60;
                v.Hour = h;
                Value = CheckTimeDuration(v);
            }
        }

        public string StartMinute
        {
            get => Value.Minute.ToString();
            set
            {
                var v = Value;
                var m = int.Parse(value).LimitToRange(0, 59);
                v.Duration += (v.Minute - m);
                v.Minute = m;
                Value = CheckTimeDuration(v);
            }
        }

        public string EndingHour
        {
            get => ((Value.Hour + (Value.Minute + Value.Duration) / 60) % 24).ToString();
            set
            {
                var v = Value;
                var h = int.Parse(value).LimitToRange(0, 23);

                int minutes_start = v.Hour * 60 + v.Minute;
                int minutes_stop = h * 60 + ((Value.Minute + Value.Duration) % 60);
                v.Duration = minutes_stop - minutes_start;

                Value = CheckTimeDuration(v);
            }
        }

        public string EndingMinute
        {
            get => ((Value.Minute + Value.Duration) % 60).ToString();
            set
            {
                var v = Value;
                var m = int.Parse(value).LimitToRange(0, 59);

                int minutes_start = v.Hour * 60 + v.Minute;
                int minutes_stop = ((Value.Hour + (Value.Minute + Value.Duration) / 60) % 24) * 60 + m;
                v.Duration = minutes_stop - minutes_start;

                Value = CheckTimeDuration(v);
            }
        }

        private CalendarTask CheckTimeDuration(CalendarTask v)
        {
            if ((v.Hour * 60 + v.Minute + v.Duration) >= 1440)
            {
                v.Duration = 1440 - v.Hour * 60 - v.Minute;
            }
            if (v.Duration < 0)
            {
                v.Duration = 1;
            }
            else if (v.Duration == 0 && v.Hour == 0 && v.Minute == 0)
            {
                v.Duration = 1440;
            }
            return v;
        }

        public string ExhaustPower
        {
            get => Value.ExhaustPower.ToString();
            set
            {
                var v = Value;
                v.ExhaustPower = int.Parse(value);
                Value = v;
            }
        }

        public string AirflowPower
        {
            get => Value.AirflowPower.ToString();
            set
            {
                var v = Value;
                v.AirflowPower = int.Parse(value);
                Value = v;
            }
        }

        public bool Heater
        {
            get => Value.Heater;
            set
            {
                var v = Value;
                v.Heater = value;
                Value = v;
            }
        }

        public string AirTemperature
        {
            get => Value.AirTemperature.ToString();
            set
            {
                var v = Value;
                v.AirTemperature = int.Parse(value);
                Value = v;
            }
        }

        public bool Boost
        {
            get => Value.Boost;
            set
            {
                var v = Value;
                v.Boost = value;
                Value = v;
            }
        }

        private void ActiveChange(object sender, RoutedEventArgs e)
        {
            Active = !Active;
        }

        private void Button_Minus_Click(object sender, RoutedEventArgs e)
        {
            Delete?.Invoke(Value);
        }

        public Task()
        {
            InitializeComponent();
        }

        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
