namespace ShorterLink.Code.DataObjects.Links;

public class LinkOverallObject : DataObject {
	public LinkObject link { get; set; }
	public LinkStatsObject stats { get; set; }
}
