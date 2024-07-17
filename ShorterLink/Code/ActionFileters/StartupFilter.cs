using Microsoft.AspNetCore.Mvc.Filters;

namespace ShorterLink.Code.ActionFileters;

public class StartupFilterAttribute : ActionFilterAttribute {
	public StartupFilterAttribute(IHttpContextAccessor context) {
	}

    public override void OnResultExecuted(ResultExecutedContext context)
    {
    }
}
