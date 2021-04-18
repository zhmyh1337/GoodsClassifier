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
    class Good : IEditableObject
    {
        [DataGridColumnAutogenerating(Name = "Name")]
        public string Name { get; set; }

        [DataGridColumnAutogenerating(Name = "Vendor code")]
        public string VendorCode { get; set; }

        [DataGridColumnAutogenerating(Name = "Price")]
        public float Price { get; set; }

        [DataGridColumnAutogenerating(Name = "Remaining amount")]
        public uint RemainingAmount { get; set; }

        public string Description { get; set; }

        public Good() => _editableObjectLogic = new(this, (Good)MemberwiseClone());

        public bool IsValid() => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(VendorCode);

        #region IEditableObject

        private readonly EditableObjectDefaultLogic _editableObjectLogic;

        public void BeginEdit() => _editableObjectLogic.BeginEdit();

        public void CancelEdit() => _editableObjectLogic.CancelEdit();

        public void EndEdit() => _editableObjectLogic.EndEdit();

        #endregion

        public string Error
        {
            get
            {
                StringBuilder error = new();

                // iterate over all of the properties
                // of this object - aggregating any validation errors
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(this);
                foreach (PropertyDescriptor prop in props)
                {
                    string propertyError = this[prop.Name];
                    if (propertyError != string.Empty)
                    {
                        error.Append((error.Length != 0 ? ", " : "") + propertyError);
                    }
                }

                return error.Length == 0 ? null : error.ToString();
            }
        }

        public string this[string columnName]
        {
            get
            {
                return columnName switch
                {
                    nameof(Name) when string.IsNullOrEmpty(Name) => "err1",
                    nameof(VendorCode) when string.IsNullOrEmpty(VendorCode) => "err2",
                    _ => string.Empty,
                };
            }
        }
    }

    public class MyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value,
            System.Globalization.CultureInfo cultureInfo)
        {
            var good = (value as BindingGroup).Items[0] as Good;
            if (string.IsNullOrEmpty(good.Name))
            {
                return new ValidationResult(false, "e1");
            }
            else if (string.IsNullOrEmpty(good.VendorCode))
            {
                return new ValidationResult(false, "e2");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
