using Microsoft.AspNetCore.Mvc;
using ShorterLink.Models.User;
using ShorterLink.Code.Devices;
using ShorterLink.Code.Links;
using ShorterLink.Controllers.Links;
using ShorterLink.Code.Subscriptions;
using ShorterLink.Code.Subscriptions.Logic;
using Mysqlx.Crud;
using Microsoft.AspNetCore.Http.Connections;

namespace ShorterLink;

public class LinksController : Controller {
    private AppDatabase _database;
    private UserSession _userSession;
    private UserService _userService;
    private DeviceService _devices;
    private LinksService _linkService;
    private ISubscriptionLogic _subscriptionLogic;

    public LinksController(UserSession session, AppDatabase database, UserService userService, DeviceService deviceService, LinksService linksService, ISubscriptionLogic subscriptionLogic) { 
        _database = database;
        _userSession = session;
        _devices = deviceService;
        _userService = userService;
        _linkService = linksService;
        _subscriptionLogic = subscriptionLogic;
    }

    [Route("links/")]
    public IActionResult History() {
        if(_userSession.IsAnonymous) {
            return Redirect("/");
        }

        return View();
    }
    [Route("links/edit/{hash_url}")]
    public IActionResult Edit(string hash_url) {
        var hashedLink = _linkService[hash_url];

        if(hashedLink.user_id != _userSession.User.id) {
            Redirect("/links/");
        }

        return View(new {
            HashedLink = hashedLink
        });
    }
    [Route("links/edit/{hash_url}/save")]
    [HttpPost]
    public ActionResult<string> Save(string hash_url, string alias, bool active) {
        var hashedLink = _linkService[hash_url];

        if(hashedLink.user_id != _userSession.User.id) {
            return Redirect("/");
        }

        if(!string.IsNullOrWhiteSpace(alias)) {
            if(!Links.VerifyAlias(alias)) {
                return new LinkSaveResponse { 
                    message = "Only dash, underscore and latin characters are allowed in aliases",
                    redirect = "",
                    response_code = 400,
                }.GetJson();
            }
            if(!_subscriptionLogic.IsAllowed(Code.DataObjects.UserActionCode.LinkAddAlias)) {
                return new LinkSaveResponse {
                    message = "You can't assign aliases. Consider having superior plan",
                    response_code = 403,
                    redirect = ""
                }.GetJson();
            }

            _linkService.UpdateState(hash_url, alias, active);

            return new LinkSaveResponse { 
                message = "OK",
                redirect = "/links/",
                response_code = 200
            }.GetJson();
        }

        try { 
            _linkService.SetActive(hash_url, active);
        } catch {
            return new LinkSaveResponse { 
                message = "An unexpected error occured",
                redirect = "",
                response_code = 500
            }.GetJson();
        }

        return new LinkSaveResponse { 
            message = "OK",
            redirect = "/links/",
            response_code = 200
        }.GetJson();
    }
}
