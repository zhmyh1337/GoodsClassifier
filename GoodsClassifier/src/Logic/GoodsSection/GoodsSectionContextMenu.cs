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
            MenuItem newSection = new() { Header = "New section" };
            newSection.Click += (_, _) => section.AddSubsection();

            MenuItem renameSection = new() { Header = "Rename section", IsEnabled = !isRootItem };
            renameSection.Click += (_, _) => section.Rename();

            MenuItem deleteSection = new() { Header = "Delete section", IsEnabled = !isRootItem };
            deleteSection.Click += (_, _) => section.Delete();

            MenuItem newGood = new() { Header = "New good" };
            newGood.Click += (_, _) => section.AddGood();

            MenuItem expandAll = new() { Header = "Expand all" };
            expandAll.Click += (_, _) => section.ExpandAll();

            Control[] contextMenuItemsCollection = { newSection, renameSection, deleteSection, new Separator(), newGood, new Separator(), expandAll };
            ItemsSource = contextMenuItemsCollection;
        }

        public void Show() => IsOpen = true;
    }
}
