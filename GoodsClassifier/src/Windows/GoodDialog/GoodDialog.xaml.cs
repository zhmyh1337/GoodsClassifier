using GoodsClassifier.Logic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GoodsClassifier.GoodDialog
{
    /// <summary>
    /// Interaction logic for GoodDialog.xaml
    /// </summary>
    partial class GoodDialog : Window
    {
        public GoodDialog()
        {
            InitializeComponent();
        }

        public Good Good
        {
            get => ViewModel.Good;
            init => ViewModel.Good = value;
        }

        public Good.CreateModifyViewMode Mode
        {
            get => ViewModel.Mode;
            init => ViewModel.Mode = value;
        }

        private void Image_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog dialog = new()
            {
                Filter = "PNG files (*.png)|*.png|JPG files (*.jpg)|*.jpg|JPEG files (*.jpeg)|*.jpeg|GIF files (*.gif)|*.gif|All files (*.*)|*.*"
            };
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    BitmapImage bitmapImage = new(new Uri(dialog.FileName));
                    PngBitmapEncoder encoder = new();
                    var frame = BitmapFrame.Create(bitmapImage);
                    encoder.Frames.Add(frame);
                    using MemoryStream stream = new();
                    encoder.Save(stream);
                    Good.ImageBase64 = Convert.ToBase64String(stream.ToArray());
                }
                catch (Exception)
                {
                    MessageBox.Show("An error occurred loading the image.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            NameTextBox.Style = (Style)Resources["TextBoxWithErrorBackground"];
            CodeTextBox.Style = (Style)Resources["TextBoxWithErrorBackground"];

            // Converting to array because System.Linq.Enumerable.Concat2Iterator
            // is unstable here for some reason.
            var allErrors = GetAllValidationErrors(this).ToArray();
            if (allErrors.Any())
            {
                var firstError = allErrors.First();
                MessageBox.Show((string)firstError.ErrorContent, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ((firstError.BindingInError as BindingExpression).Target as UIElement).Focus();
                return;
            }

            DialogResult = true;
        }

        private static IEnumerable<ValidationError> GetAllValidationErrors(DependencyObject obj)
        {
            // 2-line DFS :)
            return Validation.GetErrors(obj).Concat(LogicalTreeHelper.GetChildren(obj)
                .OfType<DependencyObject>().SelectMany(x => GetAllValidationErrors(x)));
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
