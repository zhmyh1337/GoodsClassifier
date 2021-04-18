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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GoodsClassifier.MainWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Tree_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var section = VisualUpwardSearch(e.OriginalSource as DependencyObject);
            if (section == null)
                return;

            new GoodsSectionContextMenu(section, section == TreeRoot).Show();
        }

        private static GoodsSection VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && !(source is GoodsSection))
                source = VisualTreeHelper.GetParent(source);

            return source as GoodsSection;
        }

        private void Tree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.OriginalSource is GoodsSection section)
            {
                switch (e.Key)
                {
                    case Key.Delete when section != TreeRoot:
                        section.Delete();
                        break;
                }
            }
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            e.Column.Header = Good.PropertyNameToDisplayNameMapping[e.PropertyName];
        }
    }
}
