using GoodsClassifier.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsClassifier.Logic
{
    class Good
    {
        public string Name { get; set; }

        public string VendorCode { get; set; }

        public float Price { get; set; }

        public uint RemainingAmount { get; set; }

        public string Description { get; set; }

        public static Dictionary<string, DataGridColumnsAutogeneratingSettings> PropertyNameToDisplayNameMapping { get; } = new() {
            { nameof(Name), new(true, "Name") },
            { nameof(VendorCode), new(true, "Vendor code") },
            { nameof(Price), new(true, "Price") },
            { nameof(RemainingAmount), new(true, "Remaining amount") },
            { nameof(Description), new(false) },
        };
    }
}
