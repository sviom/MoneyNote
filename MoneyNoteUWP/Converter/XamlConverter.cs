using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using MoneyNoteLibrary.Enums;
using static MoneyNoteLibrary.Enums.MoneyEnum;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Xaml;

namespace MoneyNote.Converter
{
    public class DivisionColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var color = new SolidColorBrush(Colors.Black);
            if (value is MoneyCategory category)
            {
                switch (category)
                {
                    case MoneyCategory.Expense:
                        color = new SolidColorBrush(Colors.Red);
                        break;
                    case MoneyCategory.Income:
                        break;
                    default:
                        break;
                }
            }
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return new SolidColorBrush(Colors.Black);
        }
    }

    public class MoneyDividerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string result = string.Empty;
            if (value is double money)
            {
                result = money.ToString("C0");
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value.ToString();
        }
    }

    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string result = string.Empty;
            if (value is DateTimeOffset dateTime)
            {
                result = dateTime.ToString("yyyy-MM-dd");
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return "";
        }
    }

    public class NullToBoolean : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return true;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return false;
        }
    }

    public class NotNullToBoolean : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
                return true;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return false;
        }
    }



    #region Visibility 관련


    public class StringNotNullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var result = Visibility.Collapsed;
            if (value is string item)
            {
                if (!string.IsNullOrEmpty(item))
                    result = Visibility.Visible;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return Visibility.Collapsed;
        }
    }

    public class NullToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var result = Visibility.Collapsed;
            if (value == null)
                result = Visibility.Visible;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return Visibility.Collapsed;
        }
    }

    public class NotNullToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var result = Visibility.Collapsed;
            if (value != null)
                result = Visibility.Visible;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return Visibility.Collapsed;
        }
    }

    public class CountVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int count)
            {
                if (count > 0)
                    return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return Visibility.Collapsed;
        }
    }

    #endregion
}
