using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GoodsClassifier.Logic
{
    class GoodsSection : TreeViewItem
    {
        public ObservableCollection<Good> Goods { get; } = new();

        public GoodsSection()
        {
            for (int i = 0; i < 5; i++)
            {
                Goods.Add(new(this) { Name = "name", VendorCode = "vendor-1" } );
            }
        }

        public bool ContainsGoods() => Goods.Any();

        public bool ContainsSections() => !Items.IsEmpty;

        public void AddSection()
        {
            Dialog.Dialog dialog = new() { Message = "Enter a name for the new section:" };
            if (dialog.ShowDialog() == true)
            {
                Items.Add(new GoodsSection() { Header = dialog.ResponseText });
                IsExpanded = true;
            }
        }

        public void Rename()
        {
            Dialog.Dialog dialog = new() { Message = "Enter a new section name:", ResponseText = (string)Header };
            if (dialog.ShowDialog() == true)
            {
                Header = dialog.ResponseText;
            }
        }

        public void Delete()
        {
            if ((ContainsGoods() || ContainsSections()) &&
                MessageBox.Show("This section contains other sections or goods. " +
                "Are you sure to delete it?", "Confirmation", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            (Parent as GoodsSection).Items.Remove(this);
        }

        public void AddGood()
        {
            Good newGood = new(this);
            if (newGood.CreateModifyView(Good.CreateModifyViewMode.Create))
            {
                Goods.Add(newGood);
            }
        }
    }
}
