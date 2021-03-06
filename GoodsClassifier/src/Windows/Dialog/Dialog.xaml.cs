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

namespace GoodsClassifier.Dialog
{
    /// <summary>
    /// Interaction logic for Dialog.xaml
    /// </summary>
    partial class Dialog : Window
    {
        public Dialog()
        {
            InitializeComponent();
        }

        /// Did not figure out how to be able to use bound dp properties
        /// as properties in this class without losing the designer preview
        /// functionality, so I'm just doing this wrapping.
        
        public string Caption
        {
            get => ViewModel.Caption;
            set => ViewModel.Caption = value;
        }

        public string Message
        {
            get => ViewModel.Message;
            set => ViewModel.Message = value;
        }

        public string ResponseText
        {
            get => ViewModel.ResponseText;
            set => ViewModel.ResponseText = value;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e) => DialogResult = true;

        private void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            textBox.SelectAll();
            textBox.Focus();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DialogResult = true;
            }
        }
    }
}
