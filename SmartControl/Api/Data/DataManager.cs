﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SmartControl.Api.Data
{
    public class DataManager : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;




        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
