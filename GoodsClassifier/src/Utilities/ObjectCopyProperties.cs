using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsClassifier.Utilities
{
    static class ObjectCopyProperties
    {
        public static void Copy(object source, object dest)
        {
            var sourceProps = source.GetType().GetProperties().Where(x => x.CanRead).ToList();
            var destProps = dest.GetType().GetProperties().Where(x => x.CanWrite).ToList();

            foreach (var sourceProp in sourceProps)
            {
                destProps.FirstOrDefault(x => x.Name == sourceProp.Name)
                    ?.SetValue(dest, sourceProp.GetValue(source, null), null);
            }
        }
    }
}
