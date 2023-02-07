using System;
using System.Collections.Generic;
using System.Text;

namespace freebyTech.Common.Data.Interfaces
{
  /// <summary>
  /// Interface represents all the properties that standard editable data times should implement within freebyTech systems.
  /// </summary>
  public interface IEditableResource
  {
    byte[] Ts { get; set; }

    // This properties are added after the fact by scaffolding and are used as non entity framework specific state information
    // used by the client UI for change tracking.
    bool IsNew { get; set; }
    bool IsDirty { get; set; }
    bool IsDeleted { get; set; }
  }
}
