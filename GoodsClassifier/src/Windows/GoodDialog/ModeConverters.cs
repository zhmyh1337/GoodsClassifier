using GoodsClassifier.Logic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace GoodsClassifier.GoodDialog
{
    class ModeToTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                Good.CreateModifyViewMode.Create => "Good creation",
                Good.CreateModifyViewMode.Modify => "Good modifying",
                Good.CreateModifyViewMode.View => "Good overview",
                _ => throw new ArgumentException(null, nameof(value)),
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    class ModeToControlIsEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                Good.CreateModifyViewMode.Create => true,
                Good.CreateModifyViewMode.Modify => true,
                Good.CreateModifyViewMode.View => false,
                _ => throw new ArgumentException(null, nameof(value)),
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    class ModeToTextBoxReadOnlyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                Good.CreateModifyViewMode.Create => false,
                Good.CreateModifyViewMode.Modify => false,
                Good.CreateModifyViewMode.View => true,
                _ => throw new ArgumentException(null, nameof(value)),
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
