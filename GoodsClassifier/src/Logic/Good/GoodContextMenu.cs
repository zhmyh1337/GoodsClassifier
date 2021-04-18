using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GoodsClassifier.Logic
{
    class GoodContextMenu : ContextMenu
    {
        public GoodContextMenu(Good good)
        {
            MenuItem viewGood = new() { Header = "View good" };
            viewGood.Click += (_, _) => good.CreateModifyView(Good.CreateModifyViewMode.View);

            MenuItem modifyGood = new() { Header = "Modify good" };
            modifyGood.Click += (_, _) => good.CreateModifyView(Good.CreateModifyViewMode.Modify);

            MenuItem deleteGood = new() { Header = "Delete good" };
            deleteGood.Click += (_, _) => good.Delete();

            Control[] contextMenuItemsCollection = { viewGood, modifyGood, deleteGood };
            ItemsSource = contextMenuItemsCollection;
        }

        public void Show() => IsOpen = true;
    }
}
