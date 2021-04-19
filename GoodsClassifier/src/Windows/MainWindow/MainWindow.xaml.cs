using CsvHelper;
using GoodsClassifier.Logic;
using GoodsClassifier.Utilities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;

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
            if (VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject)?.DataContext is not GoodsSection section)
                return;

            new GoodsSectionContextMenu(section, section == ViewModel.TreeRoot).Show();
        }

        private static T VisualUpwardSearch<T>(DependencyObject source) where T : class
        {
            while (source != null && !(source is T))
                source = VisualTreeHelper.GetParent(source);

            return source as T;
        }

        private void Tree_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.OriginalSource as TreeViewItem)?.DataContext is GoodsSection section)
            {
                switch (e.Key)
                {
                    case Key.Delete when section != ViewModel.TreeRoot:
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

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Click on a row.
            if (VisualUpwardSearch<DataGridRow>(e.OriginalSource as DependencyObject)?.DataContext is Good good)
            {
                good.CreateModifyView(Good.CreateModifyViewMode.View);
            }
        }

        private void MenuItemNew_Click(object sender, RoutedEventArgs e) => ViewModel.TreeRoot.Clear();

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new() { Filter = "Goods classifier data (*.gcd)|*.gcd|All files (*.*)|*.*", FileName = "data" };
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    using FileStream fs = new(dialog.FileName, FileMode.OpenOrCreate);
                    BinaryFormatter formatter = new();
                    ViewModel.TreeRoot = (GoodsSection)formatter.Deserialize(fs);
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to load data from file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void MenuItemSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new() { Filter = "Goods classifier data (*.gcd)|*.gcd|All files (*.*)|*.*", FileName = "data" };
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    using FileStream fs = new(dialog.FileName, FileMode.OpenOrCreate);
                    BinaryFormatter formatter = new();
                    formatter.Serialize(fs, ViewModel.TreeRoot);
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to save data to file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void MenuItemCsvRunningLowOn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new() { Filter = "Csv files (*.csv)|*.csv|All files (*.*)|*.*", FileName = "data" };

            if (dialog.ShowDialog() == true)
            {
                var records = from good in ViewModel.TreeRoot.GoodsAllSubtree
                              where good.Amount < ViewModel.LowAmountSetting
                              select new
                              {
                                  good.ParentSection.Path,
                                  good.Code,
                                  good.Name,
                                  good.Amount
                              };

                try
                {
                    using StreamWriter writer = new(dialog.FileName);
                    using CsvWriter csv = new(writer, CultureInfo.InvariantCulture);
                    csv.WriteRecords(records);
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to save CSV file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
