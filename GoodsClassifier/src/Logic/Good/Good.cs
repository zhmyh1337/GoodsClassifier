using GoodsClassifier.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace GoodsClassifier.Logic
{
    [Serializable]
    public class Good : INotifyPropertyChanged, IDataErrorInfo
    {
        public enum CreateModifyViewMode
        {
            Create,
            Modify,
            View
        }

        public Good(GoodsSection parentSection) => ParentSection = parentSection;

        [DataGridColumnAutogenerating(Name = "Name")]
        public string Name { get; set; }

        [DataGridColumnAutogenerating(Name = "Code")]
        public string Code { get; set; }

        [DataGridColumnAutogenerating(Name = "Price")]
        public float Price { get; set; }

        [DataGridColumnAutogenerating(Name = "Amount")]
        public uint Amount { get; set; }

        public string Description { get; set; }

        public string ImageBase64
        {
            get => _imageBase64;
            set
            {
                _imageBase64 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageBase64)));
            }
        }

        public GoodsSection ParentSection { get; }

        public string Error => string.Empty;

        public string this[string columnName]
        {
            get => columnName switch
            {
                nameof(Name) when string.IsNullOrWhiteSpace(Name) => "\"Name\" field cannot be empty.",
                nameof(Code) when string.IsNullOrWhiteSpace(Code) => "\"Code\" field cannot be empty.",
                _ => string.Empty
            };
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        private string _imageBase64;

        public bool CreateModifyView(CreateModifyViewMode mode)
        {
            switch (mode)
            {
                case CreateModifyViewMode.Create:
                case CreateModifyViewMode.View:
                    return new GoodDialog.GoodDialog() { Good = this, Mode = mode }.ShowDialog() == true;

                case CreateModifyViewMode.Modify:
                    // Doing all the changes in the temporary copy so that
                    // the user can use "Cancel" button. If the user clicked "OK"
                    // and all checks were passed, apply the changes.
                    var mutableCopy = (Good)MemberwiseClone();
                    bool dialogResult = new GoodDialog.GoodDialog() { Good = mutableCopy, Mode = mode }.ShowDialog() == true;
                    if (dialogResult)
                    {
                        ObjectCopyProperties.Copy(mutableCopy, this);
                        // All properties might have been changed.
                        PropertyChanged?.Invoke(this, new(null));
                    }
                    return dialogResult;

                default:
                    throw new ArgumentException(null, nameof(mode));
            }
        }

        public void Delete() => ParentSection.Goods.Remove(this);
    }
}
