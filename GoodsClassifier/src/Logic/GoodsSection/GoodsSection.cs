using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace GoodsClassifier.Logic
{
    [Serializable]
    public class GoodsSection : INotifyPropertyChanged
    {
        public string Header
        {
            get => _header;
            set
            {
                _header = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Header)));
            }
        }

        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                _isExpanded = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsExpanded)));
            }
        }

        public ObservableCollection<GoodsSection> Subsections { get; } = new();

        public ObservableCollection<Good> Goods { get; } = new();

        public GoodsSection Parent { get; init; }

        public string Path
        {
            get
            {
                var currentSection = this;
                List<string> headersInversedOrder = new();
                while (currentSection != null)
                {
                    headersInversedOrder.Add(currentSection.Header);
                    currentSection = currentSection.Parent;
                }
                return string.Join("/", headersInversedOrder.Reverse<string>());
            }
        }

        public IEnumerable<GoodsSection> SectionsAllSubtree
        {
            // 1-line DFS :)
            get => new[] { this }.Concat(Subsections.SelectMany(x => x.SectionsAllSubtree));
        }

        public IEnumerable<Good> GoodsAllSubtree => SectionsAllSubtree.SelectMany(x => x.Goods);

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        [NonSerialized]
        private bool _isExpanded;

        private string _header;

        public bool ContainsSubsections() => Subsections.Any();

        public bool ContainsGoods() => Goods.Any();

        public void AddSubsection()
        {
            Dialog.Dialog dialog = new() { Caption = "New section", Message = "Enter a name for the new section:" };
            if (dialog.ShowDialog() == true)
            {
                AddSubsection(dialog.ResponseText);
            }
        }

        public void AddSubsection(string name)
        {
            Subsections.Add(new GoodsSection() { Parent = this, Header = name });
            IsExpanded = true;
        }

        public void Rename()
        {
            Dialog.Dialog dialog = new() { Caption = "Renaming", Message = "Enter a new section name:", ResponseText = (string)Header };
            if (dialog.ShowDialog() == true)
            {
                Header = dialog.ResponseText;
            }
        }

        public void Delete()
        {
            if ((ContainsGoods() || ContainsSubsections()) &&
                MessageBox.Show("This section contains other sections or goods. " +
                "Are you sure to delete it?", "Confirmation", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            Parent.Subsections.Remove(this);
        }

        public void AddGood()
        {
            Good newGood = new(this);
            if (newGood.CreateModifyView(Good.CreateModifyViewMode.Create))
            {
                Goods.Add(newGood);
            }
        }

        public void Clear()
        {
            Subsections.Clear();
            Goods.Clear();
        }
    }
}
