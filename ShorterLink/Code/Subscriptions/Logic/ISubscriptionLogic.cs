using ShorterLink.Code.DataObjects;

namespace ShorterLink.Code.Subscriptions.Logic;

public interface ISubscriptionLogic {
	bool IsAllowed(UserActionCode action);
}
