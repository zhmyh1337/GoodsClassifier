using GoodsClassifier.Logic;
using GoodsClassifier.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GoodsClassifier.MainWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Tree_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var section = VisualUpwardSearch<GoodsSection>(e.OriginalSource as DependencyObject);
            if (section == null)
                return;

            new GoodsSectionContextMenu(section, section == TreeRoot).Show();
        }

        private static T VisualUpwardSearch<T>(DependencyObject source) where T : class
        {
            while (source != null && !(source is T))
                source = VisualTreeHelper.GetParent(source);

            return source as T;
        }

        private void Tree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.OriginalSource is GoodsSection section)
            {
                switch (e.Key)
                {
                    case Key.Delete when section != TreeRoot:
                        section.Delete();
                        break;
                }
            }
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            var columnAutogeneratingAttribute = propertyDescriptor.Attributes.OfType<DataGridColumnAutogeneratingAttribute>().FirstOrDefault();
            if (columnAutogeneratingAttribute != null)
            {
                e.Column.Header = columnAutogeneratingAttribute.Name;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void DataGrid_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Click on a row.
            if (VisualUpwardSearch<DataGridRow>(e.OriginalSource as DependencyObject)?.DataContext is Good good)
            {
                new GoodContextMenu(good).Show();
            }
            // Click on empty space.
            else if (Tree.SelectedItem is GoodsSection section)
            {
                MenuItem newGood = new() { Header = "New good" };
                newGood.Click += (_, _) => section.AddGood();
                _ = new ContextMenu() { ItemsSource = new[] { newGood }, IsOpen = true };
            }
        }
    }
}
