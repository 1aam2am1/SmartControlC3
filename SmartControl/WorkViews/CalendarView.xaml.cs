﻿using SmartControl.Api;
using Api.Data;
using SmartControl.Api.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
    /// Interaction logic for CalendarView.xaml
    /// </summary>
    public partial class CalendarView : UserControl
    {
        private readonly IClient client;

        public ObservableCollection<CalendarTask> Monday
        {
            get => client.GetDataManager().Task[(int)DayOfWeek.Monday];
        }

        public class DayCollection
        {
            public DayCollection(string _name, ObservableCollection<CalendarTask> t)
            {
                Name = _name;
                Tasks = t;

                D = (o) =>
                {
                    BindingOperations.AccessCollection(Tasks, () =>
                    {
                        Tasks.Remove(o);
                    }, true);
                };
            }
            public string Name { get; private set; }
            public ObservableCollection<CalendarTask> Tasks { get; private set; }

            public Action<CalendarTask> D { get; }

            private ICommand _A;
            public ICommand A
            {
                get
                {
                    if (_A == null)
                    {
                        _A = new RelayCommand((_) =>
                        {
                            BindingOperations.AccessCollection(Tasks, () =>
                            {
                                if (Tasks.Count < 5)
                                    Tasks.Add(new CalendarTask());
                            }, true);
                        }
                        );
                    }
                    return _A;
                }
            }
        }

        private IEnumerable<DayCollection> _Tasks
        {
            get
            {
                yield return new DayCollection("Poniedziałek", client.GetDataManager().Task[(int)DayOfWeek.Monday]);
                yield return new DayCollection("Wtorek", client.GetDataManager().Task[(int)DayOfWeek.Tuesday]);
                yield return new DayCollection("Środa", client.GetDataManager().Task[(int)DayOfWeek.Wednesday]);
                yield return new DayCollection("Czwartek", client.GetDataManager().Task[(int)DayOfWeek.Thursday]);
                yield return new DayCollection("Piątek", client.GetDataManager().Task[(int)DayOfWeek.Friday]);
                yield return new DayCollection("Sobota", client.GetDataManager().Task[(int)DayOfWeek.Saturday]);
                yield return new DayCollection("Niedziela", client.GetDataManager().Task[(int)DayOfWeek.Sunday]);
            }
        }

        private IEnumerable<DayCollection> T;
        public IEnumerable<DayCollection> Tasks
        {
            get => T ?? (T = _Tasks);
        }

        public CalendarView(IClient c)
        {
            InitializeComponent();

            client = c;
        }
    }
}
