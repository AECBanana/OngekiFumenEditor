﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace OngekiFumenEditor.Modules.AudioPlayerToolViewer.Converters
{
    public class IntToTimeDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var timeSpan = (TimeSpan)(value is float f ? TimeSpan.FromMilliseconds(f) : value);
            return $"{timeSpan.Minutes}:{timeSpan.Seconds}.{timeSpan.Milliseconds}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
