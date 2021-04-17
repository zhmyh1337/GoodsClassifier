using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GoodsClassifier.MainWindow
{
    class GoodsSection : TreeViewItem
    {
        public bool ContainsGoods() => false;

        public bool ContainsSections() => !Items.IsEmpty;
    }
}
