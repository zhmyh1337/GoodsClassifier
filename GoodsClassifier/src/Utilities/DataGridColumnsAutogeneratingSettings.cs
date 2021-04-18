using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsClassifier.Utilities
{
    class DataGridColumnsAutogeneratingSettings
    {
        public DataGridColumnsAutogeneratingSettings(bool doGenerate, string displayName = null) =>
            (DoGenerate, DisplayName) = (doGenerate, displayName);

        public bool DoGenerate { get; set; }

        public string DisplayName { get; set; }
    }
}
