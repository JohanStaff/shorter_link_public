using ShorterLink.Code.DataObjects;
using ShorterLink.Code.DataObjects.Subscriptions;

namespace ShorterLink.Code.Subscriptions;

public class ActualUsersSubscriptionsService : DataObjectService<ActualSubscriptionObject>
{
    public ActualUsersSubscriptionsService(AppDatabase database) : base(database) { }

    public override ActualSubscriptionObject DBToObject(DatabaseReader reader) {
		return new ActualSubscriptionObject { 
			user_id = (ulong)reader["user_id"],
			subscription_id = (ulong)reader["subscription_id"],
			expires_ts = (DateTime)reader["expires_ts"],
			updated_ts = (DateTime)reader["updated_ts"]
		};
    }
}
