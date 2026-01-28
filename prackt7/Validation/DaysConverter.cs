using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace prackt7.Validation
{
    class DaysConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return String.Empty;

            if (value is Appointment ap)
            {
                if(ap.Date == null)
                    return "Первый прием";

                if (ap.Date is DateTime dt)
                {
                    int days = (DateTime.Today - dt.Date).Days;
                    return days;
                }


            }


            return String.Empty;


        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

    }
}
