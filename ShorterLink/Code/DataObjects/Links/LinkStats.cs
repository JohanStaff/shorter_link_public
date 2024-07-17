namespace ShorterLink.Code.DataObjects;

public class LinkStatsObject : DataObject {
	public ulong? link_id { get; set; }
	public uint? visits { get; set; }
}
