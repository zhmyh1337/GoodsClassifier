using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsClassifier.Utilities
{
    class EditableObjectDefaultLogic : IEditableObject
    {
        private readonly object _targetObject;
        private readonly object _backupCopy;
        private bool _inEdit;

        public EditableObjectDefaultLogic(object obj, object objCopy) => (_targetObject, _backupCopy) = (obj, objCopy);

        public void BeginEdit()
        {
            if (_inEdit)
                return;
            _inEdit = true;
            _targetObject.CopyPropertiesTo(_backupCopy);
        }

        public void CancelEdit()
        {
            if (!_inEdit)
                return;
            _inEdit = false;
            _backupCopy.CopyPropertiesTo(_targetObject);
        }

        public void EndEdit()
        {
            if (!_inEdit)
                return;
            _inEdit = false;
        }
    }
}
