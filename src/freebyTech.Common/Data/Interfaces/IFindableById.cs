using System;
using System.Collections.Generic;
using System.Text;

namespace freebyTech.Common.Data.Interfaces
{
  public interface IFindableById<TIDType>
  {
    TIDType Id { get; set; }
  }
}
