using System.Net.Http.Headers;
using ShorterLink.Code.DataObjects;
using ShorterLink.Code.DataObjects.Subscriptions;

namespace ShorterLink.Code.Subscriptions;

public class UserSubscriptionService : DataObjectService<UserSubscriptionStatusObject>
{
	private ActualUsersSubscriptionsService _actualSubscriptionService;
	private SubscriptionsService _subscriptionService;

	private static SubscriptionPlanObject _freeSubscription;

    public UserSubscriptionService(AppDatabase database, ActualUsersSubscriptionsService subscriptions, SubscriptionsService subscriptionsService) : base(database) {
		_actualSubscriptionService = subscriptions;
		_subscriptionService = subscriptionsService;
	 }

    public SubscriptionPlanObject FreeSubscription {
		get {
			if (_freeSubscription is not null) {
				return _freeSubscription;
			}
			return (_freeSubscription = _subscriptionService[0]) ?? throw new Exception("Heck!.. you have really screwed something up");
		}
	}

	public UserSubscriptionStatusObject? this[ulong userId]
	{
		get { 
			var command = _database.CreatePlainCommand("SELECT * FROM actual_subscriptions us RIGHT JOIN subscription_plans sp on us.subscription_id = sp.id WHERE us.user_id = @userId;");
			command.AddWithValue("@userId", userId);

			using(var reader = command.ExecuteReader()) { 
				if(reader.Read()) {
					return DBToObject(reader);
				}
			}

			return null;
		}
	}

    public override UserSubscriptionStatusObject DBToObject(DatabaseReader reader) {
		var subscriptionObject = _subscriptionService.DBToObject(reader);
		var userSubscription = _actualSubscriptionService.DBToObject(reader);

		return new UserSubscriptionStatusObject {
			Subscription = subscriptionObject,
			UserSubscription = userSubscription
		};
    }
}
