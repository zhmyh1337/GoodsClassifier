using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GoodsClassifier.Dialog
{
    class ViewModel : DependencyObject
    {
        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Caption. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption", typeof(string), typeof(ViewModel), new PropertyMetadata("Dialog"));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Message. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(ViewModel), new PropertyMetadata("Input here:"));

        public string ResponseText
        {
            get { return (string)GetValue(ResponseTextProperty); }
            set { SetValue(ResponseTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ResponseText. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResponseTextProperty =
            DependencyProperty.Register("ResponseText", typeof(string), typeof(ViewModel), new PropertyMetadata(""));
    }
}
