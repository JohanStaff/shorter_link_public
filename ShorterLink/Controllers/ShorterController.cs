using System.Text;
using Microsoft.AspNetCore.Mvc;
using ShorterLink.Code.DataObjects;
using ShorterLink.Code.Events;
using ShorterLink.Code.Links;
using ShorterLink.Code.Links.Exceptions;
using ShorterLink.Code.Links.LinkStats;
using ShorterLink.Code.Subscriptions.Logic;
using ShorterLink.Code.Workers;

namespace ShorterLink;

public class ShorterController : Controller {
    private readonly ILogger<ShorterController> _logger;
    private readonly EventsService _events;
    private readonly LinksService _linkService; 
    private readonly ISubscriptionLogic _subscriptionLogic;
    private readonly UserSession _userSession;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IConfiguration _configuration;

    public ShorterController(ILogger<ShorterController> logger, UserSession session, LinksService linksService,
                             ISubscriptionLogic subscriptionLogic, EventsService eventsService,
                             IHttpContextAccessor httpContext,
                             IConfiguration configuration)
    {
        _logger = logger;
        _userSession = session;
        _linkService = linksService;
        _subscriptionLogic = subscriptionLogic;
        _events = eventsService;
        _httpContext = httpContext;
        _configuration = configuration;
    }

    public ActionResult<string> Unshorten(string hash_url) {
        if(string.IsNullOrEmpty(hash_url) || hash_url.Length < 4) {
            return "Invalid link";
        }
        hash_url = hash_url.Replace("`", "''");
        var groups = Links.GetGroups(hash_url, _configuration.GetValue<bool?>("Debug") ?? false);

        if(groups["domain"].Value != _httpContext.HttpContext.Request.Host.Host) {
            return "You can unshorten only links that are issued by our service";
        }

        var query = groups["query"].Value;
        var splitQuery = query.Split('/');

        if(splitQuery[1] != "go") {
            return "Invalid link";
        }

        var link = _linkService[splitQuery[2]];
        
        if(link is null) {
            return "No such link on this service";
        }

        return link.original_url;
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
            string trueLink = Links.GetFullLinkAndCheck(source);
        } catch(DomainBannedException) {
            return "The domain is banned";
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
