using GoodsClassifier.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GoodsClassifier.GoodDialog
{
    class ViewModel : DependencyObject
    {
        public Good Good
        {
            get { return (Good)GetValue(GoodProperty); }
            set { SetValue(GoodProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Good. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GoodProperty =
            DependencyProperty.Register("Good", typeof(Good), typeof(ViewModel), new PropertyMetadata(null));

        public Good.CreateModifyViewMode Mode
        {
            get { return (Good.CreateModifyViewMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Mode. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(Good.CreateModifyViewMode), typeof(ViewModel), new PropertyMetadata(Good.CreateModifyViewMode.View));
    }
}
