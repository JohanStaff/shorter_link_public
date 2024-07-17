using Microsoft.AspNetCore.Mvc.Filters;
using ShorterLink.Code.Devices;
using ShorterLink.Code.Events;
using ShorterLink.Code.Links;
using ShorterLink.Code.Links.LinkStats;
using ShorterLink.Code.Subscriptions;
using ShorterLink.Code.Subscriptions.Logic;

namespace ShorterLink.Code.ActionFileters;

public class StartupFilterAttribute : ActionFilterAttribute {
	public StartupFilterAttribute(IHttpContextAccessor context) {
	}

    public override void OnResultExecuted(ResultExecutedContext context)
    {
    }
}
