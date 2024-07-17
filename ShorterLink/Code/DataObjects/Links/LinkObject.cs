namespace ShorterLink.Code.DataObjects;

public class LinkObject : DataObject {
	public ulong id { get; set; }
	public string original_url { get; set; }
	public string name { get; set; }
	public string hash_url { get; set; }
	public string mask_url { get; set; }
	public ulong user_id { get; set; }
	public bool active { get; set; }
	public DateTime added_ts { get; set; }
}
