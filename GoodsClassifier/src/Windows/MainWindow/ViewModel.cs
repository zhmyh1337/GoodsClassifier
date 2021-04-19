using GoodsClassifier.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GoodsClassifier.MainWindow
{
    class ViewModel : DependencyObject
    {
        public GoodsSection TreeRoot
        {
            get { return (GoodsSection)GetValue(TreeRootProperty); }
            set { SetValue(TreeRootProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TreeRoot. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TreeRootProperty = DependencyProperty.Register("TreeRoot",
            typeof(GoodsSection), typeof(ViewModel), new PropertyMetadata(new GoodsSection() { Header = "Root" }));
    }
}
