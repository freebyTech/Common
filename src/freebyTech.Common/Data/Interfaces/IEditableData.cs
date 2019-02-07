using System;
using System.Collections.Generic;
using System.Text;

namespace freebyTech.Common.Data.Interfaces
{
    /// <summary>
    /// Interface represents all the properties that standard editable data times should implement within freebyTech systems.
    /// </summary>
    public interface IEditableData
    {
        DateTime CreatedOn { get; set; }
        string CreatedBy { get; set; }
        DateTime? ModifiedOn { get; set; }
        string ModifiedBy { get; set; }
        byte[] Ts { get; set; }
    }
}
