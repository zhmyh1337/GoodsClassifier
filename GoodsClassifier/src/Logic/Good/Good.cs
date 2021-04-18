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

        public static Dictionary<string, string> PropertyNameToDisplayNameMapping { get; } = new() {
            { nameof(Name), "Name" },
            { nameof(VendorCode), "Vendor code" },
            { nameof(Price), "Price" },
            { nameof(RemainingAmount), "Remaining amount" },
            { nameof(Description), "Description" },
        };
    }
}
