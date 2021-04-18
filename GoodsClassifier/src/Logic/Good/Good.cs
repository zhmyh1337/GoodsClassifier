using GoodsClassifier.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace GoodsClassifier.Logic
{
    class Good
    {
        public enum CreateModifyViewMode
        {
            Create,
            Modify,
            View
        }

        public Good(GoodsSection parentSection) => _parentSection = parentSection;

        [DataGridColumnAutogenerating(Name = "Name")]
        public string Name { get; set; }

        [DataGridColumnAutogenerating(Name = "Vendor code")]
        public string VendorCode { get; set; }

        [DataGridColumnAutogenerating(Name = "Price")]
        public float Price { get; set; }

        [DataGridColumnAutogenerating(Name = "Remaining amount")]
        public uint RemainingAmount { get; set; }

        public string Description { get; set; }

        private readonly GoodsSection _parentSection;

        public bool IsValid() => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(VendorCode);

        public void CreateModifyView(CreateModifyViewMode mode)
        {
            throw new NotImplementedException();
        }

        public void Delete() => _parentSection.Goods.Remove(this);
    }
}
