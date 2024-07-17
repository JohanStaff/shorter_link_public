using ShorterLink.Code.DataObjects;
using ShorterLink.Code.Events;

namespace ShorterLink.Code.Subscriptions.Logic;

public class InitialSubscriptionLogic : ISubscriptionLogic { 
	private UserSubscriptionService _userSubscriptions;
	private UserSession _userSession;
	private EventsService _events;

	public InitialSubscriptionLogic(UserSubscriptionService userSubscription, UserSession userSession, EventsService eventsService)  {
		_userSubscriptions = userSubscription;
		_userSession = userSession;
		_events = eventsService;
	}

    public bool IsAllowed(UserActionCode action) {
		var user = _userSession.User;
		switch(action) {
			case UserActionCode.LinkAddition:
				if(_userSession.IsAnonymous) {
					return _events[_userSession.DeviceId, days: 31].Count() <= _userSubscriptions.FreeSubscription.links_per_month;
				}


				return _events.GetEventsByUserIdOrDeviceId(user.id, user.device_id, 31).Count() <= (_userSubscriptions[user.id]?.Subscription ?? _userSubscriptions.FreeSubscription).links_per_month;
			case UserActionCode.LinkAddAlias:
				if(_userSession.IsAnonymous) {
					return false;
				}

				return _userSubscriptions[user.id]?.Subscription.alias_edition ?? _userSubscriptions.FreeSubscription.alias_edition;
		}
		
		return true;
    }
}
