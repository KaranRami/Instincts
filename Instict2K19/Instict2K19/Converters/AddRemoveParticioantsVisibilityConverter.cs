using System;
using System.Globalization;
using Xamarin.Forms;

namespace Instict2K19.Converters
{
    public class AddRemoveParticioantsVisibilityConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return true;
            if (int.Parse(value.ToString()) == 1)
                return false;
            else
                return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
