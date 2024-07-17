using Microsoft.AspNetCore.Mvc; 
using ShorterLink.Code.Links;
using ShorterLink.Code.Links.Exceptions;
using ShorterLink.Code.Links.LinkStats;

namespace ShorterLink;

public class GoController : Controller {
    private AppDatabase _database; 
    private LinksService _linkService;
    private LinkStatsService _linkStats;

    public GoController(AppDatabase database, LinksService linksService, LinkStatsService linkStatsService) {
        _database = database;
        _linkService = linksService;
        _linkStats = linkStatsService;
    }    

    [Route("go/{hash_url}")]
    public ActionResult<string> Go(string hash_url) {
        DatabaseQuery theLink = _database.CreatePlainCommand("SELECT original_url, active FROM links WHERE hash_url = @url;");

        theLink.AddWithValue("@url", hash_url);

        var url = _linkService[hash_url]; 
        if(url is null || !url.active) {
            return RedirectToAction("LinkNotAvailable");
        }
        
        _linkStats.Increment(url.id);

        try { 
            return Redirect(Links.GetFullLinkAndCheck(url.original_url));
        } catch(DomainBannedException) {
            return RedirectToAction("LinkNotAvailable");
        }
    }
    
    [Route("go/na")]
    public ActionResult<string> LinkNotAvailable() {
        return View();
    }
}
