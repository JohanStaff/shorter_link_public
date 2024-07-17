namespace ShorterLink.Code.DataObjects.Subscriptions;

public class ActualSubscriptionObject : DataObject {
	public ulong user_id { get; set; }
	public ulong subscription_id { get; set; }
	public DateTime updated_ts { get; set; }
	public DateTime expires_ts { get; set; }
}
