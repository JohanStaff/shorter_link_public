namespace ShorterLink.Code.DataObjects.Subscriptions;

public class UserSubscriptionStatusObject : DataObject {
	public SubscriptionPlanObject Subscription { get; set; }
	public ActualSubscriptionObject UserSubscription { get; set; }
}
