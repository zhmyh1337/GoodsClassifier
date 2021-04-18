using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsClassifier.Utilities
{
    [AttributeUsage(AttributeTargets.Property)]
    class DataGridColumnAutogeneratingAttribute : Attribute
    {
        public string Name { get; set; }
    }
}
