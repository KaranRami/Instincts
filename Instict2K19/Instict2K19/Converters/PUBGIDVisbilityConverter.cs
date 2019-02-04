using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Instict2K19.DataModel;
using Xamarin.Forms;

namespace Instict2K19.Converters
{
    class PUBGIDVisbilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
            if (value is Event && (value as Event).Name.ToString().ToUpper().Contains("PUBG"))
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
