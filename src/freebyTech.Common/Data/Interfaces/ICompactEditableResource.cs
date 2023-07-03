namespace freebyTech.Common.Data.Interfaces
{
  public interface ICompactEditableResource : IEditableResource
  {
    /// <summary>
    /// The ticks for the last update time, whether that comes from CreatedOn or ModifiedOn.
    /// </summary>
    long Lut { get; set; }
  }
}
