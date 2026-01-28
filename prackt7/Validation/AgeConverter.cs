using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace prackt7.Validation
{
    class AgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return String.Empty;
            if(value is DateTime dt)
            {
                DateTime today = DateTime.Today;
                int age = today.Year - dt.Year;

                if (age < 18)
                {
                    return "Несовершеннолетний(ая)";
                }
                else
                {
                    return "Совершеннолетний(ая)";
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
