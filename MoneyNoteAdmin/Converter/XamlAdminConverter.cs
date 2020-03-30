using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace MoneyNoteAdmin.Converter
{
    public class BoolToOpposite : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var returnValue = (bool)value;

            return !returnValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return false;
        }
    }
}
