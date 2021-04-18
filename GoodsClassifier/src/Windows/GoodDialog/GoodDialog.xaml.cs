using GoodsClassifier.Logic;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace GoodsClassifier.GoodDialog
{
    /// <summary>
    /// Interaction logic for GoodDialog.xaml
    /// </summary>
    partial class GoodDialog : Window
    {
        public GoodDialog()
        {
            InitializeComponent();
        }

        public Good Good
        {
            get => ViewModel.Good;
            init => ViewModel.Good = value;
        }

        public Good.CreateModifyViewMode Mode
        {
            get => ViewModel.Mode;
            init => ViewModel.Mode = value;
        }
    }
}
