using GoodsClassifier.Logic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Convert;

namespace GoodsClassifier.GoodDialog
{
    class Base64ImageSourceConverter : IValueConverter
    {
        /// <param name="parameter">Default image of type <see cref="BitmapImage"/>.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string base64)
            {
                var bytes = FromBase64String(base64);
                using MemoryStream stream = new(bytes);
                return BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
            return parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
