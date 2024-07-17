namespace ShorterLink.Code.DataObjects;

public class SubscriptionPlanObject : DataObject {
	public ulong id { get; set; }
	public string title { get; set; }
	public string description { get; set; }
	public uint period { get; set; }
	public int group { get; set; }
	/// <summary>
	/// I don't think it's neccessary to use double either double precision or decimal
	/// as we don't perform any accounting calculations with the column
	/// </summary>
	public float price { get; set; }
	public uint links_per_month { get; set; }
	public bool alias_edition { get; set; }
}
