using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFA.Utility.Converter
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = DateTime.Parse((string)value);
            var difDate = DateTime.Now - date;
            if (difDate.TotalDays < 1)
            {
                if (difDate.TotalHours < 1)
                {
                    if (difDate.TotalMinutes < 1)
                        return "now";
                    return difDate.Minutes.ToString() + "m";
                }
                return difDate.Hours.ToString() + "h";
            }
            return difDate.Days.ToString() + "d";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
