using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GoodsClassifier.Logic
{
    class GoodsSectionContextMenu : ContextMenu
    {
        public GoodsSectionContextMenu(GoodsSection section, bool isRootItem)
        {
            MenuItem newSection = new() { Header = "New section", Tag = section };
            newSection.Click += (sender, e) => RoutedEventHandlerWrapperForGoodsSection(sender, (section) => section.AddSection());

            MenuItem renameSection = new() { Header = "Rename section", Tag = section, IsEnabled = !isRootItem };
            renameSection.Click += (sender, e) => RoutedEventHandlerWrapperForGoodsSection(sender, (section) => section.Rename());

            MenuItem deleteSection = new() { Header = "Delete section", Tag = section, IsEnabled = !isRootItem };
            deleteSection.Click += (sender, e) => RoutedEventHandlerWrapperForGoodsSection(sender, (section) => section.Delete());

            MenuItem newGood = new() { Header = "New good", Tag = section };
            newGood.Click += (sender, e) => RoutedEventHandlerWrapperForGoodsSection(sender, (section) => section.AddGood());

            Control[] contextMenuItemsCollection = { newSection, renameSection, deleteSection, new Separator(), newGood };
            ItemsSource = contextMenuItemsCollection;
        }
        public void Show() => IsOpen = true;

        private static void RoutedEventHandlerWrapperForGoodsSection(object sender, Action<GoodsSection> methodToInvoke) =>
            methodToInvoke.Invoke((GoodsSection)(sender as MenuItem).Tag);
    }
}
