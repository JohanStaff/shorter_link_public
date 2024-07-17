using Microsoft.AspNetCore.Mvc;
using ShorterLink.Code.DataObjects;
using ShorterLink.Code.Events;
using ShorterLink.Code.Links;
using ShorterLink.Code.Links.LinkStats;
using ShorterLink.Code.Subscriptions.Logic;

namespace ShorterLink;

public class ShorterController : Controller {
    private readonly ILogger<ShorterController> _logger;
    private readonly EventsService _events;
    private readonly LinksService _linkService; 
    private readonly ISubscriptionLogic _subscriptionLogic;
    private readonly UserSession _userSession;

    public ShorterController(ILogger<ShorterController> logger, UserSession session, LinksService linksService, ISubscriptionLogic subscriptionLogic, EventsService eventsService)
    {
        _logger = logger;
        _userSession = session;
        _linkService = linksService;
        _subscriptionLogic = subscriptionLogic;
        _events = eventsService;
    }

    public ActionResult<string> Shorter(string source) {
        if(string.IsNullOrEmpty(source) || source.Length < 4) {
            return "Invalid link";
        }

        if(!_subscriptionLogic.IsAllowed(UserActionCode.LinkAddition)) {
            return "You have reached limit, consider upgrading your membership";
        }

        source = source.Replace("`", "''");

        try { 
            string trueLink = Links.GetFullLink(source);
        } catch {
            return "Invalid link";
        }

        string hashLink = LinksHashing.HashLink();
        string finalLink = $"{HttpContext.Request.Host.Value}/go/{hashLink}";

        _linkService.Create(source, hashLink, _userSession.User.id);
        _events.Create(new EventObject {
            action_id = (int)UserActionCode.LinkAddition, 
            user_id = _userSession.User.id,
            device_id = _userSession.DeviceId,
            message = $"Shorted {source} into {finalLink}", 
        });

        return finalLink;
    }
}
