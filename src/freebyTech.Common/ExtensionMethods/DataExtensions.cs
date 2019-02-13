using System.Collections.Generic;
using System.Linq;
using System.Text;
using freebyTech.Common.CommandLine;
using freebyTech.Common.Data.Interfaces;

namespace freebyTech.Common.ExtensionMethods
{
    public static class DataExtensions
    {
        public static bool UpdateIfEdited(this IEditableModel model, string userName)
        {
            if(model.IsNew) {
                model.CreatedBy = userName;
                model.CreatedOn = System.DateTime.Now;
                return true;
            }
            else if(model.IsDirty) {
                model.ModifiedBy = userName;
                model.ModifiedOn = System.DateTime.Now;
                return true;
            }
            return false;
        }
    }
}