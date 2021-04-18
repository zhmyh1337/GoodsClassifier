﻿using GoodsClassifier.Logic;
using GoodsClassifier.Utilities;
using System;
using System.Collections.Generic;
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
    public partial class MainWindow : Window
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
                var textColumn = e.Column as DataGridTextColumn;
                //(textColumn.Binding as Binding).ValidatesOnExceptions = true;
                //(textColumn.Binding as Binding).ValidatesOnDataErrors = true;
                textColumn.EditingElementStyle = (Style)(sender as DataGrid).FindResource("errorStyle");
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void DataGrid_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (VisualUpwardSearch<DataGridRow>(e.OriginalSource as DependencyObject)?.DataContext is Good good)
            {

            }
        }

        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //e.Cancel = true;
            //if (!(e.Row.DataContext as Good).IsValid())
            //{
            //    e.Cancel = true;
            //    e.EditAction = DataGridEditAction.Cancel;
            //}
        }
    }
}
