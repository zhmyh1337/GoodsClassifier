using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GoodsClassifier.Logic
{
    class GoodsSection : TreeViewItem
    {
        public List<Good> Goods = new();

        public bool ContainsGoods() => Goods.Any();

        public bool ContainsSections() => !Items.IsEmpty;

        public void AddSection()
        {
            
        }

        public void Rename()
        {
            Dialog.Dialog dialog = new() { Message = "Input new section name:" };
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
            throw new NotImplementedException();
        }
    }
}
