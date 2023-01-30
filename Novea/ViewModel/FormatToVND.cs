using System;
using System.Globalization;
using System.Windows.Data;

namespace Novea.ViewModel
{
    public class FormatToVND : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal money)
            {
                return string.Format(culture, "{0:0,0}", money) + " VNĐ";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
