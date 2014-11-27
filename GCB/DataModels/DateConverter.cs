using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization.DateTimeFormatting;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GCB
{
    public class DateConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value", "Wartość nie może być pusta!");
            }
            if(!typeof(DateTime).Equals(value.GetType()))
            {
                throw new ArgumentException("Wartość musi być typu DateTime", "value");
            }
            DateTime date = (DateTime)value;
            if (parameter == null)
            {
                return DateTimeFormatter.ShortDate.Format(date);
            }
            else if ((string)parameter == "date")
            {
                DateTimeFormatter dateFormatter = new DateTimeFormatter("{day.integer(2)}-{month.integer(2)}-{year.full}");
                return dateFormatter.Format(date);
            }
            else
            {
                return date.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            string strValue = value as string;
            DateTime resultDateTime;
            if (DateTime.TryParse(strValue, out resultDateTime))
            {
                return resultDateTime;
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
