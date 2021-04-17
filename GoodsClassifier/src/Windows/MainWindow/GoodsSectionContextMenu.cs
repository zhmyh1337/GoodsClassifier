using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GoodsClassifier.MainWindow
{
    class GoodsSectionContextMenu : ContextMenu
    {
        public GoodsSectionContextMenu(GoodsSection section, bool isRootItem)
        {
            MenuItem newSection = new() { Header = "New section", Tag = section };
            newSection.Click += (sender, e) => RoutedEventHandlerWrapperForGoodsSection(
                sender, NewSection_Click);

            MenuItem renameSection = new() { Header = "Rename section", Tag = section,
                IsEnabled = !isRootItem };
            renameSection.Click += (sender, e) => RoutedEventHandlerWrapperForGoodsSection(
                sender, RenameSection_Click);

            MenuItem deleteSection = new() { Header = "Delete section", Tag = section,
                IsEnabled = !isRootItem };
            deleteSection.Click += (sender, e) => RoutedEventHandlerWrapperForGoodsSection(
                sender, DeleteSection_Click);

            MenuItem newItem = new() { Header = "New item", Tag = section };
            newItem.Click += (sender, e) => RoutedEventHandlerWrapperForGoodsSection(
                sender, NewItem_Click);

            Control[] contextMenuItemsCollection = { newSection, renameSection, deleteSection,
                new Separator(), newItem };
            ItemsSource = contextMenuItemsCollection;
        }

        private static void RoutedEventHandlerWrapperForGoodsSection(object sender,
            Action<GoodsSection> methodToInvoke)
        {
            methodToInvoke.Invoke((GoodsSection)(sender as MenuItem).Tag);
        }

        private void NewSection_Click(GoodsSection section)
        {
            throw new NotImplementedException();
        }

        private void RenameSection_Click(GoodsSection section)
        {
            throw new NotImplementedException();
        }

        private void DeleteSection_Click(GoodsSection section)
        {
            if ((section.ContainsGoods() || section.ContainsSections()) &&
                MessageBox.Show("This section contains other sections or goods. " +
                "Are you sure to delete it?", "Confirmation", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            (section.Parent as GoodsSection).Items.Remove(section);
        }

        private void NewItem_Click(GoodsSection section)
        {
            throw new NotImplementedException();
        }

        public void Show() => IsOpen = true;
    }
}
